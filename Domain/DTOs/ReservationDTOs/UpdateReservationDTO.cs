namespace Domain.DTOs.ReservationDTOs;

public class UpdateReservationDTO : CreateReservationDTO
{
    public DateTime ReservationDate { get; set; }
}