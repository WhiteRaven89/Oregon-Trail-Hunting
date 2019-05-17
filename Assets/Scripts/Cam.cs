using UnityEngine;

public class Cam : MonoBehaviour
{
    /// <summary>
    /// Cached gloabl reference to the main camera
    /// </summary>
    public static Camera Main;

    public static Vector2 GetMouseWorldPosition => Main.ScreenToWorldPoint(Input.mousePosition);

    private void Awake()
    {
        Screen.SetResolution(640, 440, false);
        MoveButton.OnMove += Move;
        Main = Camera.main;
    }

    private void OnDestroy()
    {
        MoveButton.OnMove -= Move;
    }

    private void Move(int sceneIndex)
    {
        var scene = GameManager.Instance.CurrentLevel.transform.GetChild(0).GetChild(sceneIndex).transform;
        transform.position = new Vector3(scene.position.x, transform.position.y, transform.position.z);
    }

}
