using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;
using System.Data;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalksRepository _walksRepository;

        public WalksController(IWalksRepository walksRepository, IMapper mapper)
        {
            this._walksRepository = walksRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = await _walksRepository.GetAllAsync();
            var walkDtos = _mapper.Map<List<WalkDto>>(walks);

            return Ok(walkDtos);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await _walksRepository.GetAsync(id);
            if(walk == null)
            {
                return NotFound();
            }
            var walkDto = _mapper.Map<WalkDto>(walk);

            return Ok(walkDto);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkAsync(AddWalkRequest addWalkRequest)
        {
            var newWalk = new Walk
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };
            var walk = await _walksRepository.AddWalksAsync(newWalk);
            var walkDto = _mapper.Map<WalkDto>(walk);
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDto.Id }, walkDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walk = await _walksRepository.DeleteWalksAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDto = _mapper.Map<WalkDto>(walk);
            return Ok(walkDto);

        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,[FromBody] UpdateWalkRequest updateWalkRequest)
        {
            var walk = new Walk
            {
                Length = updateWalkRequest.Length,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
                Name = updateWalkRequest.Name,
            };
            var updatedWalk = await _walksRepository.UpdateWalksAsync(id, walk);
            if(updatedWalk == null)
            {
                return NotFound();
            }
            var walkDto = _mapper.Map<WalkDto>(updatedWalk);
            return Ok(walkDto);
        }
    }
}
