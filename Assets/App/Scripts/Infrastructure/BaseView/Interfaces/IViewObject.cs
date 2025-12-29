using UnityEngine;

namespace App.Scripts.Infrastructure.BaseView.Interfaces
{
    public interface IViewObject
    {
        public void SetParent(Transform container);
        public void SetSize(float size);
        public void Move(Vector2 position);
        public void Rotate(float rotation);
        public void Remove();
    }
}