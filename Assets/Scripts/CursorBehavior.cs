using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CursorBehavior : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursor;

    private Vector2 _cursorHotSpot;

    private void Start()
    {
        _cursorHotSpot = new Vector2(_cursor.width / 2, _cursor.height / 2);
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(_cursor, _cursorHotSpot, CursorMode.ForceSoftware);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
