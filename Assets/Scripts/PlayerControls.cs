using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public float purityPercentage;
	static float total;
	static float killed;
	static float missed;

    public float damage;
    public Camera fpsCam;

    Animator anim;

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

        purityPercentage = 0;
		killed = 0;
		missed = 0;
		total = 100;

        damage = 10f;

        anim = GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();

            if (weapon3.activeInHierarchy) {
                StartCoroutine(FireLightningBolt());
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

        if (Physics.Raycast(weapon1.transform.position, weapon1.transform.forward, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null &&
                ((hit.transform.CompareTag("Bacteria") && weapon1.activeInHierarchy) || (hit.transform.CompareTag("Germ") && weapon2.activeInHierarchy) || weapon3.activeInHierarchy))
            {
				killed++; 
                target.TakeDamage(damage);
				if (CheckWin ()) {
					// winning situation
				}
            }
        }
    }

	bool CheckWin(){
		purityPercentage = killed * 100 / total;
		return purityPercentage >= 80;
	}

	static bool IsGameover(){
		return (missed * 100 / total) >= 20;
	}

	static public void Cleanup(GameObject o)
    {
		missed++;
		GameObject.Destroy(o);
		if (IsGameover ()) {
			// losing sitiuation
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
