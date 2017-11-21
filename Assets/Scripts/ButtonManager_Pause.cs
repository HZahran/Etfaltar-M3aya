using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager_Pause : MonoBehaviour {

	public GameObject pausecanvas;

	public void QuitToMainMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Main Menu");
	}

	public void Resume()
	{
		Time.timeScale = 1;
		pausecanvas.SetActive(false);
	}

}
