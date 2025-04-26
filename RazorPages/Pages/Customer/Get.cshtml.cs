using System.ComponentModel.DataAnnotations;
using Domain.DTOs.CustomerDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages.Customer;

public class Get(ICustomerService customerService) : PageModel
{
    [Required]
    [BindProperty]
    public List<GetCustomerDTO> getCustomer { get; set; } = new();
    [BindProperty]
    public List<string> Messages { get; set; } = new();

    public async Task OnGetAsync()
    {
        var result = await customerService.GetAllAsync();
        if (!ModelState.IsValid)
        {
            Messages.Add("Something went wrong");
            return;
        }

        getCustomer = result.Data!;
    }
}