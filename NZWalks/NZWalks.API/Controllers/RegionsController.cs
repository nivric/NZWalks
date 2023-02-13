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
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await _repository.GetAllAsync();
            var regionDtos = _mapper.Map(regions, typeof(List<Region>), typeof(List<RegionDto>));
           
            return Ok(regionDtos);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await _repository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(AddRegionRequest addRegionRequest)
        {
            var newRegion = new Region
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Latitude = addRegionRequest.Latitude,
                Longitude = addRegionRequest.Longitude,
                Population = addRegionRequest.Population,
                Name = addRegionRequest.Name
            };
            var region = await _repository.AddRegionAsync(newRegion);

            var regionDto = _mapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDto.Id }, regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region = await _repository.DeleteRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] UpdateRegionRequest updateRegionRequest)
        {
            var region = new Region
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Latitude = updateRegionRequest.Latitude,
                Longitude = updateRegionRequest.Longitude,
                Population = updateRegionRequest.Population,
                Name = updateRegionRequest.Name
            };

            var updatedRegion = await _repository.UpdateRegionAsync(id, region);
            if (updatedRegion == null)
            {
                return NotFound();
            }
            var regionDto = _mapper.Map<RegionDto>(updatedRegion);
            return Ok(regionDto);
        }
    }
}
