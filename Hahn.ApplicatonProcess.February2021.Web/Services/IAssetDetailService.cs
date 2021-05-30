using Hahn.ApplicatonProcess.February2021.Domain.Models;
using Hahn.ApplicatonProcess.February2021.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Services
{
    public interface IAssetDetailService
    {
        Task<int> SaveAssetDetails(Asset asset);

        Task<Tuple<int>> UpdateAssetDetails(int id, Asset asset);

        Task<AssetSuccessResponseModel[]> GetAssetDetails();

        Task<AssetSuccessResponseModel> GetAssetDetailsById(int Id);
        Task<Tuple<int>> DeleteAssetDetailsById(int id);
    }
}
