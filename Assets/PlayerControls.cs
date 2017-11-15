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

	// Use this for initialization
	void Start () {
		weaponOne = weapon1;
		weaponTwo = weapon2;
		weaponThree = weapon3;
		percentage = 1;
		missed = 0;
		killed = 0;
		total = 100;
	}
	
	// Update is called once per frame
	void Update () {
		percentage = (total-killed)/total;
		cleanup ();
		if (Input.GetKeyDown (KeyCode.Z))
			switchWeapons ();
	}

	void cleanup(){
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Bacteria")) {
			if (o.transform.position.z < transform.position.z - 30)
				missed++;
				GameObject.Destroy (o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Germ")) {
			if (o.transform.position.z < transform.position.z - 30)
				missed++;
				GameObject.Destroy (o);
		}
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Metal")) {
			if (o.transform.position.z < transform.position.z - 30)
				missed++;
				GameObject.Destroy (o);
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
