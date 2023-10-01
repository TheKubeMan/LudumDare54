using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoodSystem.Data
{
    public class FoodPool
    {
        private List<GameObject> _pool = new List<GameObject>();

        public void Add(GameObject gameObject)
        {
            _pool.Add(gameObject);
        }

        public bool Remove(GameObject gameObject)
        {
            return _pool.Remove(gameObject);
        }

        public GameObject GetActive()
        {
            return _pool.FirstOrDefault(obj => !obj.activeSelf);
        }

        public GameObject GetByIndex(int index)
        {
            return index < _pool.Count ? _pool[index] : null;
        }

        public int Count()
        {
            return _pool.Count;
        }
    }
}