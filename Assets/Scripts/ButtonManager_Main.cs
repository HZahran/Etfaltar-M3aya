using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager_Main : MonoBehaviour {

	public GameObject moreInfo, credits;
    private bool showMoreInfo = false;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MoreInfo()
    {
        showMoreInfo = !showMoreInfo;
        moreInfo.SetActive(showMoreInfo);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
		
}
