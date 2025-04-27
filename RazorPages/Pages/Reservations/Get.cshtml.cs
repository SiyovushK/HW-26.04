using System.ComponentModel.DataAnnotations;
using Domain.DTOs.ReservationDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.Reservations;

public class Get(IReservationService reservationService) : PageModel
{
    [Required]
    [BindProperty]
    public List<GetReservationDTO> getReservation { get; set; } = new();
    [BindProperty]
    public List<string> Messages { get; set; } = new();

    public async Task OnGetAsync()
    {
        var result = await reservationService.GetAllAsync();
        if (!result.IsSuccess)
        {
            Messages.Add(result.Message);
            return;
        }

        getReservation = result.Data!;
    }

    public async Task<IActionResult> OnPostAsync(int ReservationId)
    {
        var result = await reservationService.DeleteAsync(ReservationId);
        if (!result.IsSuccess)
        {
            Messages.Add(result.Message);
            return Page();
        }

        return RedirectToPage();
    }
}