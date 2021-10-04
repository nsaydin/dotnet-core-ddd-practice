namespace Application.Mapping
{
    public interface IMapper 
    {
        TDestination Map<TDestination>(object source);
    }
}