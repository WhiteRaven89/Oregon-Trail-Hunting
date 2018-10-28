using System.Collections;
using System.Linq;
using UnityEngine;

#pragma warning disable 649

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float _spawnDelay;

    private float nextTimeToSpawn;

    [SerializeField]
    private bool _dynamicScaling;

    [SerializeField]
    private GameObject[] _animals;

    [SerializeField]
    private Transform[] _spawnPoints;

    private void Update()
    {
        if (nextTimeToSpawn <= Time.time)
        {
            Spawn();
            nextTimeToSpawn = Time.time + _spawnDelay;
        }
    }

    private void Spawn()
    {
        int randomPosition = Random.Range(0, _spawnPoints.Length);
        int randomAnimal = Random.Range(0, _animals.Length);

        Transform spawnPoint = _spawnPoints[randomPosition];
        GameObject animal = _animals[randomAnimal];
        GameObject go = null;

        var poolAnimal = ObjectPool.Find(animal);

        if (poolAnimal && !poolAnimal.activeSelf)
        {
            go = ObjectPool.Reuse(animal, spawnPoint.position, spawnPoint.rotation);
            ObjectPool.Pool.Remove(animal.name);
        }
        else
        {
            go = Instantiate(animal, spawnPoint.position, spawnPoint.rotation, GameManager.Instance.CurrentLevel.transform);
            go.name = animal.name;
        }

        if (_dynamicScaling)
            Scale(randomPosition, go);

        StartCoroutine(Disable(go, 60f));
    }

    private void Scale(int randomPosition, GameObject animal)
    {
        int position = (randomPosition % (_spawnPoints.Length / 2)) + 1;
        float scale = position * .5f;

        animal.transform.localScale = new Vector2(scale, scale);
    }

    private IEnumerator Disable(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);

        var pool = ObjectPool.Pool;

        if (!pool.Keys.Contains(go.name))
            pool.Add(go.name, go);

        var other = ObjectPool.Find(go);

        if (other)
        {
            if (go != other)
                Destroy(go);
            else
                go.SetActive(false);
        }
    }

}
