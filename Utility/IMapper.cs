namespace Traverse.Utility
{
    public interface IMapper<E, D>
    {
        static abstract E MapTo(D input);
        static abstract D MapFrom(E input);
    }
}