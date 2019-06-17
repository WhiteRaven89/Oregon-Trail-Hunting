using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float _timeToSpawn, _spawnRate;

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
        var randomPosition = Random.Range(0, _spawnPoints.Length);
        var randomAnimal = Random.Range(0, _animals.Length);
        var spawnPoint = _spawnPoints[randomPosition];
        var animal = _animals[randomAnimal];
        var go = ObjectPool.GetFromPool(animal, spawnPoint, GameManager.Instance.CurrentLevel.transform);

        if (_dynamicScaling)
            Scale(go, randomPosition);

        StartCoroutine(Disable(go, 60f));
    }

    /// <summary>
    /// Animals will scale based on where they spawn
    /// </summary>
    /// <param name="randomPosition"></param>
    /// <param name="animal"></param>
    private void Scale(GameObject animal, int randomPosition)
    {
        var position = (randomPosition % (_spawnPoints.Length / 2)) + 1;
        var scale = position * .5f;
        animal.transform.localScale = Vector2.one * scale;
    }

    private static IEnumerator Disable(GameObject animal, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.RemoveFromPool(animal);
    }

}
