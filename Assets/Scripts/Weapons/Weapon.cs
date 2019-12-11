using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject _bullet;

    protected Transform _firePoint;

    public static event Func<Vector2, IEnumerator> OnFire;

    [SerializeField]
    protected float _fireRate;

    protected float _nextTimeToFire = 0f;

    protected virtual void OnEnable()
    {
        MoveButton.OnMove += Move;
        Statistics.OnDisplay += Disable;

        _firePoint = transform.Find("FirePoint");
        _firePoint.localPosition = Vector2.up * -1.25f;
    }

    protected virtual void Fire(Vector2 target)
    {
        ObjectPool.GetFromPool(_bullet, _firePoint, GameManager.Instance.CurrentLevel.transform);

        if (OnFire != null)
        {
            StartCoroutine(OnFire(target));
        }

        GameManager.Instance.ShotsFired++;
    }

    private void Move(int sceneIndex)
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
