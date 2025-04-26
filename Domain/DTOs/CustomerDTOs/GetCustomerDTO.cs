namespace Domain.DTOs.CustomerDTOs;

public class GetCustomerDTO
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}