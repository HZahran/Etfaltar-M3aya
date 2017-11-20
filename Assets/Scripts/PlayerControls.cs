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

    static float percentage;
    static int total;
    static int killed;
    static int missed;

    public float damage;
    public float range;
    public Camera fpsCam;

    Animator anim;

    void Start()
    {
        weaponOne = weapon1;
        weaponTwo = weapon2;
        weaponThree = weapon3;

        percentage = 1;
        missed = 0;
        killed = 0;
        total = 100;

        damage = 10f;
        range = 1000f;

        anim = GetComponent<Animator>();
		sounds = GetComponent<AudioSource> ();
    }

    void Update()
    {
        percentage = (total - killed) / total; // FIXME: Throws exception when total = 0
        Cleanup();

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
        RaycastHit hit;

        if (Physics.Raycast(weapon1.transform.position, weapon1.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null &&
                ((hit.transform.CompareTag("Bacteria") && weapon1.activeInHierarchy) || (hit.transform.CompareTag("Germ") && weapon2.activeInHierarchy) || weapon3.activeInHierarchy))
            {
                target.TakeDamage(damage);
				AudioSource S = sounds [1];
				S.Play ();
            }
        }
    }

    void Cleanup()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Bacteria"))
        {
            if (o.transform.position.z < transform.position.z - 30)
            {
                missed++;
                GameObject.Destroy(o);
            }
        }

        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Germ"))
        {
            if (o.transform.position.z < transform.position.z - 30)
            {
                missed++;
                GameObject.Destroy(o);
            }
        }

        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Metal"))
        {
            if (o.transform.position.z < transform.position.z - 30)
            {
                missed++;
                GameObject.Destroy(o);
            }
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
