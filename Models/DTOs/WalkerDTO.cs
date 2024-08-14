namespace DeShawnsDogWalking.Models.DTOs;

public class WalkerDTO
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public List<CityWalkerDTO> CityWalkers {get; set;}
    public List<DogDTO> Dogs {get; set;}
}