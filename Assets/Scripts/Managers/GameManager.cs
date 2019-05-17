using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject _world;

    public GameObject CurrentLevel { get; set; }

    public static bool IsGamePaused => Time.deltaTime == 0f;

    public int ShotsFired { get; set; }
    public int AnimalsKilled { get; set; }

    public float Accuracy => ShotsFired > 0 ? AnimalsKilled / (float)ShotsFired * 100 : 0;

    protected override void Awake()
    {
        base.Awake();

        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (UIManager.Instance.GetCurrentState == UIManager.UIState.MENU)
        {
            DeactivateRecursively(_world);
        }
    }

    /// <summary>
    /// Deactivates the world, as well as everything inside of it (children, grandchildren, etc.)
    /// </summary>
    /// <param name="world"></param>
    private void DeactivateRecursively(GameObject world)
    {
        world.SetActive(false);

        foreach (Transform child in world.transform)
        {
            child.gameObject.SetActive(false);
            DeactivateRecursively(child.gameObject);
        }
    }

    /// <summary>
    /// Registered as an OnClick event whenever the user selects a level with the mouse button
    /// </summary>
    /// <param name="levelChosen"></param>
    public void ChooseLevel(GameObject levelChosen)
    {
        levelChosen.SetActive(true);
        levelChosen.transform.GetChild(0).gameObject.SetActive(true);
        levelChosen.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        CurrentLevel = levelChosen;

        _world.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnLevelWasLoaded()
    {
        ObjectPool.Pool.Clear();
    }

}
