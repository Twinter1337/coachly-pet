using CoachlyBackEnd.Models;

namespace CoachlyBackEnd.Services.CRUD.Interfaces;

public interface ICrudService<T> where T : class
{
    public CoachlyDbContext Context { get; }
    Task<bool> CreateEntityAsync(T entity);
    Task<List<T>> GetAllEntitiesAsync();
    Task<T?> GetEntityByIdAsync(int id);
    Task<bool> UpdateEntityAsync<TDto>(int id, TDto entity);
    Task<bool> PatchEntityAsync<TUpdateDto>(int id, TUpdateDto patchDto);
    Task<bool> DeleteEntityAsync(int id);
}