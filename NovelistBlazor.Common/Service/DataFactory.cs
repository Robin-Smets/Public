using AutoMapper;

namespace NovelistBlazor.Common.Service
{
    public class DataFactory
    {
        private IMapper _mapper;

        public DataFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public T CreateEntity<T, TDto>(TDto dto) where T : class where TDto : class
        {
            return _mapper.Map<T>(dto);
        }

        public TDto CreateDTO<TDto, T>(T entity) where TDto : class, new() where T : class
        {
            return _mapper.Map<TDto>(entity);
        }

        public IEnumerable<T> CreateEntities<T, TDto>(IEnumerable<TDto> dtos) where T : class where TDto : class
        {
            return _mapper.Map<IEnumerable<T>>(dtos);
        }

        public IEnumerable<TDto> CreateDTOs<TDto, T>(IEnumerable<T> entities) where TDto : class, new() where T : class
        {
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public TDto CopyDTO<TDto>(TDto dto) where TDto : class, new()
        {
            return _mapper.Map<TDto>(dto);
        }

        public IEnumerable<TDto> CopyDTOs<TDto>(IEnumerable<TDto> dtos) where TDto : class, new()
        {
            return dtos.Select(dto => CopyDTO(dto)).ToList();
        }
    }
}
