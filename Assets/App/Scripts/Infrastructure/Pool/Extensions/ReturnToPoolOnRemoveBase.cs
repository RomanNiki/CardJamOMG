using App.Scripts.Infrastructure.BaseView;
using UnityEngine;
using UnityEngine.Pool;

namespace App.Scripts.Infrastructure.Pool.Extensions
{
    public class ReturnToPoolOnRemoveBase : MonoBehaviour
    {
        private MonoView _view;
        private ObjectPool<MonoView> _pool;

        public void Initialize(MonoView view, ObjectPool<MonoView> pool)
        {
            _view = view;
            _pool = pool;
            _view.Removed += OnRemoved;
        }

        private void OnEnable()
        {
            if (_view != null)
            {
                _view.Removed += OnRemoved;
            }
        }

        private void OnRemoved()
        {
            _pool.Release(_view);
        }

        private void OnDisable()
        {
            if (_view != null )
            {
                _view.Removed -= OnRemoved;
            }
        }
    }
}