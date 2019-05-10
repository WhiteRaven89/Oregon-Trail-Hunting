using UnityEngine;

public class Cam : MonoBehaviour
{
    /// <summary>
    /// Cached gloabl reference to the main camera
    /// </summary>
    public static Camera Main { get; private set; }

    private void Awake()
    {
        Screen.SetResolution(640, 440, false);
        MoveButton.OnMove += Move;
        Main = Camera.main;
    }

    private void Move(int sceneIndex)
    {
        Transform scene = GameManager.Instance.CurrentLevel.transform.GetChild(0).GetChild(sceneIndex).transform;
        transform.position = new Vector3(scene.position.x, transform.position.y, transform.position.z);
    }

    private void OnDestroy()
    {
        MoveButton.OnMove -= Move;
    }

}
