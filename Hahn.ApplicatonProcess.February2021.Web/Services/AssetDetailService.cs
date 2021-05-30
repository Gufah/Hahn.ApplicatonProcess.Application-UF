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

        public async Task<Tuple<int>> DeleteAssetDetailsById(int id)
        {
            var assetFromDb = await _assetRepository.Get(id);
            if (assetFromDb == null)
            {
                return null;
            }
            return new Tuple<int>(await _assetRepository.Delete(id));
        }

        public async Task<AssetSuccessResponseModel[]> GetAssetDetails()
        {
            var assetDetails = await _assetRepository.Get();
            return assetDetails.Select(x => ModelConvert.ConvAssetModelToResponse(x)).ToArray();
        }

        public async Task<AssetSuccessResponseModel> GetAssetDetailsById(int id)
        {
            var asset = await _assetRepository.Get(id);
            return ModelConvert.ConvAssetModelToResponse(asset);
        }

        public Task<int> SaveAssetDetails(Asset asset)
        {
            return _assetRepository.Create(asset);
        }

        public async Task<Tuple<int>> UpdateAssetDetails(int id, Asset asset)
        {
            var assetFromDb = await _assetRepository.Get(id);
            if (assetFromDb == null)
            {
                return null;
            }
            return new Tuple<int>(await _assetRepository.Put(asset));
        }
    }
}
