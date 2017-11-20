using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour {

	void OnCollisionEnter(Collision c){
		PlayerControls.Cleanup (c.gameObject);
	}
}
