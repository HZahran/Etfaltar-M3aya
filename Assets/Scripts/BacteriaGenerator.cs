using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaGenerator : MonoBehaviour {

    public Transform bacteriaPrefab;
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

    void CreateBacteria()
    {
        // Create a new bacteria instance
        Transform bacteria = Instantiate(bacteriaPrefab, this.transform);

        // Random position in X,Y
        float posX = Random.Range(-4, 4);
        float posY = Random.Range(-3, 3);
        bacteria.Translate(posX, posY, 0);

        // Random scale
        float scale = Random.Range(0.2f, 1.5f);
        bacteria.localScale = new Vector3(scale, scale, scale);

        // Add positional & rotational velocity
        bacteria.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
        bacteria.GetComponent<Rigidbody>().angularVelocity = new Vector3(speed, speed, 0);

    }

    // Generating bacteria coroutine
    private IEnumerator WaitAndGenerate(float waitTime)
    {
        while (true)
        {
            CreateBacteria();
            yield return new WaitForSeconds(waitTime);
        }
    }
}
