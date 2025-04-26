using System.ComponentModel.DataAnnotations;
using Domain.DTOs.TableDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.Tables;

public class Get(ITableService tableService) : PageModel
{
    [Required]
    [BindProperty]
    public List<GetTableDTO> getTableDTO { get; set; } = new();
    [BindProperty]
    public List<string> Messages { get; set; } = new();

    public async Task OnGetAsync()
    {
        var result = await tableService.GetAllAsync();
        if (!ModelState.IsValid)
        {
            Messages.Add("Something went wrong");
            return;
        }

        getTableDTO = result.Data!;
    }
}