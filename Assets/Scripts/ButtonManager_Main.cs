using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager_Main : MonoBehaviour {

	public GameObject moreInfo, credits;
    private bool showMoreInfo = false;
	private bool showCredits = false;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MoreInfo()
    {
        showMoreInfo = !showMoreInfo;
        moreInfo.SetActive(showMoreInfo);
        credits.SetActive(false);
    }

	public void Credits()
	{
		showCredits = !showCredits;
		credits.SetActive(showCredits);
        moreInfo.SetActive(false);
    }

    public void QuitGame()
    {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
    }
		
}
