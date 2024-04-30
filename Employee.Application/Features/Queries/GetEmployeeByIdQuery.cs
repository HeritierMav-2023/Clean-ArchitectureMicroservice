using AutoMapper;
using AutoMapper.QueryableExtensions;
using Employee.Application.Common;
using Employee.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Employee.Application.Features.Queries
{

    public record GetEmployeeByIdQuery : IRequest<GetEmployeeByIdDto>
    {
        public int Id { get; set; }

        public GetEmployeeByIdQuery()
        {

        }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdDto>
    {
        #region Object Reference
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion


        #region Ovveride Methods
        public async Task<GetEmployeeByIdDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Domain.b_Entities.Employee>().GetByIdAsync(request.Id);
            return _mapper.Map<GetEmployeeByIdDto>(entity);
        }
        #endregion

    }
}
