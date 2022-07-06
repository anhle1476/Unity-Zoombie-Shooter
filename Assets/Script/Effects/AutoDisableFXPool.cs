using Script.Base.Utils;
using UnityEngine;

namespace Script.Effects
{
    public class AutoDisableFXPool : ObjectPool<AutoDisableFX>
    {
        public AutoDisableFXPool(AutoDisableFX objectPrefab, Transform parent = null) 
            : base(objectPrefab, initialPoolSize: 5, parent: parent)
        {
        }
    }
}