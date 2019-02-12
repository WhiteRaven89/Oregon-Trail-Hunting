using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private GameObject _world;

    public GameObject CurrentLevel { get; set; }

    public static bool GameIsPaused { get { return Time.deltaTime == 0f; } }

    public int ShotsFired { get; set; }
    public int AnimalsKilled { get; set; }

    public float Accuracy { get { return ShotsFired > 0 ? AnimalsKilled / (float)ShotsFired * 100 : 0; } }

    protected override void Awake()
    {
        base.Awake();

        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (UIManager.Instance.CurrentState == UIManager.UIState.MENU)
        {
            DeactivateRecursively(_world);
        }
    }

    //Deactivates the world, as well as everything inside of it (children, grandchildren, etc.)
    private void DeactivateRecursively(GameObject world)
    {
        world.SetActive(false);

        foreach (Transform child in world.transform)
        {
            child.gameObject.SetActive(false);
            DeactivateRecursively(child.gameObject);
        }
    }

    //Gets called when the user selects a level.
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
