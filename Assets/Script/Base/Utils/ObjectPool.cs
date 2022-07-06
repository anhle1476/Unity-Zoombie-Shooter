using System.Collections.Generic;
using UnityEngine;

namespace Script.Base.Utils
{
    public abstract class ObjectPool<T> where T : MonoBehaviour
    {
        protected ICollection<T> _pool = new List<T>();
        protected int? _maxPoolSize;
        protected readonly T _objectPrefab;
        protected readonly Transform _parent;

        public ObjectPool(T objectPrefab, int? maxPoolSize = null, int initialPoolSize = 0, Transform parent = null)
        {
            _objectPrefab = objectPrefab;
            _maxPoolSize = maxPoolSize;
            _parent = parent;

            PopulateInitialPool(initialPoolSize);
        }

        #region virtual methods

        protected virtual T InstantiateNewObject()
        {
            return Object.Instantiate(_objectPrefab, _parent);
        }

        #endregion
        
        private void PopulateInitialPool(int initialPoolSize)
        {
            for (var i = 0; i < initialPoolSize; i++)
            {
                InstantiateNewObjectInPool();
            }
        }

        private T InstantiateNewObjectInPool()
        {
            if (_maxPoolSize.HasValue && _maxPoolSize.Value >= _pool.Count) 
                return null;
            
            T newObject = InstantiateNewObject();
            _pool.Add(newObject);

            newObject.gameObject.SetActive(false);
            
            return newObject;

        }

        public T GetInactiveObjectFromPool()
        {
            foreach (T objInPool in _pool)
            {
                if (!objInPool.gameObject.activeInHierarchy)
                {
                    return objInPool;
                }
            }

            return InstantiateNewObjectInPool();
        }
    }
}