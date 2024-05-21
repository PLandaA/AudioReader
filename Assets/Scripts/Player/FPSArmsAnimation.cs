using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;


public class FPSArmsAnimation : MonoBehaviour {
	Animator anim;
	PlayerManager player;

	void Start () {
		player = FindObjectOfType<PlayerManager>(); 
		anim = GetComponent<Animator>();
	}

	void Update () {
		Shoot();

	}

	void Shoot () {
		if (player.isShooting) {
			anim.SetBool("Shooting", true);
		} else {
			anim.SetBool("Shooting", false);
		}
	}
}
