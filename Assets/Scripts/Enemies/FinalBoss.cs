using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FinalBoss : MonoBehaviour {


	[SerializeField] AudioClip deathAudio;
	[SerializeField] float health;
	[SerializeField] Image healthBar;


	void Start () {

	}

	private void Update () {
		checkDeath();
	}

	public void takeDamage (float damage, Vector3 hitPos, Vector3 hitNormal) {
		health -= damage;
		healthBar.fillAmount = health / 1000f;

	}

	public void takeRocketDamage (float damage) {
		health -= damage;
		healthBar.fillAmount = health / 1000f;

	}

	void checkDeath () {
		if (health <= 0) {
			SoundManager.instance.setSound(deathAudio);
			StartCoroutine(deathWait());
			
		}
	 
	}

	IEnumerator deathWait () {
		yield return new WaitForSeconds(5);
		GameManager.instance.winScreen();

	}
}
