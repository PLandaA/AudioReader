using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] float explosionRadius;
	[SerializeField] LayerMask whatIsEnemy;
	[SerializeField] GameObject explosionVFX;
	BigRookGames.Weapons.ProjectileController projectileController;
	int timeToLive;

	private void Start () {
		projectileController = FindObjectOfType<BigRookGames.Weapons.ProjectileController>();
		timeToLive = 5;
	}



	private void Update () {
		Explode();
	}

	void Explode () {
        // Find nearby colliders within the explosion radius
        Collider [] colliders = Physics.OverlapSphere(transform.position, explosionRadius, whatIsEnemy);

		foreach (Collider hit in colliders) {
			if (Time.deltaTime > timeToLive) {
				manageRocketExlosion();
			}
			if (hit.GetComponent<EnemyManager>() != null) {
				hit.GetComponent<EnemyManager>().TakeShockwaveDamage(100);
				manageRocketExlosion();
			}
			if (hit.GetComponent<FinalBoss>() != null) {
				hit.GetComponent<FinalBoss>().takeRocketDamage(50);
				manageRocketExlosion();
			}
		}

		
    }

	void manageRocketExlosion () {
		Instantiate (explosionVFX, transform.position, Quaternion.identity);
		projectileController.onFindEnemy();
		Destroy(gameObject);
	}
}