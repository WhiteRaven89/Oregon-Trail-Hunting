using System;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    private int _index, _sceneCount;

    public static event Action<int> OnMove;

    private void Start()
    {
        _index = 0;
        _sceneCount = GameManager.Instance.CurrentLevel.transform.Find("Scenes").childCount;
    }

    private void OnMouseDown()
    {
        Move();
    }

    private void Move()
    {
       if (GameManager.GameIsPaused)
            return;

        Scene(_index).SetActive(false);
        _index = ++_index % _sceneCount;
        Scene(_index).SetActive(true);

        OnMove?.Invoke(_index);
    }

    private GameObject Scene(int currentScene)
    {
        return GameManager.Instance.CurrentLevel.transform.GetChild(0).GetChild(currentScene).gameObject;
    }
}
