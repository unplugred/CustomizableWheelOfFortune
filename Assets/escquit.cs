using UnityEngine;

public class escquit : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
			Application.Quit();
		if (Input.GetKeyUp(KeyCode.F11))
			Screen.fullScreen = !Screen.fullScreen;
	}
}
