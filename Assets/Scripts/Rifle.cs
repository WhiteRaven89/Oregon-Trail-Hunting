using UnityEngine;

public class Rifle : Weapon
{

    private void OnMouseDown()
    {
        var target = Cam.GetMouseWorldPosition;
        Fire(target);
    }

    protected override void Fire(Vector2 target)
    {
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;

            base.Fire(target);
            AudioManager.Instance.Play(Sound.GUN_SHOT);
        }
    }

    protected override void Move(int sceneIndex)
    {
        base.Move(sceneIndex);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

}
