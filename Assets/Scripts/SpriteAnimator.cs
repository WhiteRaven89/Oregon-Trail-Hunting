using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _frames;

    private SpriteRenderer _sprite;

    [SerializeField]
    private float _delay, _rate;

    private int _currentFrame;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        InvokeRepeating("Animate", _delay, _rate);
    }

    private void Animate()
    {
        _sprite.sprite = _frames[_currentFrame++ % _frames.Length];
    }
}
