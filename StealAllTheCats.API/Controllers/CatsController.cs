using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StealAllTheCats.Application.Dtos;
using StealAllTheCats.Core.Entities.Dtos;
using StealAllTheCats.Core.Interfaces;

namespace StealAllTheCats.API.Controllers;

[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[Route("api/[controller]")]
[Produces("application/json")]
public class CatsController(ICatService catService, IMapper mapper) : ControllerBase
{
    [HttpPost("fetch")]
    public async Task<ActionResult<FetchCatsFromApiResponse>> FetchCats(FetchCatsFromApiRequest request)
    {
        await catService.FetchAndStoreCatsAsync(HttpContext.RequestAborted);
        return Ok(new FetchCatsFromApiResponse {CorrelationId = request.CorrelationId});
    }

    [HttpPost("GetCatById")]
    public async Task<ActionResult<GetCatResponse>> GetCatById([FromBody] GetCatByIdRequest request)
    {
        GetCatDataByIdInput input = mapper.Map<GetCatDataByIdInput>(request);
        
        CatDataResultDto result = await catService.GetCatByIdAsync(input, HttpContext.RequestAborted);

        GetCatResponseData data = mapper.Map<GetCatResponseData>(result);

        GetCatResponse response = new() { CorrelationId = request.CorrelationId, Data = data };

        return Ok(response);
    }

    [HttpPost("GetCats")]
    public async Task<ActionResult<GetCatsResponse>> GetCats([FromBody] GetCatsRequest request)
    {
        GetCatsDataInput input = mapper.Map<GetCatsDataInput>(request);
        
        List<CatDataResultDto> result = await catService.GetCatsAsync(input, HttpContext.RequestAborted);

        List<GetCatResponseData> cats = mapper.Map<List<GetCatResponseData>>(result);

        GetCatsResponse response = new() {CorrelationId = request.CorrelationId, Data = cats };

        return Ok(response);
    }
}
