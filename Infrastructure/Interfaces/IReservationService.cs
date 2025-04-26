using Domain.DTOs.ReservationDTOs;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IReservationService
{
    Task<Response<GetReservationDTO>> CreateAsync(CreateReservationDTO createReservation);
    Task<Response<GetReservationDTO>> UpdateAsync(int ReservationId, UpdateReservationDTO updateReservation);
    Task<Response<string>> DeleteAsync(int reservationId);
    Task<Response<GetReservationDTO>> GetByIdAsync(int ReservationId);
    Task<Response<List<GetReservationDTO>>> GetAllAsync();
    Task<Response<List<GetReservationDTO>>> GetFilteredAsync(ReservationFilters filter);
}