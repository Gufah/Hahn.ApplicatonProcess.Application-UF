using Hahn.ApplicatonProcess.February2021.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Models
{
    public class AssetContext : DbContext
    {
        public AssetContext (DbContextOptions<AssetContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Asset> Assets { get; set; }
    }
}
