namespace VocabularyApi.IntegrationTests.Builders.Abstractions
{
    public abstract class EntityBuilder<T> : IBuilder<T>
    {
        public abstract T Build();


        public static implicit operator T(EntityBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}
