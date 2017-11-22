using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerControls : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject lightningBolt;

    static GameObject weaponOne;
    static GameObject weaponTwo;
    static GameObject weaponThree;

	public AudioSource sounds;
	AudioSource backgroundSound;
	AudioSource thunderSound;
	AudioSource laserSound;
	AudioSource pistolSound;

	public ParticleSystem pistolMuzzleFlash;
	public ParticleSystem rifleMuzzleFlash;

	static int impurityPercentage;
	static int purityPercentage;
	static float total;
	static float killed;
	static float missed;

    public float damage;
    public Camera fpsCam;

	public Text impurityPercentageText;
	static Text staticImpurityPercentageText;
	public Text percentageText;
	public GameObject percentagecanvas;
	public GameObject pausecanvas;
	public bool paused;

    void Start()
    {
        weaponOne = weapon1;
        weaponTwo = weapon2;
        weaponThree = weapon3;

		backgroundSound = sounds.GetComponents<AudioSource> () [0];
		thunderSound = sounds.GetComponents<AudioSource> () [1];
		laserSound = sounds.GetComponents<AudioSource> () [2];
		pistolSound = sounds.GetComponents<AudioSource> () [3];

		backgroundSound.Play ();

		impurityPercentage = 0;
        purityPercentage = 0;
		killed = 0;
		missed = 0;
		total = 10;

		staticImpurityPercentageText = impurityPercentageText;
		staticImpurityPercentageText.text = "Percentage of Missed: " + impurityPercentage + " %";
		percentageText.text = "Purity Percentage: " + purityPercentage +" %";

        damage = 10f;
		paused = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            SwitchWeapons();
        }

		if (Input.GetKeyDown(KeyCode.Mouse0) && !paused)
        {
            Shoot();

            if (weapon3.activeInHierarchy) {
                StartCoroutine(FireLightningBolt());
            }
        }

		if (Input.GetKeyDown (KeyCode.P)) {

			if (pausecanvas.activeInHierarchy) 
			{
				ResumeGame();   
			} else {
				PauseGame();
			}
		}
    }

    IEnumerator FireLightningBolt()
    {
        lightningBolt.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        lightningBolt.SetActive(false);
    }

    void Shoot()
    {
		if (weapon1.activeInHierarchy) {
			pistolMuzzleFlash.Play ();
			pistolSound.Play ();
		}

		if (weapon2.activeInHierarchy) {
			rifleMuzzleFlash.Play ();
			laserSound.Play ();
		}

		if (weapon3.activeInHierarchy) {
			thunderSound.Play ();
		}

        RaycastHit hit;

        if (Physics.Raycast(weapon1.transform.position, fpsCam.transform.forward, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null &&
                ((hit.transform.CompareTag("Bacteria") && weapon1.activeInHierarchy) || (hit.transform.CompareTag("Germ") && weapon2.activeInHierarchy) || weapon3.activeInHierarchy))
            {
				if (target.TakeDamage(damage))
					killed++;
				
				if (CheckWin ()) {
                    // winning situation
                    PlayerPrefs.SetInt("score", purityPercentage);
                    SceneManager.LoadScene("Game Over");
                }
            }
        }
    }

	void PauseGame(){
		Time.timeScale = 0;
        GetComponent<FirstPersonController>().enabled = false;
        GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(false);
        paused = true;
		pausecanvas.SetActive(true);
    }

    public void ResumeGame(){
		Time.timeScale = 1;
        GetComponent<FirstPersonController>().enabled = true;
        GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(true);
        paused = false;
		pausecanvas.SetActive(false);
    }

    bool CheckWin(){
		purityPercentage = Convert.ToInt32(killed * 100 / total);
		percentageText.text = "Purity Percentage: " + purityPercentage +" %";
		return purityPercentage >= 100;
	}

	static bool IsGameover(){
		impurityPercentage = Convert.ToInt32(missed * 100 / total);
        staticImpurityPercentageText.text = "Percentage of Missed: " + impurityPercentage + " %";
		return impurityPercentage >= 20;
	}

	static public void Cleanup(GameObject o)
    {
		missed++;
		GameObject.Destroy(o);
		if (IsGameover ()) {
            // losing situation
            PlayerPrefs.SetInt("score", purityPercentage);
            SceneManager.LoadScene("Game Over");
		}
    }

    void SwitchWeapons()
    {
        if (weapon1.activeInHierarchy)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
        }
        else if (weapon2.activeInHierarchy)
        {
            weapon2.SetActive(false);
            weapon3.SetActive(true);
        }
        else
        {
            weapon3.SetActive(false);
            weapon1.SetActive(true);
        }
    }
}
