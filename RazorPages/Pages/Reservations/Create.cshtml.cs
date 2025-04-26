using System.ComponentModel.DataAnnotations;
using Domain.DTOs.ReservationDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.Reservations;

public class Create(IReservationService reservationService) : PageModel
{
    [Required]
    [BindProperty]
    public CreateReservationDTO reservation { get; set; } = new();
    [BindProperty]
    public List<string> Messages { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Messages = ModelState.Values.SelectMany(m => m.Errors).Select(x => x.ErrorMessage).ToList();
            return Page();
        }

        var response = await reservationService.CreateAsync(reservation);
        if (response.IsSuccess)
        {
            return Redirect("/Tables/Get");
        }

        Messages.Add(response.Message);
        return Page();
    }
}