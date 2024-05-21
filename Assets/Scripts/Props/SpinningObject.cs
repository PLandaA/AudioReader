using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour {
	[SerializeField] float rotationSpeed;
	

	void Start () {


	}

	void Update () {
		transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
	

	}

	public void destroyItself () {
		Destroy(gameObject);
	}
}
