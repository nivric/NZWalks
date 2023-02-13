using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
     public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _repository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _repository.GetAllAsync();
            var regionDtos = _mapper.Map(regions, typeof(List<Region>), typeof(List<RegionDto>));
           
            return Ok(regionDtos);
        }
     }
}
