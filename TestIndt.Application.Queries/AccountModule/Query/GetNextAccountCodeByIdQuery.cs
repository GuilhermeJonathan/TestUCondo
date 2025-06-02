using MediatR;
using TestUCondo.Application.CrossCutting.DTO;

namespace TestUCondo.Application.Queries.AccountModule.Query
{
    public class GetNextAccountCodeByIdQuery : IRequest<ResultType>
    {
        public long Id { get; set; }
        public GetNextAccountCodeByIdQuery(long id)
        {
            Id = id;
        }
    }
}