namespace Domain.Filters;

public class ReservationFilters
{
    public int? TableId { get; set; }
    public DateTime? ReservationDateFrom { get; set; }
    public DateTime? ReservationDateTo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}