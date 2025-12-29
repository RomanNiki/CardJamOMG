using System;
using App.Scripts.Infrastructure.BaseView.Interfaces;
using UnityEngine;

namespace App.Scripts.Infrastructure.BaseView
{
    public class MonoView : MonoBehaviour, IViewObject
    {
        [SerializeField] private Transform _root;
    
        public Transform Transform => _root;
        public event Action Removed;
        public event Action Initialized;
        
        public Vector3 localPosition
        {
            get => transform.localPosition;
            set => transform.localPosition = value;
        }

        public Vector3 localScale
        {
            get => Transform.localScale;

            set => Transform.localScale = value;
        }

        private void Awake()
        {
            _root ??= GetComponent<Transform>();
        }

        public void Initialize()
        {
            Initialized?.Invoke();
        }

        public void SetParent(Transform container)
        {
            transform.SetParent(container);
        }
        
        public void SetSize(float size)
        {
            localScale = new Vector3(size, size, 1f);
        }
        
        public void Move(Vector2 position)
        {
            Transform.position = position;
        } 
        
        public void Rotate(float rotation)
        {
            Transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        }
        
        public void Remove()
        {
            SetSize(1f);
            Removed?.Invoke();
        }
    }
}