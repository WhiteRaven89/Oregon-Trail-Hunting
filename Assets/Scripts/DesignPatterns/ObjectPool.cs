using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ObjectPool
{
    private static readonly Dictionary<string, GameObject> Pool = new Dictionary<string, GameObject>();

    public static GameObject GetFromPool(GameObject gameObject, Transform transform, Transform parent)
    {
        var objectToPool = Find(gameObject);
        GameObject go;

        if (objectToPool && !objectToPool.activeSelf)
        {
            go = Reuse(gameObject, transform.position, transform.rotation);
            Pool.Remove(gameObject.name);
        }
        else
        {
            go = Object.Instantiate(gameObject, transform.position, transform.rotation, parent);
            go.name = gameObject.name;

            if (!Pool.Keys.Contains(go.name))
                Pool.Add(go.name, go);
        }
        return go;
    }

    public static void RemoveFromPool(GameObject gameObject)
    {
        var poolObject = Find(gameObject);

        if (poolObject)
        {
            // If the object is originally from the pool, deactivate it so that it can be pooled again.
            if (gameObject == poolObject)
                gameObject.SetActive(false);
            // Otherwise, it's just a clone that can be deleted.
            else
                Object.Destroy(gameObject);
        }
    }

    private static GameObject Find(Object go)
    {
        Pool.TryGetValue(go.name, out var result);
        return result;
    }

    private static GameObject Reuse(Object go, Vector3 position, Quaternion rotation)
    {
        var obj = Find(go);

        obj.SetActive(true);
        obj.transform.localPosition = position;
        obj.transform.localRotation = rotation;
        return obj;
    }

}
