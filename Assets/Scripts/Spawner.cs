using System.Collections;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float _timeToSpawn;

    [SerializeField]
    private float _spawnRate;

    [SerializeField]
    private bool _dynamicScaling;

    [SerializeField]
    private GameObject[] _animals;

    [SerializeField]
    private Transform[] _spawnPoints;

    private void Start()
    {
        InvokeRepeating("Spawn", _timeToSpawn, _spawnRate);
    }

    private void Spawn()
    {
        int randomPosition = Random.Range(0, _spawnPoints.Length);
        int randomAnimal = Random.Range(0, _animals.Length);

        Transform spawnPoint = _spawnPoints[randomPosition];
        GameObject animal = _animals[randomAnimal];
        GameObject poolAnimal = ObjectPool.Find(animal);
        GameObject go = null;

        if (poolAnimal && !poolAnimal.activeSelf)
        {
            go = ObjectPool.Reuse(animal, spawnPoint.position, spawnPoint.rotation);
            ObjectPool.Pool.Remove(animal.name);
        }
        else
        {
            go = Instantiate(animal, spawnPoint.position, spawnPoint.rotation, GameManager.Instance.CurrentLevel.transform);
            go.name = animal.name;

            if (!ObjectPool.Pool.Keys.Contains(go.name))
                ObjectPool.Pool.Add(go.name, go);
        }

        if (_dynamicScaling)
            Scale(randomPosition, go);

        StartCoroutine(Disable(go, 60f));
    }

    private void Scale(int randomPosition, GameObject animal)
    {
        int position = (randomPosition % (_spawnPoints.Length / 2)) + 1;
        float scale = position * .5f;
        animal.transform.localScale = Vector2.one * scale;
    }

    private IEnumerator Disable(GameObject objInScene, float time)
    {
        yield return new WaitForSeconds(time);

        var poolObject = ObjectPool.Find(objInScene);

        if (poolObject)
        {
            //If the object is from the pool, deactivate it so that it can be pooled again.
            if (objInScene == poolObject)
                objInScene.SetActive(false);
            //Otherwise, it's just a clone that can be deleted.
            else
                Destroy(objInScene);
        }
    }

}
