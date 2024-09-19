using DeShawnsDogWalking.Models;
using DeShawnsDogWalking.Models.DTOs;

List<Dog> dogs = new List<Dog>()
{
    new Dog()
    {
        Id = 1,
        Name = "Baxter",
        CityId = 1,
        WalkerId = 1
    },
    new Dog()
    {
        Id = 2,
        Name = "Luna",
        CityId = 2,
        WalkerId = 1
    },
    new Dog()
    {
        Id = 3,
        Name = "Charlie",
        CityId = 3,
        WalkerId = 2
    },
    new Dog()
    {
        Id = 4,
        Name = "Bella",
        CityId = 4,
        WalkerId = 3
    },
    new Dog()
    {
        Id = 5,
        Name = "Max",
        CityId = 1,
        WalkerId = 4
    },
    new Dog()
    {
        Id = 6,
        Name = "Daisy",
        CityId = 2
    },
    new Dog()
    {
        Id = 7,
        Name = "Oliver",
        CityId = 3
    },
    new Dog()
    {
        Id = 8,
        Name = "Lucy",
        CityId = 4,
        WalkerId = 5
    },
    new Dog()
    {
        Id = 9,
        Name = "Cooper",
        CityId = 1,
        WalkerId = 6
    },
    new Dog()
    {
        Id = 10,
        Name = "Molly",
        CityId = 2,
        WalkerId = 7
    }
};

List<Walker> walkers = new List<Walker>()
{
    new Walker()
    {
        Id = 1,
        Name = "Isabelle"
    },
    new Walker()
    {
        Id = 2,
        Name = "Renee"
    },
    new Walker()
    {
        Id = 3,
        Name = "Ethan"
    },
    new Walker()
    {
        Id = 4,
        Name = "Kyo"
    },
    new Walker()
    {
        Id = 5,
        Name = "Rob"
    },
    new Walker()
    {
        Id = 6,
        Name = "Jord"
    },
    new Walker()
    {
        Id = 7,
        Name = "Chelsea"
    }
};
List<City> cities = new List<City>()
{
    new City()
    {
        Id = 1,
        Name = "Cordova"
    },
    new City()
    {
        Id = 2,
        Name = "Nashville"
    },
    new City()
    {
        Id = 3,
        Name = "Chattanooga"
    },
    new City()
    {
        Id = 4,
        Name = "Memphis"
    }
};
List<CityWalker> cityWalkers = new List<CityWalker>()
{
    new CityWalker()
    {
        Id = 1,
        CityId = 1,
        WalkerId = 1
    },
    new CityWalker()
    {
        Id = 2,
        CityId = 2,
        WalkerId = 1
    },
    new CityWalker()
    {
        Id = 3,
        CityId = 3,
        WalkerId = 2
    },
    new CityWalker()
    {
        Id = 4,
        CityId = 4,
        WalkerId = 3
    },
    new CityWalker()
    {
        Id = 5,
        CityId = 1,
        WalkerId = 4
    },
    new CityWalker()
    {
        Id = 6,
        CityId = 1,
        WalkerId = 5
    },
    new CityWalker()
    {
        Id = 7,
        CityId = 4,
        WalkerId = 5
    },
    new CityWalker()
    {
        Id = 8,
        CityId = 1,
        WalkerId = 6
    },
    new CityWalker()
    {
        Id = 9,
        CityId = 2,
        WalkerId = 7
    },
    new CityWalker()
    {
        Id = 10,
        CityId = 1,
        WalkerId = 8
    },
    new CityWalker()
    {
        Id = 11,
        CityId = 4,
        WalkerId = 8
    }
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var CreateDogDTO = (Dog dog) =>
{
    City foundCity = cities.First(city => city.Id == dog.CityId);
    Walker foundWalker = null;
    if (dog.WalkerId != null)
    {
        foundWalker = walkers.FirstOrDefault(walker => walker.Id == dog.WalkerId);
    }
    
    DogDTO dogDTO = new DogDTO()
    {
        Id = dog.Id,
        Name = dog.Name,
        WalkerId = dog.WalkerId,
        CityId = dog.CityId,
        City = new CityDTO()
        {
            Id = foundCity.Id,
            Name = foundCity.Name
        }
    };
    if (foundWalker != null)
    {
        dogDTO.Walker = new WalkerDTO()
        {
            Id = foundWalker.Id,
            Name = foundWalker.Name
        };
    }

    return dogDTO;
};

var CreateWalkerDTO = (Walker walker) =>
{
    List<CityWalker> foundCityWalkers = cityWalkers.Where(cityWalker => cityWalker.WalkerId == walker.Id).ToList();
    List<Dog> foundDogs = dogs.Where(dog => dog.WalkerId == walker.Id).ToList();

    WalkerDTO walkerDTO = new WalkerDTO()
    {
        Id = walker.Id,
        Name = walker.Name,
        CityWalkers = foundCityWalkers.Select(cityWalker => new CityWalkerDTO()
        {
            Id = cityWalker.Id,
            CityId = cityWalker.CityId,
            WalkerId = cityWalker.WalkerId
        }).ToList(),
        Dogs = foundDogs.Select(dog => new DogDTO()
        {
            Id = dog.Id,
            Name = dog.Name,
            CityId = dog.CityId,
            WalkerId = dog.WalkerId
        }).ToList()
    };

    return walkerDTO;
};
app.MapGet("api/dogs", () =>
{
    List<DogDTO> dogDTOs = new List<DogDTO>();

    foreach (Dog dog in dogs)
    {
        dogDTOs.Add(new DogDTO
        {
            Id = dog.Id,
            Name = dog.Name,
            WalkerId = dog.WalkerId,
            CityId = dog.CityId
        });
    }

    return dogDTOs;
});

app.MapGet("api/dogs/{id}", (int id) =>
{
    Dog dog = dogs.FirstOrDefault(dog => dog.Id == id);
    if (dog == null)
    {
        return Results.NotFound();
    }
    
    DogDTO dogDTO = CreateDogDTO(dog);

    return Results.Ok(dogDTO);
});

app.MapDelete("api/dogs/{id}", (int id) =>
{
    Dog foundDog = dogs.FirstOrDefault(dog => dog.Id == id);
    if (foundDog == null)
    {
        return Results.NotFound();
    }

    DogDTO dogDTO = CreateDogDTO(foundDog);
    dogs.Remove(foundDog);

    return Results.Ok(dogDTO);
});

app.MapGet("api/dogs/assign/{walkerId}", (int walkerId) =>
{
    List<DogDTO> dogDTOs = new List<DogDTO>();
    List<CityWalker> servicedCities = cityWalkers.Where(cityWalker => cityWalker.WalkerId == walkerId).ToList();

    foreach (Dog dog in dogs)
    {
        if (walkerId != dog.WalkerId & servicedCities.FirstOrDefault(cityWalker => cityWalker.CityId == dog.CityId) != null)
        {
            dogDTOs.Add(new DogDTO
            {
                Id = dog.Id,
                Name = dog.Name,
                WalkerId = dog.WalkerId,
                CityId = dog.CityId
            });
        }
    }

    return dogDTOs;
});