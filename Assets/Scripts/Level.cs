using UnityEngine;

public class Level : MonoBehaviour
{

	private void OnEnable ()
    {
        GameManager.Instance.CurrentLevel = gameObject;
	}	

}
