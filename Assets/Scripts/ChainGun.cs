using UnityEngine;

public class ChainGun : Weapon
{

    [SerializeField]
    private GameObject _barrel, _muzzleFlash;

    private Animator _barrelAnimator;

    private int _ammo;

    public int Ammo
    {
        get
        {
            return _ammo;
        }
        set
        {
            _ammo = Mathf.Clamp(value, 0, 999);
        }
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
        if (GameManager.Instance.GameIsPaused)
            return;

        Cursor.visible = false;

        if (Input.GetMouseButton(0) && Ammo != 0)
        {
            Fire(new Vector2(_firePoint.position.x, 1f));
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
        Vector2 mousePos = Input.mousePosition;

        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.localPosition = new Vector2(mousePos.x, 0f);

    }

    protected override void Fire(Vector3 target)
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
