using Hahn.ApplicatonProcess.February2021.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Domain.Repositories
{
    // this interface describes operations that can be performed against the Db
    public interface IAssetRepository
    {
        Task<IEnumerable<Asset>> Get();
        Task<Asset> Get(int id);
        Task<int> Create(Asset asset);
        Task<int> Put(Asset asset);
        Task<int> Delete(int id);
    }
}
