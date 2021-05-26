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
        Task<Asset> Post(Asset asset);
        Task Put(Asset asset);
        Task Delete(int id);
    }
}
