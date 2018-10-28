using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CursorBehavior : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursor;

    private void OnMouseEnter()
    {
        Vector2 cursorHotspot = new Vector2(_cursor.width / 2, _cursor.height / 2);

        Cursor.SetCursor(_cursor, cursorHotspot, CursorMode.ForceSoftware);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
