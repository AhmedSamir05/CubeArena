using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{

    // Dictionary using prefab refence as Key and a Queue of GameObjects as Value
    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

 
    /// <summary>
    /// Gets an object from the pool. If the pool doesn't exist, it creates it.
    /// </summary>
    /// 
    public GameObject Instantiate(GameObject prefab , Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            InitializePool(prefab, 1);
        }

        if (poolDictionary[prefab].Count == 0)
            CreateNewObject(prefab);

        GameObject obj = poolDictionary[prefab].Dequeue();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }
    public GameObject GetObject(GameObject prefab, Vector3 position = new Vector3())
    {
        return Instantiate(prefab, position, Quaternion.identity);
    }

    /// <summary>
    /// Returns an object to the pool and deactivates it.
    /// </summary>
    public void Destroy(GameObject instance, GameObject prefab)
    {
        instance.SetActive(false);

        if (poolDictionary.ContainsKey(prefab))
        {
            poolDictionary[prefab].Enqueue(instance);
        }
        else
        {
            // Fallback: If for some reason the pool was destroyed, just destroy the object
            Debug.LogWarning("Pool for ID " + prefab + " does not exist. Destroying object.");
            Destroy(instance);
        }
    }

    // initializes the pool if it is the first time to see the prefab
    public void InitializePool(GameObject prefab, int count)
    {
        // In case we had the prefab type before
        if (poolDictionary.ContainsKey(prefab))
            return;

        poolDictionary.Add(prefab, new Queue<GameObject>());

        for (int i = 0; i < count; i++)
        {
            CreateNewObject(prefab);
        }
    }

    private void CreateNewObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        poolDictionary[prefab].Enqueue(obj);

        PoolEnqueue poolEnqueue = obj.GetComponent<PoolEnqueue>();
        if(poolEnqueue == null)
        {
            poolEnqueue = obj.AddComponent<PoolEnqueue>();
        }
        poolEnqueue.parentPrefab= prefab;
    }
}