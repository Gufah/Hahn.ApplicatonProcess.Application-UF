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
        public async Task<IEnumerable<AssetSuccessResponseModel>> GetAssets()
        {
            var successModel = await _assetDetailService.GetAssetDetails();
            return (IEnumerable<AssetSuccessResponseModel>)Ok(successModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetRequestModel>> GetAssets(int id)
        {
            var successModel = await _assetDetailService.GetAssetDetailsById(id);
            return Ok(successModel);
        }

        [HttpPost]
        public async Task<ActionResult<AssetSuccessResponseModel>> PostAssets([FromBody] AssetRequestModel assetRequestModel)
        {
            AssetValidator assetValidator = new AssetValidator(_httpClient);
            //Asset asset = new Asset();
            var asset = Convert.ConvAsset(assetRequestModel);
            
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
            var asset = Convert.ConvAsset(assetRequestModel);

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
            var assetToBeDeleted = await _assetDetailService.GetAssetDetailsById(id);
            if (assetToBeDeleted == null)
            {
                return NotFound();
            }

            await _assetDetailService.SaveAssetDetails(assetToBeDeleted.Id);
            return NoContent();
        }
    }
}
