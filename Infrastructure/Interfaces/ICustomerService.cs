using Domain.DTOs.CustomerDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
    Task<Response<GetCustomerDTO>> CreateAsync(CreateCustomerDTO createCustomer);
    Task<Response<GetCustomerDTO>> UpdateAsync(int CustomerId, UpdateCustomerDTO updateCustomer);
    Task<Response<string>> DeleteAsync(int CustomerId);
    Task<Response<GetCustomerDTO>> GetByIdAsync(int CustomerId);
    Task<Response<List<GetCustomerDTO>>> GetAllAsync();
}