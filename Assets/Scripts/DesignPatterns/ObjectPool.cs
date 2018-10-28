using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool
{
    public static Dictionary<string, GameObject> Pool = new Dictionary<string, GameObject>();

    public static GameObject Find(GameObject go)
    {
        GameObject result = null;
        Pool.TryGetValue(go.name, out result);
        return result;
    }

    public static GameObject Reuse(GameObject go, Vector3 position, Quaternion rotation)
    {      
        GameObject obj = Find(go);

        obj.SetActive(true);
        obj.transform.localPosition = position;
        obj.transform.localRotation = rotation;
        return obj;
    }

}
