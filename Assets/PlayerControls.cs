using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject weapon3;

	static GameObject weaponOne;
	static GameObject weaponTwo;
	static GameObject weaponThree;

	static float percentage;
	static int total;
	static int killed;
	static int missed;

	public float damage;
	public float range;
	public Camera fpsCam;

	// Use this for initialization
	void Start () {
		weaponOne = weapon1;
		weaponTwo = weapon2;
		weaponThree = weapon3;
		percentage = 1;
		missed = 0;
		killed = 0;
		total = 100;

		damage = 10f;
		range = 1000f;

	}
	
	// Update is called once per frame
	void Update () {
		percentage = (total-killed)/total;
		cleanup ();

		if (Input.GetKeyDown (KeyCode.Z))
			switchWeapons ();

		if (Input.GetKeyDown (KeyCode.X)) {
			Shoot ();
		}
	}

	void Shoot(){
	
		RaycastHit hit;
		if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {

			Debug.Log (hit.transform.name);

			Target target = hit.transform.GetComponent<Target> ();
			if (target != null && 
				((hit.transform.CompareTag("Bacteria") && weapon1.activeInHierarchy) || (hit.transform.CompareTag("Germ") && weapon2.activeInHierarchy) || (hit.transform.CompareTag("Metal") && weapon3.activeInHierarchy))){
					target.TakeDamage (damage);
			}
		}

	}

	void cleanup(){
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Bacteria")) {
			if (o.transform.position.z < transform.position.z - 30) {
				missed++;
				GameObject.Destroy (o);
			}
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Germ")) {
			if (o.transform.position.z < transform.position.z - 30) {
				missed++;
				GameObject.Destroy (o);
			}
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Metal")) {
			if (o.transform.position.z < transform.position.z - 30) {
				missed++;
				GameObject.Destroy (o);
			}
		}
	}

	void switchWeapons(){
		
		if (weapon1.activeInHierarchy) {
			weapon1.SetActive (false);
			weapon2.SetActive (true);
		} else if (weapon2.activeInHierarchy) {
			weapon2.SetActive (false);
			weapon3.SetActive (true);
		} else {
			weapon3.SetActive (false);
			weapon1.SetActive (true);
		}
	}
}
