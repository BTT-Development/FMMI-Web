using FMMI_Domain;
using FMMI_Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FMMI_Service.Services.Base;

internal class BaseService<T> where T : class, IBaseIdEntity
{
    protected readonly FMMIContext _context;

    protected readonly DbSet<T> _dbSet;

    protected BaseService(FMMIContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    protected async Task<int> CreateAsync(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return 0;
        }
    }
    /// <summary>
    /// Update <paramref name="entity"/> in db
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>The task result contains the number of state entries written to the database.</returns>
    protected async Task<int> UpdateAsync(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return 0;
        }
    }
    /// <summary>
    /// Remove <paramref name="entity"/> From db
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>The task result contains the number of state entries written to the database.</returns>
    protected async Task<int> HardDeleteAsync(T entity)
    {
        try
        {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return 0;
        }
    }
}
