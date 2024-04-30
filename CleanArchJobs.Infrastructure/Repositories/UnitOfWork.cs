using CleanArchJobs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchJobs.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //1-Déclaration d'objets repository
        public IJobsRepository? JobsRepository { get; }

        //2-Constructeur DI
        public UnitOfWork(IJobsRepository? JobsRepositorie)
        {
            JobsRepository = JobsRepositorie;
        }

    }
}
