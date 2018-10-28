using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private Text _text;

    [SerializeField]
    private ChainGun _chainGun;

    private void Start()
    {
        _text = GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
        //Activates all children.
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        _text.text = _chainGun.Ammo.ToString();
    }

}
