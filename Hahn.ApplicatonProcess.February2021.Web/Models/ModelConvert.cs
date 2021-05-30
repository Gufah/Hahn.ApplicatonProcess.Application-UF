using Hahn.ApplicatonProcess.February2021.Domain.Models;
using Hahn.ApplicatonProcess.February2021.Domain.Repsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Models
{
    public class ModelConvert
    {
        public static Asset ConvAssetRequestToModel(AssetRequestModel assetRequestModel)
        {
            Asset asset = new Asset();
            asset.AssetName = assetRequestModel.AssetName;
            asset.Broken = assetRequestModel.Broken;
            asset.CountryOfDepartment = assetRequestModel.DepartmentCountry;
            asset.Department = assetRequestModel.Department;
            asset.PurchaseDate = assetRequestModel.PurchaseDate;
            asset.EMailAddress = assetRequestModel.DepartmentEmail;

            return asset;
        }

        public static AssetSuccessResponseModel ConvAssetModelToResponse(Asset asset)
        {
            var assetSuccessResponse = new AssetSuccessResponseModel();
            assetSuccessResponse.AssetName = asset.AssetName;
            assetSuccessResponse.CountryOfDepartment = asset.CountryOfDepartment;
            assetSuccessResponse.Department = asset.Department;
            assetSuccessResponse.EMailAddressOfDepartment = asset.EMailAddress;

            return assetSuccessResponse;
        }
    }
}
