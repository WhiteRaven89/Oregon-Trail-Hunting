using System;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    private int _index, _sceneCount;

    private Transform _currentLevel;

    private GameObject GetCurrentScene(int currentScene) => _currentLevel.GetChild(0).GetChild(currentScene).gameObject;

    public static event Action<int> OnMove;

    private void Start()
    {
        _index = 0;
        _currentLevel = GameManager.Instance.CurrentLevel.transform;
        _sceneCount = _currentLevel.Find("Scenes").childCount;
    }

    public void Move()
    {
       if (GameManager.IsGamePaused)
            return;

        GetCurrentScene(_index).SetActive(false);
        _index = ++_index % _sceneCount;
        GetCurrentScene(_index).SetActive(true);

        OnMove?.Invoke(_index);
    }

}
