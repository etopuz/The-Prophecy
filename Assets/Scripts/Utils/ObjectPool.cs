using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheProphecy
{
    public class ObjectPool 
    {
        private GameObject _prefab;
        private GameObject _container;

        private Stack<GameObject> _objectPool = new Stack<GameObject>();

        public ObjectPool(GameObject prefab, GameObject container)
        {
            _prefab = prefab;
            _container = container;
        }

        public void FillThePool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject object_ = Object.Instantiate(_prefab);
                object_.transform.parent = _container.transform;
                AddToPool(object_);
            }
        }

        public GameObject GetFromPool()
        {
            if (_objectPool.Count > 0)
            {
                GameObject object_ = _objectPool.Pop();
                object_.gameObject.SetActive(true);

                return object_;
            }

            return Object.Instantiate(_prefab);
        }

        public void AddToPool(GameObject object_)
        {
            object_.gameObject.SetActive(false);
            _objectPool.Push(object_);
        }
    }
}