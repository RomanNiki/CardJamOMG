using App.Scripts.Infrastructure.BaseView;
using UnityEngine;

namespace App.Scripts.Infrastructure.Factory
{
    public class FactoryView<T> : IFactory<T>
        where T : MonoView
    {
        private readonly T _prefab;
        private readonly Transform _parent;

        public FactoryView(T prefab, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public T Create()
        {
            return Object.Instantiate(_prefab, _parent);
        }
    }
}