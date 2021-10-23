public record GetAllBookQuery : IRequest<IEnumerable<GetBookResponse>>, IApiKeyValidation, ICacheable
{
    public string CacheKey => $"{GetType().Name}";
    public bool BypassCache { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }
}

public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQuery, IEnumerable<GetBookResponse>>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public GetAllBookQueryHandler(IMapper mapper, IBookRepository bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<GetBookResponse>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
    {
        var data = await _bookRepository.GetAll();


        return _mapper.Map<IEnumerable<GetBookResponse>>(data);
    }
}