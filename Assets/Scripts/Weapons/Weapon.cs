using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected float _fireRate;

    protected float _nextTimeToFire = 0f;

    protected virtual void OnEnable()
    {
        MoveButton.OnMove += Move;
        Statistics.OnDisplay += Disable;
    }

    protected virtual void Fire()
    {
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
