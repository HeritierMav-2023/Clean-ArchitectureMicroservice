using AutoMapper;
using AutoMapper.QueryableExtensions;
using Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Employee.Application.Features.Queries
{
    public record GetAllEmployeesQuery : IRequest<List<GetAllEmployeesDto>>;

    internal class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<GetAllEmployeesDto>>
    {
        #region Object Reference
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Implementation Methods
        public async Task<List<GetAllEmployeesDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Domain.b_Entities.Employee>().Entities
               .ProjectTo<GetAllEmployeesDto>(_mapper.ConfigurationProvider)
               .ToListAsync(cancellationToken);
        }
        #endregion
    }
}
