using Domain.DTOs.CustomerDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetCustomerDTO>> CreateAsync(CreateCustomerDTO createCustomer)
    {
        return await customerService.CreateAsync(createCustomer);
    }
    
    [HttpPut]
    public async Task<Response<GetCustomerDTO>> UpdateAsync(int CustomerId, UpdateCustomerDTO updateCustomer)
    {
        return await customerService.UpdateAsync(CustomerId, updateCustomer);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int CustomerId)
    {
        return await customerService.DeleteAsync(CustomerId);
    }
    
    [HttpGet("id")]
    public async Task<Response<GetCustomerDTO>> GetByIdAsync(int CustomerId)
    {
        return await customerService.GetByIdAsync(CustomerId);
    }
    
    [HttpGet("All")]
    public async Task<Response<List<GetCustomerDTO>>> GetAllAsync()
    {
        return await customerService.GetAllAsync();
    }
}