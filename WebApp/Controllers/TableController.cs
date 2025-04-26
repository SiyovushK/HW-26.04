using Domain.DTOs.TableDTOs;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TableController(ITableService tableService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetTableDTO>> CreateAsync(CreateTableDTO createTable)
    {
        return await tableService.CreateAsync(createTable);
    }
    
    [HttpPut]
    public async Task<Response<GetTableDTO>> UpdateAsync(int tableId, UpdateTableDTO updateTable)
    {
        return await tableService.UpdateAsync(tableId, updateTable);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int tableId)
    {
        return await tableService.DeleteAsync(tableId);
    }
    
    [HttpGet("id")]
    public async Task<Response<GetTableDTO>> GetByIdAsync(int tableId)
    {
        return await tableService.GetByIdAsync(tableId);
    }
    
    [HttpGet("All")]
    public async Task<Response<List<GetTableDTO>>> GetAllAsync()
    {
        return await tableService.GetAllAsync();
    }
    
    [HttpGet("TablesCount")]
    public async Task<Response<int>> TablesTotalCountAsync()
    {
        return await tableService.TablesTotalCountAsync();
    }
}