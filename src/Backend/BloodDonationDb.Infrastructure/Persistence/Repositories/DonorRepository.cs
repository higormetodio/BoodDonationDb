﻿using BloodDonationDb.Domain.Entities;
using BloodDonationDb.Domain.Repositories.Donor;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationDb.Infrastructure.Persistence.Repositories;
public class DonorRepository : IDonorWriteOnlyRepository, IDonorReadOnlyRepository, IDonorUpdateOnlyRepository
{
    private readonly BloodDonationDbContext _dbContext;

    public DonorRepository(BloodDonationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddDonorAsync(Donor donor) => await _dbContext.Donors.AddAsync(donor);

    public async Task<Donor> GetDonorByEmailAsync(string email)
        => await _dbContext.Donors
        .AsNoTracking()
        .SingleOrDefaultAsync(donor => donor.Email!.Equals(email) && donor.Active);

    public async Task<Donor> GetDonorDonationsByEmailAsync(string email)
        => await _dbContext.Donors
        .AsNoTracking()
        .Include(donor => donor.Donations)
        .SingleOrDefaultAsync(donor => donor.Email!.Equals(email) && donor.Active);

    public async Task<bool> ExistActiveDonorWithEmail(string email) 
        => await _dbContext.Donors.AnyAsync(donor => donor.Email!.Equals(email) && donor.Active);

    public void UpdateDonor(Donor donor)
        => _dbContext.Donors.Update(donor);
}
