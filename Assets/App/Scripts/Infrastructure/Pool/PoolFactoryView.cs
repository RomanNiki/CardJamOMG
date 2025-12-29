using App.Scripts.Infrastructure.BaseView;
using App.Scripts.Infrastructure.Factory;
using App.Scripts.Infrastructure.Pool.Extensions;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace App.Scripts.Infrastructure.Pool
{
    public class PoolFactoryView<T> : IFactory<T>, IPoolInitializable
    where T : MonoView
    {
        private readonly IFactory<T> _factory;
        private readonly Transform _parent;
        private readonly ObjectPool<MonoView> _pool;

        public PoolFactoryView(FactoryView<T> factory, Transform parent)
        {
            _factory = factory;
            _parent = parent;
            _pool = new ObjectPool<MonoView>(OnCreate, OnGet, OnRelease, OnDestroy);
        }

        private void OnRelease(MonoView obj)
        {
            obj.gameObject.SetActive(false);
            obj.SetParent(_parent);
        }

        private static void OnGet(MonoView obj)
        {
            obj.gameObject.SetActive(true);
        }

        public T Create()
        {
            return _pool.Get().GetComponent<T>();
        }

        private T OnCreate()
        {
            var item = _factory.Create();
            item.gameObject.AddComponent<ReturnToPoolOnRemoveBase>().Initialize(item, _pool);
            return item;
        }

        private static void OnDestroy(MonoView obj)
        {
            Object.Destroy(obj.gameObject);
        }

        public async UniTask Initialize(int initialSize = 10)
        {
            if (_pool.CountAll >= initialSize)
            {
                return;
            }
            
            var list = new FastList<MonoView>(initialSize);
            for (var i = 0; i < initialSize; i++)
            {
                var item = _pool.Get();
                list.Add(item);
                await UniTask.Yield();
            }

            foreach (var view in list)
            {
                _pool.Release(view);
                await UniTask.Yield();
            }
        }
    }
}