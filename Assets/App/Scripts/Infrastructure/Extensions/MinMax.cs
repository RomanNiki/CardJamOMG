using System;
using UnityEngine;

namespace App.Scripts.Infrastructure.Extensions
{
    [Serializable]
    public class MinMax<T>
    {
        [field: SerializeField] public T Min { get; set; }
        [field: SerializeField] public T Max { get; set; }
    }
}