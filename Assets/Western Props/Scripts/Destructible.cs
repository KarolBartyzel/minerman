// --------------------------------------
// This script is totally optional. It is an example of how you can use the
// destructible versions of the objects as demonstrated in my tutorial.
// Watch the tutorial over at http://youtube.com/brackeys/.
// --------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public double coinProbability = 0.7;
	public double shieldProbability = 0.7;

	public GameObject destroyedVersion;	// Reference to the shattered version of the object

	public GameObject CoinObj;
	public GameObject shieldObj;

	// If the player clicks on the object
	public void Collapse ()
	{
		CreateRandomCollectable();
		// Spawn a shattered object
		GameObject destroyed = Instantiate(destroyedVersion, transform.position, transform.rotation);
		// Remove the current object
		Destroy(gameObject);
		Destroy(destroyed, 3F);
	}

	public void CreateRandomCollectable()
	{
		System.Random rand = new System.Random();
		if(rand.NextDouble() <= coinProbability)
		{
			Instantiate(CoinObj, transform.position + Vector3.up, transform.rotation);

		}
		if(rand.NextDouble() <= shieldProbability)
		{
			Instantiate(shieldObj, transform.position + Vector3.up, transform.rotation);
		}
	}

}
