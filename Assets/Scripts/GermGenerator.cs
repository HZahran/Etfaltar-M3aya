using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GermGenerator : MonoBehaviour {

	public Transform germPrefab;
	public float speed = 5;
	public float generationTime = 3;

	// Use this for initialization
	void Start () {
		// Generate every generationTime seconds
		StartCoroutine(WaitAndGenerate(generationTime));
	}

	// Update is called once per frame
	void Update () {

	}

	void CreateGerm()
	{
		Transform germ = Instantiate(germPrefab, this.transform);

		// Random position in X,Y
		float posX = Random.Range(-4, 4);
		float posY = Random.Range(-3, 3);
		germ.Translate(posX , posY, 0);

		// Random scale
		float scale = Random.Range(0.2f, 1.5f);
		germ.localScale = new Vector3(scale, scale, scale);

		// Add positional & rotational velocity
		germ.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
		germ.GetComponent<Rigidbody>().angularVelocity = new Vector3(speed, speed, 0);

	}

	// Generating Germ coroutine
	private IEnumerator WaitAndGenerate(float waitTime)
	{
		while (true)
		{
			CreateGerm();
			yield return new WaitForSeconds(waitTime);
		}
	}
}
