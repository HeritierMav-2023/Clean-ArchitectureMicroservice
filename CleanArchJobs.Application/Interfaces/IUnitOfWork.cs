
namespace CleanArchJobs.Application.Interfaces
{
    public interface IUnitOfWork
    {
       IJobsRepository JobsRepository { get; }
    }
}
