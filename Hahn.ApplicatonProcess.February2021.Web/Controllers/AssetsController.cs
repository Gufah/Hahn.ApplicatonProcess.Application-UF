using Hahn.ApplicatonProcess.February2021.Web.Models;
using Hahn.ApplicatonProcess.February2021.Web.Services;
using FluentValidation.Results;
using Hahn.ApplicatonProcess.February2021.Domain.Repsitories;
using Hahn.ApplicatonProcess.February2021.Domain.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.February2021.Domain.Models;

namespace Hahn.ApplicatonProcess.February2021.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IHttpClient _httpClient;
        private readonly IAssetDetailService _assetDetailService;

        public AssetsController(IAssetDetailService assetDetailService, IHttpClient httpClient)
        {
            _httpClient = httpClient;
            _assetDetailService = assetDetailService;
        }

        [HttpGet]
        public async Task<IEnumerable<AssetRequestModel>> GetAssets()
        {
            var successModel = await _assetDetailService.GetAssetDetails();
            return Ok(successModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetRequestModel>> GetAssets(int id)
        {
            return await _assetRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<AssetSuccessResponseModel>> PostAssets([FromBody] AssetRequestModel assetRequestModel)
        {
            AssetValidator assetValidator = new AssetValidator(_httpClient);
            Asset asset = new Asset();
            asset.AssetName = assetRequestModel.AssetName;
            asset.Broken = assetRequestModel.Broken;
            asset.CountryOfDepartment = assetRequestModel.DepartmentCountry;
            asset.Department = assetRequestModel.Department;
            asset.EMailAddress = assetRequestModel.DepartmentEmail;

            ValidationResult validationResult = assetValidator.Validate(asset);
            if (!validationResult.IsValid)
            {
                var assetErrorResponseModel = new BaseReponseModel<object>();
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = validationResult.Errors.Select(x => x.ToString()).ToArray();
                return BadRequest(assetErrorResponseModel);
            }

            var successModel = await _assetDetailService.SaveAssetDetails(asset);
            var successResponseModel = new BaseReponseModel<AssetSuccessResponseModel>();
            successResponseModel.Data.AssetName = successModel.AssetName;

            return CreatedAtRoute("/", successResponseModel);
        }

        [HttpPut]
        public async Task<ActionResult> PutAssets(int id, [FromBody] AssetRequestModel assetRequestModel)
        {
            Asset asset = new Asset();
            asset.AssetName = assetRequestModel.AssetName;
            asset.Broken = assetRequestModel.Broken;
            asset.CountryOfDepartment = assetRequestModel.DepartmentCountry;
            asset.Department = assetRequestModel.Department;
            asset.EMailAddress = assetRequestModel.DepartmentEmail; 

            if (id != asset.Id)
            {
                return BadRequest("The Id field is empty");
            }

            var successModel = await _assetDetailService.SaveAssetDetails(asset);
            return Ok(successModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssets(int id)
        {
            var assetToBeDeleted = await _assetRepository.Get(id);
            if (assetToBeDeleted == null)
            {
                return NotFound();
            }

            await _assetRepository.Delete(assetToBeDeleted.Id);
            return NoContent();
        }
    }
}
