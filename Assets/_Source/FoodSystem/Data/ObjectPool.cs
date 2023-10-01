using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> _pool = new List<GameObject>();

    public void Add(GameObject gameObject)
    {
        _pool.Add(gameObject);
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