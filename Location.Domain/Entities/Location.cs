

namespace Location.Domain.Entities
{
    public class Location
    {
        public int locationID { get; set; }
        public string? StreetAddress { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? Country { get; set; }
    }
}
