namespace VocabularyApi.IntegrationTests.Builders.Abstractions
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}
