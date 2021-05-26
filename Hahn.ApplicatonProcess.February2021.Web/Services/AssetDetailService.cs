using Hahn.ApplicatonProcess.February2021.Domain.Models;
using Hahn.ApplicatonProcess.February2021.Domain.Repositories;
using Hahn.ApplicatonProcess.February2021.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Services
{
    public class AssetDetailService : IAssetDetailService
    {
        private readonly IAssetRepository _assetRepository;
        public AssetDetailService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<AssetSuccessResponseModel[]> GetAssetDetails()
        {
            var assetDetails = await _assetRepository.Get();

            throw new NotImplementedException();
        }

        public async Task<AssetSuccessResponseModel> GetAssetDetailsById(int id)
        {
            var asset = await _assetRepository.Get(id);
            throw new NotImplementedException();
        }

        public async Task<AssetSuccessResponseModel> SaveAssetDetails(Asset asset)
        {
            var assetDetail = await _assetRepository.Post(asset);
            return null;
        }

        public async Task<AssetSuccessResponseModel> UpdateAssetDetails(Asset asset)
        {
            await _assetRepository.Put(asset);
            return null;
        }
    }
}
