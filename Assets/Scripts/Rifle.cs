﻿using UnityEngine;

public class Rifle : Weapon
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void OnMouseDown()
    {
        Vector2 target = Cam.Main.ScreenToWorldPoint(Input.mousePosition);
        Fire(target);
    }

    protected override void Fire(Vector2 target)
    {
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;

            base.Fire(target);
            AudioManager.Instance.Play("gunShot");
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
