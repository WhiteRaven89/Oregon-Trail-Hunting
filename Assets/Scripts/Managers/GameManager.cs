using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject _world, _regularGroundSpawner, _doomGroundSpawner, _airSpawner, _rifle, _chainGun, _regularLevels, _doomLevel;

    public GameObject CurrentLevel { get; set; }

    public static bool IsGamePaused => Time.deltaTime == 0f;

    private float _accuracy;

    public int ShotsFired { get; set; }
    public int AnimalsKilled { get; set; }

    public float Accuracy
    {
        get => ShotsFired > 0 ? AnimalsKilled / (float)ShotsFired * 100 : 0;
        set => _accuracy = Mathf.Clamp(value, 0, 100);
    }

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
    private static void DeactivateRecursively(GameObject world)
    {
        world.SetActive(false);

        foreach (Transform child in world.transform)
        {
            child.gameObject.SetActive(false);
            DeactivateRecursively(child.gameObject);
        }
    }

    public void SelectLevel(GameObject level)
    {
        level.SetActive(true);
        level.transform.GetChild(0).gameObject.SetActive(true);
        level.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        CurrentLevel = level;

        _world.SetActive(true);       

        if (level.CompareTag("DoomLevel"))
        {
            _doomLevel.SetActive(true);
            _doomGroundSpawner.SetActive(true);
            _chainGun.SetActive(true);
        }
        else
        {
            _regularLevels.SetActive(true);
            _regularGroundSpawner.SetActive(true);
            _airSpawner.SetActive(true);
            _rifle.SetActive(true);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
