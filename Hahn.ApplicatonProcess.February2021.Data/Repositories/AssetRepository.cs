using Hahn.ApplicatonProcess.February2021.Data.Models;
using Hahn.ApplicatonProcess.February2021.Domain.Models;
using Hahn.ApplicatonProcess.February2021.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetContext _context;
        public AssetRepository(AssetContext context)
        {
            _context = context;
        }

        public async Task<int> Delete(int id)
        {
            var assetToBeDeleted = await _context.Assets.FindAsync(id);
            _context.Assets.Remove(assetToBeDeleted);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Asset>> Get()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task<Asset> Get(int id)
        {
            return await _context.Assets.FindAsync(id);
        }

        public Task<int> Create(Asset asset)
        {
            _context.Assets.Add(asset);
            return _context.SaveChangesAsync();
        }

        public Task<int> Put(Asset asset)
        {
            _context.Entry(asset).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
