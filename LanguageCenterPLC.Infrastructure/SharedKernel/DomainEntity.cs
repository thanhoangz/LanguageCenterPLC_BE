namespace LanguageCenterPLC.Infrastructure.SharedKernel
{
    public abstract class DomainEntity<T>
    {
        private T id;

        public T Id { get => id; set => id = value; }

        public bool IsTransient()
        {
            return Id.Equals(default(T));
        }
    }
}
