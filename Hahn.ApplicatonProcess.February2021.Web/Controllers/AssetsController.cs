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
using System;

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
            var asset = ModelConvert.ConvAssetRequestToModel(assetRequestModel);
            
            ValidationResult validationResult = assetValidator.Validate(asset);
            var assetErrorResponseModel = new BaseReponseModel<object>();
            var successResponseModel = new BaseReponseModel<object>();
            if (!validationResult.IsValid)
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = validationResult.Errors.Select(x => x.ToString()).ToArray();
                return BadRequest(assetErrorResponseModel);
            }

            var successModel = await _assetDetailService.SaveAssetDetails(asset);
            if (successModel > 0)
            {
                dynamic responseMessage = new { message = "Asset details saved successfully" };
                successResponseModel.Data = responseMessage;
            } else
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "Asset details could not be save because of an error from our end, please try again" };
                return StatusCode(StatusCodes.Status500InternalServerError, assetErrorResponseModel);
            }

            return CreatedAtRoute("/", successResponseModel);
        }

        [HttpPut]
        public async Task<ActionResult> PutAssets([FromRoute]int id, [FromBody] AssetRequestModel assetRequestModel)
        {
            var asset = ModelConvert.ConvAssetRequestToModel(assetRequestModel);
            var assetErrorResponseModel = new BaseReponseModel<object>();
            var successResponseModel = new BaseReponseModel<object>();
            if (id == null)
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "Please provide the Id" };
                return BadRequest(assetErrorResponseModel);
            }

            var result = await _assetDetailService.UpdateAssetDetails(id, asset);
            if (result == null)
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "A record with the id you specified does not exist" };
                return NotFound(assetErrorResponseModel);
            }

            if (result.Item1 > 0)
            {
                dynamic responseMessage = new { message = "Asset details saved successfully" };
                successResponseModel.Data = responseMessage;
            }
            else
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "Asset details could not be updated because of an error from our end, please try again" };
                return StatusCode(StatusCodes.Status500InternalServerError, assetErrorResponseModel);
            }
            return Ok(successResponseModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssets(int id)
        {
            var result = await _assetDetailService.DeleteAssetDetailsById(id);
            var assetErrorResponseModel = new BaseReponseModel<object>();
            var successResponseModel = new BaseReponseModel<object>();
            if (result == null)
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "A record with the id you specified does not exist" };
                return NotFound(assetErrorResponseModel);
            }

            if (result.Item1 > 0)
            {
                dynamic responseMessage = new { message = "Asset details deleted successfully" };
                successResponseModel.Data = responseMessage;
            }
            else
            {
                assetErrorResponseModel.Success = false;
                assetErrorResponseModel.Errors = new string[] { "Asset details could not be deleted because of an error from our end, please try again" };
                return StatusCode(StatusCodes.Status500InternalServerError, assetErrorResponseModel);
            }
            return Ok(successResponseModel);
        }
    }
}
