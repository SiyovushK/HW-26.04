using System.Net;
using AutoMapper;
using Domain.DTOs.TableDTOs;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TableService(DataContext context, IMapper mapper) : ITableService
{
    public async Task<Response<GetTableDTO>> CreateAsync(CreateTableDTO createTable)
    {
        var table = mapper.Map<Table>(createTable);
        table.IsReserved = false;

        await context.Tables.AddAsync(table);
        var result = await context.SaveChangesAsync();

        var getTableDTO = mapper.Map<GetTableDTO>(table);

        return getTableDTO == null
            ? new Response<GetTableDTO>(HttpStatusCode.InternalServerError, "Table couldn't be added")
            : new Response<GetTableDTO>(getTableDTO);
    }

    public async Task<Response<GetTableDTO>> UpdateAsync(int tableId, UpdateTableDTO updateTable)
    {
        var info = await context.Tables.FindAsync(tableId);
        if (info == null)
            return new Response<GetTableDTO>(HttpStatusCode.NotFound, "Table not found");

        mapper.Map(updateTable, info);
        var result = await context.SaveChangesAsync();

        var getTableDTO = mapper.Map<GetTableDTO>(info);

        return getTableDTO == null
            ? new Response<GetTableDTO>(HttpStatusCode.InternalServerError, "Table couldn't be updated")
            : new Response<GetTableDTO>(getTableDTO);
    }

    public async Task<Response<string>> DeleteAsync(int tableId)
    {
        var info = await context.Tables.FindAsync(tableId);
        if (info == null)
            return new Response<string>(HttpStatusCode.NotFound, "Table not found");

        context.Tables.Remove(info);
        var result = await context.SaveChangesAsync();

        var getTableDTO = mapper.Map<GetTableDTO>(info);

        return getTableDTO == null
            ? new Response<string>(HttpStatusCode.InternalServerError, "Table couldn't be deleted")
            : new Response<string>("Table deleted successfully");
    }

    public async Task<Response<GetTableDTO>> GetByIdAsync(int tableId)
    {
        var info = await context.Tables.FindAsync(tableId);
        if (info == null)
            return new Response<GetTableDTO>(HttpStatusCode.NotFound, "Table not found");

        var getTableDTO = mapper.Map<GetTableDTO>(info);

        return getTableDTO == null
            ? new Response<GetTableDTO>(HttpStatusCode.InternalServerError, "Table couldn't be deleted")
            : new Response<GetTableDTO>(getTableDTO);
    }

    public async Task<Response<List<GetTableDTO>>> GetAllAsync()
    {
        var tables = await context.Tables.ToListAsync();

        var getTablesDTO = mapper.Map<List<GetTableDTO>>(tables);

        return tables.Count() == 0
            ? new Response<List<GetTableDTO>>(HttpStatusCode.NotFound, "Tables not found")
            : new Response<List<GetTableDTO>>(getTablesDTO);
    }

    public async Task<Response<int>> TablesTotalCountAsync()
    {
        var tablesCount = await context.Tables.CountAsync();

        return new Response<int>(tablesCount);
    }
}