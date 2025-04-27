using System.ComponentModel.DataAnnotations;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.Reservations;

public class Delete(IReservationService reservationService) : PageModel
{
    [Required]
    public int ReservationId { get; set; }
    public List<string> Messages { get; set; } = new();

    public async Task OnGetAsync(int ReservationId)
    {
        
    }
}