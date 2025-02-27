﻿using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Enums;
using BloodDonationDb.Domain.Repositories.BloodStock;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationDb.Infrastructure.Persistence.Repositories;
public class BloodStockRepository : IBloodStockReadOnlyRepository, IBloodStockUpdateOnlyRepository
{
    private readonly BloodDonationDbContext _dbContext;

    public BloodStockRepository(BloodDonationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BloodStock>> GetAllBloodStocksAsync()
        => await _dbContext
        .BloodStocks
        .AsNoTracking()
        .ToListAsync();

    public async Task<BloodStock> GetBloodStockAsync(BloodType bloodType, RhFactor rhFactor)
        => await _dbContext
        .BloodStocks
        .AsNoTracking()
        .SingleOrDefaultAsync(stock => stock.BloodType.Equals(bloodType) && stock.RhFactor.Equals(rhFactor));

    public async Task<BloodStock> GetBloodStockByIdAsync(Guid id)
        => await _dbContext.BloodStocks
        .AsNoTracking()
        .SingleOrDefaultAsync(stock => stock.Id == id);
         

    public void UpdateBloodStock(BloodStock bloodStock)
        => _dbContext.BloodStocks.Update(bloodStock);
}
