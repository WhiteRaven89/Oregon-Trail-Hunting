using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject _bullet;

    [SerializeField]
    protected Transform _firePoint;

    public static event Func<Vector2, IEnumerator> OnFire;

    [SerializeField]
    protected float _fireRate;

    protected float _nextTimeToFire = 0f;

    protected virtual void OnEnable()
    {
        MoveButton.OnMove += Move;
        Statistics.OnDisplay += Disable;

        _firePoint.gameObject.SetActive(true);
        _firePoint.localPosition = Vector2.up * -1.25f;
    }
    
    protected virtual void Fire(Vector2 target)
    {
        GameObject go = null;

        var poolObject = ObjectPool.Find(_bullet);

        if (poolObject && !poolObject.activeSelf)
        {
            go = ObjectPool.Reuse(_bullet, _firePoint.position, _firePoint.rotation);
        }
        else
        {
            go = Instantiate(_bullet, _firePoint.position, _firePoint.rotation, GameManager.Instance.CurrentLevel.transform);
            go.name = _bullet.name;

            if (!ObjectPool.Pool.Keys.Contains(go.name))
                ObjectPool.Pool.Add(go.name, go);
        }

        if (OnFire != null)
        {
            StartCoroutine(OnFire(target));
        }

        GameManager.Instance.ShotsFired++;
    }
    
    protected virtual void Move(int sceneIndex)
    {
        var scene = GameManager.Instance.CurrentLevel.transform.GetChild(0).GetChild(sceneIndex).transform;
        transform.localPosition = new Vector2(scene.localPosition.x, transform.localPosition.y);
    }

    protected virtual void OnDisable()
    {
        Statistics.OnDisplay -= Disable;
        MoveButton.OnMove -= Move;
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

}
