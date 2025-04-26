namespace Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int CustomerId { get; set; }
    public DateTime ReservationDate { get; set; }

    public virtual Table Table { get; set; }
    public virtual Customer Customer { get; set; }
}