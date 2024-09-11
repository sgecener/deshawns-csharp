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