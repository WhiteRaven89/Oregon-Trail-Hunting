using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private bool _enableStats, _enableSound;

    private void Start()
    {
        transform.Find("StatsToggle").GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("DisplayStats") == 1;
        transform.Find("SoundToggle").GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("EnableSound") == 1;
    }

    public void ToggleStats()
    {
        _enableStats = !_enableStats;
        SetOption("DisplayStats", _enableStats);
    }

    public void ToggleSound()
    {
        _enableSound = !_enableSound;
        SetOption("EnableSound", _enableSound);

        AudioManager.Instance.gameObject.SetActive(_enableSound);
    }

    private void SetOption(string option, bool enabled)
    {
        PlayerPrefs.SetInt(option, Convert.ToInt32(enabled));
    }

}
