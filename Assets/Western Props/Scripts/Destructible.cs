// --------------------------------------
// This script is totally optional. It is an example of how you can use the
// destructible versions of the objects as demonstrated in my tutorial.
// Watch the tutorial over at http://youtube.com/brackeys/.
// --------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Destructible : MonoBehaviour {

	private Tuple<double, double> coinProbabilityThreshold = new Tuple<double, double>(0.0, 0.33);
	private Tuple<double, double> shieldProbabilityThreshold =  new Tuple<double, double>(0.34, 0.66);

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
		double drawn = rand.NextDouble();
		if(isInThreshold(drawn, coinProbabilityThreshold))
		{
			Instantiate(CoinObj, transform.position, transform.rotation);
		}
		if(isInThreshold(drawn, shieldProbabilityThreshold))
		{
			Instantiate(shieldObj, transform.position, transform.rotation);
		}
	}

	private bool isInThreshold(double val, Tuple<double, double> threshold)
	{
		return val > threshold.Item1 && val < threshold.Item2;
	}

}
