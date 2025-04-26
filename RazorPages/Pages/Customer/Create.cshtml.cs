using System.ComponentModel.DataAnnotations;
using Domain.DTOs.CustomerDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.Customer;

public class Create(ICustomerService customerService) : PageModel
{
    [Required]
    [BindProperty]
    public CreateCustomerDTO createCustomer { get; set; } = new();
    [BindProperty]
    public List<string> Messages { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Messages = ModelState.Values.SelectMany(m => m.Errors).Select(x => x.ErrorMessage).ToList();
            return Page();
        }

        var result = await customerService.CreateAsync(createCustomer);
        if (result.IsSuccess)
        {
            return Redirect("/Customer/Get");
        }

        Messages.Add(result.Message);
        return Page();
    }
}