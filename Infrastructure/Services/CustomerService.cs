using System.Net;
using AutoMapper;
using Domain.DTOs.CustomerDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CustomerService(DataContext context, IMapper mapper) : ICustomerService
{
    public async Task<Response<GetCustomerDTO>> CreateAsync(CreateCustomerDTO createCustomer)
    {
        var Customer = mapper.Map<Customer>(createCustomer);

        await context.Customers.AddAsync(Customer);
        var result = await context.SaveChangesAsync();

        var getCustomerDTO = mapper.Map<GetCustomerDTO>(Customer);

        return getCustomerDTO == null
            ? new Response<GetCustomerDTO>(HttpStatusCode.InternalServerError, "Customer couldn't be added")
            : new Response<GetCustomerDTO>(getCustomerDTO);  
    }
  
    public async Task<Response<GetCustomerDTO>> UpdateAsync(int CustomerId, UpdateCustomerDTO updateCustomer)
    {
        var info = await context.Customers.FindAsync(CustomerId);
        if (info == null)
            return new Response<GetCustomerDTO>(HttpStatusCode.NotFound, "Customer not found");

        mapper.Map(updateCustomer, info);
        var result = await context.SaveChangesAsync();

        var getCustomerDTO = mapper.Map<GetCustomerDTO>(info);

        return getCustomerDTO == null
            ? new Response<GetCustomerDTO>(HttpStatusCode.InternalServerError, "Customer couldn't be updated")
            : new Response<GetCustomerDTO>(getCustomerDTO);
    }

    public async Task<Response<string>> DeleteAsync(int CustomerId)
    {
        var info = await context.Customers.FindAsync(CustomerId);
        if (info == null)
            return new Response<string>(HttpStatusCode.NotFound, "Customer not found");

        context.Customers.Remove(info);
        var result = await context.SaveChangesAsync();

        var getCustomerDTO = mapper.Map<GetCustomerDTO>(info);

        return getCustomerDTO == null
            ? new Response<string>(HttpStatusCode.InternalServerError, "Customer couldn't be deleted")
            : new Response<string>("Customer deleted successfully");
    }

    public async Task<Response<GetCustomerDTO>> GetByIdAsync(int CustomerId)
    {
        var info = await context.Customers.FindAsync(CustomerId);
        if (info == null)
            return new Response<GetCustomerDTO>(HttpStatusCode.NotFound, "Customer not found");

        var getCustomerDTO = mapper.Map<GetCustomerDTO>(info);

        return getCustomerDTO == null
            ? new Response<GetCustomerDTO>(HttpStatusCode.InternalServerError, "Customer couldn't be deleted")
            : new Response<GetCustomerDTO>(getCustomerDTO);
    }

    public async Task<Response<List<GetCustomerDTO>>> GetAllAsync()
    {
        var Customers = await context.Customers.ToListAsync();

        var getCustomersDTO = mapper.Map<List<GetCustomerDTO>>(Customers);

        return Customers.Count() == 0
            ? new Response<List<GetCustomerDTO>>(HttpStatusCode.NotFound, "Customers not found")
            : new Response<List<GetCustomerDTO>>(getCustomersDTO);
    }
}