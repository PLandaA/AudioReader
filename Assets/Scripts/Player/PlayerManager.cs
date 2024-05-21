using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;
using TMPro;
using MoreMountains.Tools;



public class PlayerManager : MonoBehaviour {


	[SerializeField] float range;
	[SerializeField] float damage;
	[SerializeField] float cooldownTime;
	[SerializeField] float explosionRadius;
	[SerializeField] float explosionDamage;
	[SerializeField] LayerMask whatIsEnemy;
	[SerializeField] TextMeshProUGUI shockwaveChargesText;
	[SerializeField] GameObject fallText;
	[SerializeField] AudioClip shootAudio;
	[SerializeField] AudioClip shockwaveAudio;
	[SerializeField] AudioClip keyAudio;
	[SerializeField] Transform shockwavePos;
	[SerializeField] GameObject nextLevelCollider;
	[SerializeField] GameObject finalLevelCollider;
	[SerializeField] GameObject gun, rocket;
	[SerializeField] Image cooldownBar;


	public GameObject explosionVFX;

	StarterAssetsInputs starterAssetsInputs;
	FirstPersonController fps;
	EnemyManager enemyManager;
	GroundEnemyAI groundEnemyAI;
	MMProgressBar healthBar;
	Transform cam;
	public bool unlockedRocketLauncher = false;

	public bool isShooting = false;
	bool hasGun = true;
	float health = 100;
	float jump;
	int shockWaveCharges = 3;
	bool canShoot = true;

	RocketLauncher rocketLauncher;

	private void Awake () {
		starterAssetsInputs = GetComponent<StarterAssetsInputs>();
		fps = GetComponent<FirstPersonController>();
		enemyManager = FindObjectOfType<EnemyManager>();
		groundEnemyAI = FindObjectOfType<GroundEnemyAI>();
		cam = Camera.main.transform;
		healthBar = FindObjectOfType<MMProgressBar>();
		rocketLauncher = GetComponent<RocketLauncher>();
	}

	private void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		jump = fps.JumpHeight;
	}

	public void Update () {
		changeWeapon();
		checkForDeath();
		createShockwave();
		pauseGame();
	}


	public void Hit (float damage) {
		health -= damage;
		healthBar.Minus10Percent();
	}

	private void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Stick")) {
			enemyManager.damagePlayer();
		}
		if (other.CompareTag("WinPlatform")) {
			StartCoroutine(GameManager.instance.showFinalBoss());
			
		}
		if (other.CompareTag("Floor")) {
			fallText.SetActive(true);
			InvokeRepeating("damageOnFloor", 1, 1);

		}
		if (other.CompareTag("NextLevel")) {
			nextLevelCollider.SetActive(false);
			GameManager.instance.changeState();
			GameManager.instance.showLevel2Instructions();
			fps.JumpHeight = 2;

		}
		if (other.CompareTag("Key")) {
			GameManager.instance.openDoor();
			other.GetComponent<SpinningObject>().destroyItself();
			SoundManager.instance.setSound(keyAudio);
			GameManager.instance.showKeyTextInstructionsAndRocketLauncher();

		}
		if (other.CompareTag("Rocket")) {
			unlockedRocketLauncher = true;
			GameManager.instance.showRocketLauncherInstructionsText();
			other.GetComponent<SpinningObject>().destroyItself();
		}
		if (other.CompareTag("FinalLevel")) {
			fps.JumpHeight = jump;
            GameManager.instance.hideFinalDoor();
            GameManager.instance.changeState();
			finalLevelCollider.SetActive(false);


        }



    }

	private void OnTriggerExit (Collider other) {
		if (other.CompareTag("Floor")) {
			fallText.SetActive(false);
			CancelInvoke("damageOnFloor");

		}
	}


	void Shoot () {
		if (starterAssetsInputs.fire) {
			isShooting = true;
			RaycastHit hit;
			SoundManager.instance.setSoundDifferentVolume(shootAudio, 0.1f);
			if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)) {
				if (hit.collider.GetComponent<EnemyManager>() != null) {
					hit.collider.GetComponent<EnemyManager>().TakeDamage(damage, hit.point, hit.normal);
				}
				if (hit.collider.GetComponent<GroundEnemyAI>() != null) {
					hit.collider.GetComponent<GroundEnemyAI>().TakeDamage(damage, hit.point, hit.normal);
				}
				if (hit.collider.GetComponent<FinalBoss>() != null) {
					hit.collider.GetComponent<FinalBoss>().takeDamage(damage, hit.point, hit.normal);
				}
			}
			starterAssetsInputs.fire = false;
		} else {
			isShooting =  false;
		}


	}

	void shootRocketWeapon () {
		if (canShoot) {
			if (starterAssetsInputs.fire) {
				canShoot = false;
				rocketLauncher.shootRocket();
				starterAssetsInputs.fire = false;
				StartCoroutine(shootCooldown());
			} else {
				isShooting =  false;
			}

		} else {
			starterAssetsInputs.fire = false;
		}


	}

	void pauseGame () {
		if (starterAssetsInputs.pause) {
			GameManager.instance.controlPauseGame();
		}
		starterAssetsInputs.pause = false;
	}

	void createShockwave () {
		if (shockWaveCharges > 0) {
			if (starterAssetsInputs.shockwave) {
				Vector3 explosionPos = transform.position;
				Collider [] colliders = Physics.OverlapSphere(explosionPos, explosionRadius, whatIsEnemy);
				manageShockwaveCharges();
				checkForTargets(colliders);
				SoundManager.instance.setSound(shockwaveAudio);
			}
			starterAssetsInputs.shockwave = false;
		}
	}

	void manageShockwaveCharges () {
		shockWaveCharges--;
		shockwaveChargesText.text = " " + shockWaveCharges.ToString();
		Instantiate(explosionVFX, shockwavePos.position, Quaternion.identity);
	}

	void checkForTargets (Collider [] colliders) {
		foreach (Collider hit in colliders) {
			if (hit.GetComponent<EnemyManager>() != null) {
				hit.GetComponent<EnemyManager>().TakeShockwaveDamage(explosionDamage);
			}
		}
	}

	void damageOnFloor () {
		health -= 10;
		healthBar.Minus10Percent();
	}

	void checkForDeath () {
		if (health <= 0) {
			GameManager.instance.gameOver();
		}
	}

	void changeWeapon () {
		if (unlockedRocketLauncher) {
			if (!canShoot) {
				cooldownTime -= Time.deltaTime;
				cooldownBar.fillAmount = cooldownTime / 5f;
			} else {
				cooldownTime = 5f;
				cooldownBar.fillAmount = cooldownTime / 5f;

			}
			GameManager.instance.cooldownEnable();
			if (starterAssetsInputs.changeweapon) {
				gun.SetActive(false);
				rocket.SetActive(true);
				hasGun = false;
				starterAssetsInputs.changeweapon = false;
			}
			if (starterAssetsInputs.changeToGun) {
				gun.SetActive(true);
				rocket.SetActive(false);
				hasGun = true;
				starterAssetsInputs.changeToGun = false;
			}

		}
		checkWeapon();
	}

	void checkWeapon () {
		if (hasGun) {
			Shoot();
		} else {
			shootRocketWeapon();

		
		}
	}

	IEnumerator shootCooldown () {
		yield return new WaitForSeconds(5);
		canShoot = true;
	}



}

