

namespace CleanArchJobs.Domain.Entities
{
    public class Jobs
    {
        public int Id { get; set; }
        public string? ShortTitle { get; set; }
        public string? LongTitle { get; set; }
        public double MinSalary { get; set; }
        public double MaxSalary { get; set; }
    }
}
