using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour {


	[SerializeField] Transform firePoint;
	[SerializeField] float launchForce;
	[SerializeField] GameObject rocketPrefab;


	private void Start () {
	}

	public void shootRocket () {
		createRocket();
		Debug.Log("fire");


	}


	void createRocket () {
		// Instantiate a rocket prefab at the fire point position and rotation
		GameObject rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);

		// Get the Rigidbody component of the rocket
		Rigidbody rocketRb = rocket.GetComponent<Rigidbody>();

		// Apply force to the rocket to launch it
		rocketRb.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);
	}

	
}
