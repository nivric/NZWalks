﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        public Guid WalkDifficultyId { get; set; }

        public Guid RegionId { get; set; }

        //Navigation Properties

        public RegionDto Region { get; set; }

        public WalkDifficultyDto WalkDifficulty { get; set; }
    }
}
