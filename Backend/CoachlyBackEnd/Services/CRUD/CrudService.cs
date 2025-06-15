using AutoMapper;
using CoachlyBackEnd.Models;
using CoachlyBackEnd.Services.CRUD.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CrudService<T> : ICrudService<T> where T : class
{
    public CoachlyDbContext Context { get; }
    protected readonly IMapper Mapper;

    public CrudService(CoachlyDbContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    public virtual async Task<bool> CreateEntityAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<List<T>> GetAllEntitiesAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetEntityByIdAsync(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    public virtual async Task<bool> UpdateEntityAsync<TDto>(int id, TDto dto)
    {
        var dbSet = Context.Set<T>();
        var entity = await dbSet.FindAsync(id);
        if (entity == null)
            return false;

        Mapper.Map(dto, entity);
        await Context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<bool> PatchEntityAsync<TPatchDto>(int id, TPatchDto dto)
    {
        var dbSet = Context.Set<T>();
        var entity = await dbSet.FindAsync(id);
        if (entity == null)
            return false;

        var dtoProperties = typeof(TPatchDto).GetProperties();
        var entityProperties = typeof(T).GetProperties();

        foreach (var dtoProp in dtoProperties)
        {
            var newValue = dtoProp.GetValue(dto);
            if (newValue == null)
                continue;

            var entityProp = entityProperties.FirstOrDefault(p => p.Name == dtoProp.Name);
            if (entityProp != null && entityProp.CanWrite)
            {
                entityProp.SetValue(entity, newValue);
            }
        }

        await Context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<bool> DeleteEntityAsync(int id)
    {
        var dbSet = Context.Set<T>();
        var entity = await dbSet.FindAsync(id);
        if (entity == null)
            return false;

        dbSet.Remove(entity);
        await Context.SaveChangesAsync();
        return true;
    }
}