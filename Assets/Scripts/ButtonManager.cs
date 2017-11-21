using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public GameObject moreInfo,credits;
    private bool showMoreInfo = false;
	void Awake()
    {
        DontDestroyOnLoad(this);
    }

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
