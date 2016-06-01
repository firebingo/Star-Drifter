using UnityEngine;
using System.Collections;

public class ParticleSystemCleanup : MonoBehaviour {

    float timer = 0;

    float destroyTime = 1;
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;

        if (timer >= destroyTime)
            Destroy(this.gameObject);
	}
}
