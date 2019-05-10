using System;
using TMPro;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public static event Action OnDisplay;

    private void Start()
    {
        transform.position = GameObject.Find("LevelPanel").transform.position;
        DisplayText();
    }

    private void OnEnable()
    {
        //Activate all children.
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void DisplayText()
    {
        var gm = GameManager.Instance;

        SetText(gm.ShotsFired, "ShotsFired");
        SetText(gm.AnimalsKilled, "AnimalsKilled");
        SetText(gm.Accuracy, "Accuracy", true);

        OnDisplay?.Invoke();
    }

    private void SetText(float stat, string name, bool useFormat = false)
    {
        var text = transform.Find(name).GetComponent<TextMeshProUGUI>();

        if (useFormat)
            text.text = stat.ToString("F0") + "%";
        else
            text.text = stat.ToString();

    }

}
