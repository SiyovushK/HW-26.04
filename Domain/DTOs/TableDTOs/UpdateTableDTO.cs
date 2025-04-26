namespace Domain.DTOs.TableDTOs;

public class UpdateTableDTO : CreateTableDTO
{
    public bool IsReserved { get; set; }
}