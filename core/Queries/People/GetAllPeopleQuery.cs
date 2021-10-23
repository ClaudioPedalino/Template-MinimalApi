public record GetAllPeopleQuery : PaginationQuery, IRequest<IEnumerable<GetPeopleResponse>>, IApiKeyValidation, ICacheable
{
    public string CacheKey => $"{GetType().Name}";
    public bool BypassCache { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }
}

public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, IEnumerable<GetPeopleResponse>>
{
    private readonly IMapper _mapper;
    private readonly IPeopleRepository _peopleRepository;

    public GetAllPeopleQueryHandler(IMapper mapper, IPeopleRepository peopleRepository)
    {
        _mapper = mapper;
        _peopleRepository = peopleRepository;
    }

    public async Task<IEnumerable<GetPeopleResponse>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        var data = await _peopleRepository.GetAll();
        var total = data.Count();

        //data = data
        //    .WhereIf(!string.IsNullOrWhiteSpace(request.Nombre), x => x.FirstName.Contains(request.Nombre))
        //    .WhereIf(!string.IsNullOrWhiteSpace(request.Apellido), x => x.LastName.Contains(request.Apellido))
        //    .WhereIf(request.Edad.HasValue && request.Edad > 16 && request.Edad < 120, x => x.Age == request.Edad);

        //var paginatedResponse = data
        //    .Skip((request.PageNumber - 1) * request.PageSize)
        //    .Take(request.PageSize);

        var response = _mapper.Map<IEnumerable<GetPeopleResponse>>(data);

        //var finalResponse = new PaginationResult<GetPeopleResponse>(
        //    total,
        //    request.PageSize,
        //    (total / request.PageSize) + 1,
        //    response);

        return response;
    }
}


public record PaginationQuery
{
    public PaginationQuery()
    {
        PageNumber = 1;
        PageSize = 15;
    }

    public virtual int PageNumber { get; set; }
    public virtual int PageSize { get; set; }
}