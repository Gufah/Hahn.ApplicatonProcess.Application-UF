using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    // this interface describes operations that can be performed against the Db
    public interface IAssetRepository
    {
        Task<IEnumerable<AssetRequestModel>> Get();
        Task<AssetRequestModel> Get(int id);
        Task<AssetRequestModel> Post(AssetRequestModel asset);
        Task Put(AssetRequestModel asset);
        Task Delete(int id);
    }
}
