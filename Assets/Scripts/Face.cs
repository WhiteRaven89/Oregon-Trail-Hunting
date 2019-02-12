using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _faces;

    private SpriteRenderer _sprite;

    private int _index;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        InvokeRepeating("Animate", 1f, 1f);
    }

    private void Animate()
    {
        _sprite.sprite = _faces[_index++ % _faces.Length];
    }
}
