using Domain.DTOs.TableDTOs;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface ITableService
{
    Task<Response<GetTableDTO>> CreateAsync(CreateTableDTO createTable);
    Task<Response<GetTableDTO>> UpdateAsync(int tableId, UpdateTableDTO updateTable);
    Task<Response<string>> DeleteAsync(int tableId);
    Task<Response<GetTableDTO>> GetByIdAsync(int tableId);
    Task<Response<List<GetTableDTO>>> GetAllAsync();
    Task<Response<int>> TablesTotalCountAsync();
}