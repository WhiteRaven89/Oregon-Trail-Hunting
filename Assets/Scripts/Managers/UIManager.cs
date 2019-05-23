using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject _menuPanel, _levelPanel, _statsPanel, _settingsPanel, _hud;

    public enum UIState
    {
        MENU,
        LEVEL,
    }

    public UIState GetCurrentState => _menuPanel.activeSelf ? UIState.MENU : UIState.LEVEL;   

    private void Start()
    {
        if (GetCurrentState == UIState.MENU)
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

    public void ActivateLevelPanel()
    {
        _menuPanel.SetActive(false);
        _levelPanel.SetActive(true);

        foreach (Transform child in _levelPanel.transform)
        {
            child.gameObject.SetActive(true);
        }

        if (GameManager.Instance.CurrentLevel.CompareTag("DoomLevel"))
            _hud.SetActive(true);
    }

    public void EndHunting()
    {
        if (PlayerPrefs.GetInt("DisplayStats") == 1)
            _statsPanel.SetActive(true);
        else
            GameManager.Instance.Restart();
    }

}
