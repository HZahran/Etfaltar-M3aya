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

	// Use this for initialization
	void Start () {
		weaponOne = weapon1;
		weaponTwo = weapon2;
		weaponThree = weapon3;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Z))
			switchWeapons ();
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
