using UnityEngine;
using System.Collections.Generic;

namespace DPX.Main
{
    public class VFXService 
    {
        private Dictionary<string, VFXPool> pools = new Dictionary<string, VFXPool>();

        public VFXService(GameObject firePrefab, GameObject hitPointPrefab, Transform parentTransform)
        {
            InitializePools(firePrefab, hitPointPrefab, parentTransform);
        }

        public void InitializePools(GameObject firePrefab, GameObject hitPointPrefab, Transform parentTransform)
        {
            pools.Add("MuzzleFire", new VFXPool(firePrefab, parentTransform));
            pools.Add("HitPoint", new VFXPool(hitPointPrefab, parentTransform));
        }

        public GameObject GetObject(string poolName)
        {
            if (pools.ContainsKey(poolName))
            {
                return pools[poolName].GetPooledObject();
            }
            return null;
        }

        public void ReturnObject(string poolName, GameObject obj)
        {
            if (pools.ContainsKey(poolName))
            {
                pools[poolName].ReturnToPool(obj);
            }
        }
    }
}