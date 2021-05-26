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
        Task<AssetSuccessResponseModel> SaveAssetDetails(Asset asset);

        Task<AssetSuccessResponseModel> UpdateAssetDetails(Asset asset);

        Task<AssetSuccessResponseModel[]> GetAssetDetails();

        Task<AssetSuccessResponseModel> GetAssetDetailsById(int Id);
    }
}
