namespace MockDataPatterns.Core.Interfaces
{
    /// <summary>
    /// Builder pattern için temel interface
    /// </summary>
    public interface IBuilder<T>
    {
        T Build();
    }
}
