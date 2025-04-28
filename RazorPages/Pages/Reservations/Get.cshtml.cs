using System.ComponentModel.DataAnnotations;
using Domain.DTOs.ReservationDTOs;
using Domain.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.Reservations;

public class Get(IReservationService reservationService) : PageModel
{
    [BindProperty]
    public ReservationFilters Filters { get; set; } = new ReservationFilters
    {
        PageNumber = 1,
        PageSize = 10
    };

    [BindProperty]
    public List<GetReservationDTO> Reservations { get; set; } = new();
    [BindProperty]
    public List<string> Messages { get; set; } = new();
    public int TotalRecords { get; set; }

    public async Task<IActionResult> OnGetAsync(int? TableId = null, DateTime? ReservationDateFrom = null, 
        DateTime? ReservationDateTo = null, int PageNumber = 1, int PageSize = 10)
    {
        Filters.TableId = TableId ?? Filters.TableId;
        Filters.ReservationDateFrom = ReservationDateFrom?.Date.ToUniversalTime();
        Filters.ReservationDateTo = ReservationDateTo?.Date.ToUniversalTime();
        Filters.PageNumber = PageNumber > 0 ? PageNumber : 1;
        Filters.PageSize = PageSize > 0 ? PageSize : 10;
        
        var response = await reservationService.GetFilteredAsync(Filters);
        if (response.IsSuccess)
        {
            Reservations = response.Data!;
            TotalRecords = response.TotalRecords;
            return Page();
        }

        Messages.Add(response.Message);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int reservationId)
    {
        var response = await reservationService.DeleteAsync(reservationId);
        if (response.IsSuccess)
        {
            return RedirectToPage();
        }
        
        Messages.Add(response.Message);
        return await OnGetAsync();
    }
}