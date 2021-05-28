using Hahn.ApplicatonProcess.February2021.Domain.Models;
using Hahn.ApplicatonProcess.February2021.Domain.Repsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Web.Models
{
    public class Convert
    {
        public static Asset ConvAsset(AssetRequestModel assetRequestModel)
        {
            Asset asset = new Asset();
            asset.AssetName = assetRequestModel.AssetName;
            asset.Broken = assetRequestModel.Broken;
            asset.CountryOfDepartment = assetRequestModel.DepartmentCountry;
            asset.Department = assetRequestModel.Department;
            asset.EMailAddress = assetRequestModel.DepartmentEmail;

            return asset;
        }
    }
}
