using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Rigidbody2D _rb;

    private void Awake()
    {
        Weapon.OnFire += ShootAt;
        _rb = GetComponent<Rigidbody2D>();
    }

    private IEnumerator ShootAt(Vector2 target)
    {
        while ((Vector2)transform.localPosition != target)
        {
            yield return null;
            var position = Vector2.MoveTowards(transform.localPosition, target, _speed * Time.deltaTime);
            _rb.MovePosition(position);
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Animal"))
        {
            var animal = other.GetComponent<Animal>();
            animal.Death();
            GameManager.Instance.AnimalsKilled++;
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        Weapon.OnFire -= ShootAt;
    }

}



