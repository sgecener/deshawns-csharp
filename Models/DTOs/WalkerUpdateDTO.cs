namespace DeShawnsDogWalking.Models.DTOs;

public class WalkerUpdateDTO
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public List<CityWalkerAddDTO> CityWalkers {get; set;}
}