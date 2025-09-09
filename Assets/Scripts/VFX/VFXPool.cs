using System.Collections.Generic;
using UnityEngine;

public class VFXPool
{
    private GameObject prefabToPool;
    private List<PooledObject> pooledObjects = new List<PooledObject>();
    private Transform parentTransform;

    public VFXPool(GameObject prefab, Transform parent)
    {
        this.prefabToPool = prefab;
        this.parentTransform = parent;
    }

    public GameObject GetPooledObject()
    {
        PooledObject pooledObject = pooledObjects.Find(item => !item.isUsed);

        if (pooledObject != null)
        {
            pooledObject.isUsed = true;
            pooledObject.obj.SetActive(true);
            return pooledObject.obj;
        }

        return CreateNewPooledObject();
    }

    public void ReturnToPool(GameObject returnedObject)
    {
        PooledObject pooledObject = pooledObjects.Find(item => item.obj.Equals(returnedObject));
        if (pooledObject != null)
        {
            pooledObject.isUsed = false;
            pooledObject.obj.SetActive(false);
        }
    }

    private GameObject CreateNewPooledObject()
    {
        PooledObject newPooledObject = new PooledObject();
        newPooledObject.obj = GameObject.Instantiate(prefabToPool, parentTransform);
        newPooledObject.isUsed = true;
        pooledObjects.Add(newPooledObject);
        return newPooledObject.obj;
    }

    private class PooledObject
    {
        public GameObject obj;
        public bool isUsed;
    }
}