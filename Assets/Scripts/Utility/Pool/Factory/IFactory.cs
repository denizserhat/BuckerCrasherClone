namespace Utility.Pool.Factory
{
	public interface IFactory<T>
	{
		T Create();
	}
}