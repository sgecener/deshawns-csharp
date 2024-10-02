using DeShawnsDogWalking.Models;
using DeShawnsDogWalking.Models.DTOs;

List<Dog> dogs = new List<Dog>()
{
    new Dog()
    {
        Id = 1,
        Name = "Fancy",
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
        Name = "Pillow",
        CityId = 3,
        WalkerId = 2
    },
    new Dog()
    {
        Id = 4,
        Name = "Radar",
        CityId = 4,
        WalkerId = 3
    },
    new Dog()
    {
        Id = 5,
        Name = "Jimmy",
        CityId = 1,
        WalkerId = 4
    },
    new Dog()
    {
        Id = 6,
        Name = "Logar",
        CityId = 2
    },
    new Dog()
    {
        Id = 7,
        Name = "Baby",
        CityId = 3
    },
    new Dog()
    {
        Id = 8,
        Name = "Tikka",
        CityId = 4,
        WalkerId = 5
    },
    new Dog()
    {
        Id = 9,
        Name = "Monkey",
        CityId = 1,
        WalkerId = 6
    },
    new Dog()
    {
        Id = 10,
        Name = "Bill",
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
        Name = "Chicago"
    },
    new City()
    {
        Id = 2,
        Name = "Nashville"
    },
    new City()
    {
        Id = 3,
        Name = "DC"
    },
    new City()
    {
        Id = 4,
        Name = "New York"
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
app.MapPost("api/dogs", (DogAddDTO dogAdd) =>
{
    if (dogAdd.Name == "" | dogAdd.Name == null | dogAdd.CityId != dogs.FirstOrDefault(dog => dog.CityId == dogAdd.CityId)?.CityId)
    {
        return Results.BadRequest();
    }

    Dog newDog = new Dog()
    {
        Id = dogs.Max(dog => dog.Id) + 1,
        Name = dogAdd.Name,
        CityId = dogAdd.CityId
    };

    dogs.Add(newDog);
    DogDTO createdDog = CreateDogDTO(newDog);
    
    return Results.Created($"/api/dogs/{newDog.Id}", createdDog);
});
app.MapPatch("api/dogs/assign", (DogAssignDTO dogToAssign) =>
{
    Dog foundDog = dogs.FirstOrDefault(dog => dog.Id == dogToAssign.Id);
    if (foundDog == null)
    {
        return Results.BadRequest();
    }

    Walker foundWalker = walkers.FirstOrDefault(walker => walker.Id == dogToAssign.WalkerId);
    if (foundWalker == null)
    {
        return Results.BadRequest();
    }

    CityWalker foundCityWalker = cityWalkers.FirstOrDefault(cityWalker => cityWalker.CityId == dogToAssign.CityId & cityWalker.WalkerId == dogToAssign.WalkerId);
    if (foundCityWalker == null)
    {
        return Results.BadRequest();
    }

    foundDog.WalkerId = dogToAssign.WalkerId;

    DogDTO dogDTO = CreateDogDTO(foundDog);

    return Results.Created($"api/dogs/{dogDTO.Id}", dogDTO);
});
app.MapGet("api/cities", () =>
{
    List<CityDTO> cityDTOs = new List<CityDTO>();

    foreach (City city in cities)
    {
        cityDTOs.Add(new CityDTO()
        {
            Id = city.Id,
            Name = city.Name
        });
    }

    return cityDTOs;
});

app.MapPost("api/cities", (CityAddDTO city) =>
{
    if (String.IsNullOrWhiteSpace(city.Name))
    {
        return Results.BadRequest();
    }

    City newCity = new City()
    {
        Id = cities.Max(city => city.Id) + 1,
        Name = city.Name
    };
    cities.Add(newCity);

    CityDTO createdCity = new CityDTO()
    {
        Id = newCity.Id,
        Name = newCity.Name
    };

    return Results.Created($"api/cities/{newCity.Id}", createdCity);
});

app.MapGet("api/walkers", () =>
{
    List<WalkerDTO> walkerDTOs = new List<WalkerDTO>();

    foreach (Walker walker in walkers)
    {
        List<CityWalker> foundCityWalkers = cityWalkers.Where(cityWalker => cityWalker.WalkerId == walker.Id).ToList();

        WalkerDTO newWalkerDTO = new WalkerDTO()
        {
            Id = walker.Id,
            Name = walker.Name,
            CityWalkers = foundCityWalkers.Select(cityWalker => new CityWalkerDTO()
            {
                Id = cityWalker.Id,
                CityId = cityWalker.CityId,
                WalkerId = cityWalker.WalkerId
            }).ToList(),
        };
        
        walkerDTOs.Add(newWalkerDTO);
    }

    return walkerDTOs;
});


app.MapGet("api/walkers/{id}", (int id) => 
{
    Walker foundWalker = walkers.FirstOrDefault(walker => walker.Id == id);
    if (foundWalker == null)
    {
        return Results.NotFound();
    }

    WalkerDTO walkerDTO = CreateWalkerDTO(foundWalker);

    return Results.Ok(walkerDTO);
});
app.MapPut("api/walkers", (WalkerUpdateDTO walkerDTO) =>
{
    if (String.IsNullOrWhiteSpace(walkerDTO.Name) | walkerDTO.CityWalkers == null)
    {
        return Results.BadRequest();
    }
    
    Walker foundWalker = walkers.FirstOrDefault(walker => walker.Id == walkerDTO.Id);
    if (foundWalker == null)
    {
        return Results.BadRequest();
    }

    // Removes all old City Walkers associated with found Walker
    List<CityWalker> oldCityWalkers = cityWalkers.Where(cityWalker => cityWalker.WalkerId == foundWalker.Id).ToList();
    foreach (CityWalker cityWalker in oldCityWalkers)
    {
        cityWalkers.Remove(cityWalker);
    }

    // Creates new City Walkers based received WalkerUpdateDTO
    foreach (CityWalkerAddDTO cityWalkerAddDTO in walkerDTO.CityWalkers)
    {
        cityWalkers.Add(new CityWalker()
        {
            Id = cityWalkers.Max(cityWalker => cityWalker.Id),
            CityId = cityWalkerAddDTO.CityId,
            WalkerId = cityWalkerAddDTO.WalkerId
        });
    }

    // Updates Dogs that can no longer be walked by the Walker
    List<Dog> foundDogs = dogs.Where(dog => dog.WalkerId == foundWalker.Id).ToList();
    foreach (Dog dog in foundDogs)
    {
        if (dog.CityId != cityWalkers.Find(cityWalker => cityWalker.WalkerId == foundWalker.Id).CityId)
        {
            dog.WalkerId = null;
        }
    }

    foundWalker.Name = walkerDTO.Name;

    WalkerDTO updatedWalker = CreateWalkerDTO(foundWalker);

    return Results.Created($"api/walkers/{updatedWalker.Id}", updatedWalker);
});
app.MapDelete("api/walkers/{id}", (int id) =>
{
    Walker foundWalker = walkers.FirstOrDefault(walker => walker.Id == id);
    if (foundWalker == null)
    {
        return Results.NotFound();
    }

    WalkerDTO deletedWalker = CreateWalkerDTO(foundWalker);
    walkers.Remove(foundWalker);

    cityWalkers.RemoveAll(cityWalker => cityWalker.WalkerId == id);
    dogs.Select(dog => 
    {
        if (dog.WalkerId == id)
        {
            dog.WalkerId = null;
        }

        return dog;
    }).ToList();

    return Results.Ok(deletedWalker);
});

