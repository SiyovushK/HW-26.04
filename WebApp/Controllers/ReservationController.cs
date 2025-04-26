using Domain.DTOs.ReservationDTOs;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController(IReservationService reservationService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetReservationDTO>> CreateAsync(CreateReservationDTO createReservation)
    {
        return await reservationService.CreateAsync(createReservation);
    }
    
    [HttpPut]
    public async Task<Response<GetReservationDTO>> UpdateAsync(int ReservationId, UpdateReservationDTO updateReservation)
    {
        return await reservationService.UpdateAsync(ReservationId, updateReservation);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int reservationId)
    {
        return await reservationService.DeleteAsync(reservationId);
    }
    
    [HttpGet("id")]
    public async Task<Response<GetReservationDTO>> GetByIdAsync(int ReservationId)
    {
        return await reservationService.GetByIdAsync(ReservationId);
    }
    
    [HttpGet("All")]
    public async Task<Response<List<GetReservationDTO>>> GetAllAsync()
    {
        return await reservationService.GetAllAsync();
    }
    
    [HttpGet("Filter")]
    public async Task<Response<List<GetReservationDTO>>> GetFilteredAsync([FromQuery] ReservationFilters filter)
    {
        return await reservationService.GetFilteredAsync(filter);
    }
}