using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;



public class TutorialManager : MonoBehaviour {

	
	[SerializeField] GameObject hookText, movementText, shootingText, shockWaveText, sceneText, playButton, continueButton;
	[SerializeField] TutorialState state;
	[SerializeField] GameObject enemy;
	[SerializeField] GameObject spawnPos;
	Vector3 enemyPos;
	public int enemiesInScene;




	void Start () {
		state = TutorialState.Movement;
		enemyPos = spawnPos.transform.position;
		enemiesInScene = 0;


	}

	void Update () {
		changeState();
	}

	void changeState () {
		switch (state) {
			case TutorialState.None:
				break;
			case TutorialState.Movement:
				showMovementText();
				StartCoroutine(changeToShootingText());
				break;
			case TutorialState.Shooting:
				showShootingText();
				break;
			case TutorialState.Shockwave:
				showShockwaveText();
				break;
			case TutorialState.Hook:
				showHooKText();
				break;
			case TutorialState.Scene:
				showSceneText();
				break;

		}

	}

	void showMovementText () {
		movementText.SetActive(true);
	}

	void showShootingText () {
		movementText.SetActive(false);
		shootingText.SetActive(true);
		if (enemiesInScene >= 1) {
			StartCoroutine(waitToRespawn());

		}

	}

	void showShockwaveText() {
		shootingText.SetActive(false);
		shockWaveText.SetActive(true);
		if (enemiesInScene >= 2) {
			StartCoroutine(waitToHook());
		}
	}

	void showHooKText () {
		shockWaveText.SetActive(false);
		hookText.SetActive(true);
		StartCoroutine(changeToChooseSong());


	}

	void showSceneText () {
		hookText.SetActive(false);
		sceneText.SetActive(true);
		playButton.SetActive(true);
		continueButton.SetActive(true);
		Cursor.lockState = CursorLockMode.None;

	}
	IEnumerator changeToShootingText () {
		yield return new WaitForSeconds(15);
		state = TutorialState.Shooting;
		if (enemiesInScene <= 0) {
			Instantiate(enemy, enemyPos, Quaternion.identity);
			enemiesInScene++;
		}

		

	}

	IEnumerator waitToRespawn () {
		yield return new WaitForSeconds(15);
		state = TutorialState.Shockwave;
		if (enemiesInScene <= 1) {
			Instantiate(enemy, enemyPos, Quaternion.identity);
			enemiesInScene++;
		}


	}

	IEnumerator waitToHook () {
		yield return new WaitForSeconds(15);
		state = TutorialState.Hook;

	}

	IEnumerator changeToChooseSong () {
		yield return new WaitForSeconds(15);
		state = TutorialState.Scene;


	}

	public void changeScene (string scene) {
		SceneManager.LoadScene(scene);


	}

	public void playAgain (string scene) {
		SceneManager.LoadScene(scene);


	}
}

enum TutorialState {
	None,
	Movement,
	Shooting, //Enemy 
	Shockwave,
	Hook, 
	Scene
}
