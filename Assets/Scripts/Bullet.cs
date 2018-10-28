using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private void Awake()
    {
        Weapon.OnFire += ShootAt;
    }

    private IEnumerator ShootAt(Vector3 target)
    {
        while (transform.localPosition != target)
        {
            yield return null;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, _speed * Time.deltaTime);
        }        
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Animal")
        {
            Animal animal = other.gameObject.GetComponent<Animal>();
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



