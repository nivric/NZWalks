using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class RegionDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public double Area { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public long Population { get; set; }

        //Navigation Properties
        //Creating circular reference while serializing and also not needed to be exposed

        //public IEnumerable<WalkDto> Walks { get; set; }
    }
}
