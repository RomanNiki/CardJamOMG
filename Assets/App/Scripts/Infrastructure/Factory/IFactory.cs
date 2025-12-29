namespace App.Scripts.Infrastructure.Factory
{
    public interface IFactory<out T>
    {
        T Create();
    }
    
    public interface IFactory<in TParam, out T>
    {
        T Create(TParam param);
    }
}