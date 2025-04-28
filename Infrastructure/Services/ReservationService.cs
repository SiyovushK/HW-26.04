using System.Net;
using AutoMapper;
using Domain.DTOs.ReservationDTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ReservationService(DataContext context, IMapper mapper) : IReservationService
{
    public async Task<Response<GetReservationDTO>> CreateAsync(CreateReservationDTO createReservation)
    {
        var table = await context.Tables.FindAsync(createReservation.TableId);
        if (table == null)
            return new Response<GetReservationDTO>(HttpStatusCode.NotFound, "Table not found");
        if (table.IsReserved)
            return new Response<GetReservationDTO>(HttpStatusCode.BadRequest, "Table is reserved");
        table.IsReserved = true;    

        var customer = await context.Customers.FindAsync(createReservation.CustomerId);
        if (customer == null)
            return new Response<GetReservationDTO>(HttpStatusCode.NotFound, "Customer not found");

        var reservation = mapper.Map<Reservation>(createReservation);

        await context.Reservations.AddAsync(reservation);
        var result = await context.SaveChangesAsync();

        var getReservationDTO = mapper.Map<GetReservationDTO>(reservation);

        return getReservationDTO == null
            ? new Response<GetReservationDTO>(HttpStatusCode.InternalServerError, "Reservation couldn't be added")
            : new Response<GetReservationDTO>(getReservationDTO);
    }

    public async Task<Response<GetReservationDTO>> UpdateAsync(int ReservationId, UpdateReservationDTO updateReservation)
    {
        var info = await context.Reservations.FindAsync(ReservationId);
        if (info == null)
            return new Response<GetReservationDTO>(HttpStatusCode.NotFound, "Reservation not found");

        if (info.TableId != updateReservation.TableId)
        {
            var table = await context.Tables.FindAsync(updateReservation.TableId);
            if (table == null)
                return new Response<GetReservationDTO>(HttpStatusCode.NotFound, $"Table not found");
            if (table.IsReserved)
                return new Response<GetReservationDTO>(HttpStatusCode.BadRequest, $"Table with Id {updateReservation.TableId} is already taken");

            table.IsReserved = true;
    
            var updateOldTable = await context.Tables.FindAsync(info.TableId);
            updateOldTable!.IsReserved = false;

            info.TableId = updateReservation.TableId;
        }

        if (info.CustomerId != updateReservation.CustomerId)
        {
            var customer = await context.Customers.FindAsync(updateReservation.CustomerId);
            if (customer == null)
                return new Response<GetReservationDTO>(HttpStatusCode.NotFound, "Customer not found");

            info.CustomerId = updateReservation.CustomerId;
        }

        info.ReservationDate = updateReservation.ReservationDate;

        var result = await context.SaveChangesAsync();

        var getReservationDTO = mapper.Map<GetReservationDTO>(info);

        return getReservationDTO == null
            ? new Response<GetReservationDTO>(HttpStatusCode.InternalServerError, "Reservation couldn't be updated")
            : new Response<GetReservationDTO>(getReservationDTO);
    }

    public async Task<Response<string>> DeleteAsync(int reservationId)
    {
        var info = await context.Reservations
            .Include(r => r.Table)
            .FirstOrDefaultAsync(r => r.Id == reservationId);
        if (info == null)
            return new Response<string>(HttpStatusCode.NotFound, "Reservation not found");

        if (info.Table != null)
            info.Table.IsReserved = false;

        context.Reservations.Remove(info);
        var result = await context.SaveChangesAsync();

        var getReservationDTO = mapper.Map<GetReservationDTO>(info);

        return getReservationDTO == null
            ? new Response<string>(HttpStatusCode.InternalServerError, "Reservation couldn't be deleted")
            : new Response<string>("Reservation deleted successfully");
    }

    public async Task<Response<GetReservationDTO>> GetByIdAsync(int ReservationId)
    {
        var info = await context.Reservations.FindAsync(ReservationId);
        if (info == null)
            return new Response<GetReservationDTO>(HttpStatusCode.NotFound, "Reservation not found");

        var getReservationDTO = mapper.Map<GetReservationDTO>(info);

        return getReservationDTO == null
            ? new Response<GetReservationDTO>(HttpStatusCode.InternalServerError, "Reservation couldn't be deleted")
            : new Response<GetReservationDTO>(getReservationDTO);
    }

    public async Task<Response<List<GetReservationDTO>>> GetAllAsync()
    {
        var Reservations = await context.Reservations.ToListAsync();

        var getReservationsDTO = mapper.Map<List<GetReservationDTO>>(Reservations);

        return Reservations.Count() == 0
            ? new Response<List<GetReservationDTO>>(HttpStatusCode.NotFound, "Reservations not found")
            : new Response<List<GetReservationDTO>>(getReservationsDTO);
    }

    public async Task<PagedResponse<List<GetReservationDTO>>> GetFilteredAsync(ReservationFilters filter)
    {
        var pageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
        var pageSize = filter.PageSize < 10 ? 10 : filter.PageSize;

        var reservationsQuery = context.Reservations.AsQueryable();

        if (filter.TableId != null)
        {
            reservationsQuery = reservationsQuery
                .Where(r => r.TableId == filter.TableId);
        }        

        if (filter.ReservationDateFrom != null)
        {
            var dateFrom = filter.ReservationDateFrom.Value.Date.ToUniversalTime();
            reservationsQuery = reservationsQuery
                .Where(r => r.ReservationDate >= dateFrom);
        }        

        if (filter.ReservationDateTo != null)
        {
            var dateTo = filter.ReservationDateTo.Value.Date.AddDays(1).ToUniversalTime();
            reservationsQuery = reservationsQuery
                .Where(r => r.ReservationDate < dateTo);
        }

        var totalRecords = await reservationsQuery.CountAsync();    

        var result = await reservationsQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var getReservationDTO = mapper.Map<List<GetReservationDTO>>(result);    

        return new PagedResponse<List<GetReservationDTO>>(getReservationDTO, pageSize, pageNumber, totalRecords);
    }
}