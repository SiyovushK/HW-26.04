using System.ComponentModel.DataAnnotations;
using Domain.DTOs.ReservationDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql.Replication;

namespace RazorPages.Pages.Reservations;

public class Create(IReservationService reservationService) : PageModel
{
    [Required]
    [BindProperty]
    public CreateReservationDTO reservation { get; set; } = new();
    [BindProperty]
    public List<string> Messages { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int tableId)
    {
        reservation.TableId = tableId;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Messages = ModelState.Values.SelectMany(m => m.Errors).Select(x => x.ErrorMessage).ToList();
            return Page();
        }

        reservation.ReservationDate = reservation.ReservationDate.ToUniversalTime();

        var response = await reservationService.CreateAsync(reservation);
        if (response.IsSuccess)
        {
            return Redirect("/Reservations/Get");
        }

        Messages.Add(response.Message);
        return Page();
    }
}