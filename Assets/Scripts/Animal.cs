﻿using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject _deathPrefab;

    private void Update()
    {
        transform.Translate(Vector2.right * (_speed * Time.deltaTime));
    }

    private void OnMouseDown()
    {
        Death();
       
    }

    public void Death()
    {
        var go = Instantiate(_deathPrefab, transform.localPosition, transform.localRotation, transform.parent);
        go.transform.localScale = transform.localScale;

        GameManager.Instance.AnimalsKilled++;
        AudioManager.Instance.Play(Sound.ANIMAL_HIT);

        gameObject.SetActive(false);
    }

}
