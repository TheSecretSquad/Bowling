namespace Bowling
{
    public interface IPrinter<SourceType>
        where SourceType : class
    {
        void BeginPrint(SourceType source);

        void EndPrint(SourceType source);
    }
}
