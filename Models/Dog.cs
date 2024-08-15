namespace DeShawnsDogWalking.Models;

public class Dog
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public int? WalkerId {get; set;}
    public int CityId {get; set;}
}