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

    public void Death()
    {
        GameObject go = Instantiate(_deathPrefab, transform.localPosition, transform.localRotation, transform.parent);
        go.transform.localScale = transform.localScale;

        AudioManager.Instance.Play("animalHit");

        gameObject.SetActive(false);
    }

}
