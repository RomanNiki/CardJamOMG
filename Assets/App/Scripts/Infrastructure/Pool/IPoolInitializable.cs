using Cysharp.Threading.Tasks;

namespace App.Scripts.Infrastructure.Pool
{
    public interface IPoolInitializable
    {
        UniTask Initialize(int initialSize = 10);
    }
}