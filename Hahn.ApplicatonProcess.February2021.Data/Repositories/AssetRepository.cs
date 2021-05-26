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

        public async Task Delete(int id)
        {
            var assetToBeDeleted = await _context.Assets.FindAsync(id);
            _context.Assets.Remove(assetToBeDeleted);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Asset>> Get()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task<Asset> Get(int id)
        {
            return await _context.Assets.FindAsync(id);
        }

        public async Task<Asset> Post(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            return asset;
        }

        public async Task Put(Asset asset)
        {
            _context.Entry(asset).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
