﻿using UnityEngine;

public class ChainGun : Weapon
{
    [SerializeField]
    private GameObject _barrel, _muzzleFlash;

    private Animator _barrelAnimator;

    private int _ammo;

    public int Ammo
    {
        get => _ammo;
        set => _ammo = Mathf.Clamp(value, 0, 999);
    }

    private void Start()
    {
        _barrelAnimator = _barrel.GetComponent<Animator>();
        _barrelAnimator.speed = 0f;

        Ammo = 999;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _muzzleFlash.SetActive(false);
        _barrel.SetActive(true);

    }

    private void OnMouseOver()
    {
        if (GameManager.GameIsPaused)
            return;

        Cursor.visible = false;

        if (Input.GetMouseButton(0) && Ammo != 0)
        {
            Fire(new Vector2(_firePoint.position.x, .75f));
        }
        else
        {
            _muzzleFlash.SetActive(false);
            _barrelAnimator.speed = 0;
        }

        FollowMouse();
    }

    private void FollowMouse()
    {
        Vector2 mousePos = Cam.Main.ScreenToWorldPoint(Input.mousePosition);
        transform.localPosition = Vector2.right * mousePos.x;
    }

    protected override void Fire(Vector2 target)
    {
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;

            base.Fire(target);

            _muzzleFlash.SetActive(true);
            _barrelAnimator.speed = 1;
            Ammo--;

            AudioManager.Instance.Play("chainGun");
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
   
    private void OnMouseExit()
    {
        Cursor.visible = true;
    }
}
