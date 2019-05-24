using UnityEngine;
using UnityEngine.EventSystems;

public class UICursorBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Texture2D _cursor;

    private Vector2 _cursorHotSpot;

    private void Start()
    {
        _cursorHotSpot = new Vector2(_cursor.width / 2, _cursor.height / 2);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(_cursor, _cursorHotSpot, CursorMode.ForceSoftware);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
