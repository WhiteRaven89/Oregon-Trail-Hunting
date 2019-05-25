using UnityEngine;

public class Rifle : Weapon
{

    private void OnMouseDown()
    {
        Fire();
    }

    protected override void Fire()
    {
        base.Fire();

        // Fire rate only applies to audio. 
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            AudioManager.Instance.Play(Sound.GUNSHOT);
        }
    }

}
