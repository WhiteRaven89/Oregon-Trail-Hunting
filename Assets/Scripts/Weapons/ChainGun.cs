using UnityEngine;

public class ChainGun : Weapon
{
    [SerializeField]
    private GameObject _barrel, _muzzleFlash;

    [SerializeField]
    private Transform _firePoint;

    private Animator _barrelAnimator;

    private int _ammo;

    public int Ammo
    {
        get => _ammo;
        set => _ammo = Mathf.Clamp(value, 0, 999);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _muzzleFlash.SetActive(false);
        _barrel.SetActive(true);
    }

    private void Start()
    {
        _barrelAnimator = _barrel.GetComponent<Animator>();
        _barrelAnimator.speed = 0f;

        Ammo = 999;
    }

    private void OnMouseOver()
    {
        if (GameManager.IsGamePaused)
            return;

        Cursor.visible = false;

        if (Input.GetMouseButton(0) && Ammo != 0)
        {
            Fire();
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
        var mousePos = Cam.GetMouseWorldPosition;
        transform.localPosition = Vector2.right * mousePos.x;
    }

    protected override void Fire()
    {
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;

            base.Fire();

            var hit = Physics2D.Raycast(_firePoint.position, Vector2.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("Animal"));

            if (hit)
            {
                var animal = hit.collider.GetComponent<Animal>();
                animal.Death();
            }

            AudioManager.Instance.Play(Sound.CHAINGUN);
        }

        _muzzleFlash.SetActive(true);
        _barrelAnimator.speed = 1;
        Ammo--;
        
    }

    private void OnMouseExit()
    {
        Cursor.visible = true;
    }

}
