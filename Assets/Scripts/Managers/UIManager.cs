using UnityEngine;

public class UIManager : Singleton<UIManager>
{

    [SerializeField]
    private GameObject _menuPanel, _levelPanel, _statsPanel, _settingsPanel, _hud;

    protected override void Awake()
    {
        base.Awake();

    }

    public enum UIState
    {
        MENU,
        LEVEL,
    }

    public UIState CurrentState
    {
        get
        {
            return _menuPanel.activeSelf ? UIState.MENU : UIState.LEVEL;
        }
    }

    private void Start()
    {
        if (CurrentState == UIState.MENU)
        {
            foreach (Transform child in _menuPanel.transform)
            {
                child.gameObject.SetActive(true);
            }

            _levelPanel.SetActive(false);
            _statsPanel.SetActive(false);
            _settingsPanel.SetActive(false);
            _hud.SetActive(false);
        }

    }

    //Gets called when the user selects a level.
    public void ActivateLevelPanel()
    {
        _levelPanel.SetActive(true);

        foreach (Transform child in _levelPanel.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void EndHunting()
    {
        if (PlayerPrefs.GetInt("DisplayStats") == 1)
            _statsPanel.SetActive(true);
        else
            GameManager.Instance.Restart();
    }

}
