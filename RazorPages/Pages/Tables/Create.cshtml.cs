using System.ComponentModel.DataAnnotations;
using Domain.DTOs.TableDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.Tables;

public class Create(ITableService tableService) : PageModel
{
    [Required]
    [BindProperty]
    public CreateTableDTO createTable { get; set; } = new();
    [BindProperty]
    public List<string> Messages { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Messages = ModelState.Values.SelectMany(m => m.Errors).Select(x => x.ErrorMessage).ToList();
            return Page();
        }

        var response = await tableService.CreateAsync(createTable);
        if (response.IsSuccess)
        {
            return Redirect("/Tables/Get");
        }

        Messages.Add(response.Message);
        return Page();
    }
}