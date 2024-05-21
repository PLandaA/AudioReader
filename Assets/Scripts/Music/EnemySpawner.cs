using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public int _band4, _band5, _band6;
	public float [] frequencies;
	public GameObject [] spawnPoints;
	public GameObject enemyPrefab;
	public float maxSpawnTime;


	float scaleMultiplier;
	int index;
	int newIndex;
	float [] frequencyBuffer;
	int windowSize = 3;
	float spawnTime;
	float time;
	bool firstTime = false;



	// Start is called before the first frame update
	void Start () {
		scaleMultiplier = 1.5f;
		frequencyBuffer = new float [windowSize];
		frequencies = new float [3];
		index = 0;


		InvokeRepeating("saveFrequency", 2, 5);
		StartCoroutine(startSpawning());
	}

	private void Update () {
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		time += Time.deltaTime;

		if (firstTime) {

			if (time >= spawnTime) {
				spawnEnemies();
				setSpawnTime();
				GameManager.instance.activateEnemies();
				time = 0;
			}

		
		}

		

	}

	void saveFrequency () {

		for (int i = 0; i < frequencies.Length; i++) {
			frequencies [i] = 1.0f /setSpawnSpeed() [newIndex] * scaleMultiplier;
		}

		float mobileAverage = calculateMobileAverage();

		

	}


	float [] setSpawnSpeed () {
		float [] spawnSpeed = { AudioPeer._freqBand [_band4], AudioPeer._freqBand [_band5], AudioPeer._freqBand [_band6] };
		newIndex++;

		if (newIndex >= spawnSpeed.Length) {
			newIndex = 0;
		}

		return spawnSpeed;

	}




	private float calculateMobileAverage () {
		float sum = 0;

		// Add the new frequency value to the buffer and subtract the oldest value
		sum += frequencies [index];
		frequencyBuffer [index] = frequencies [index];

		// Update the current index and wrap around if needed
		index = (index + 1) % windowSize;

		// Calculate the sum of the values in the buffer
		for (int i = 0; i < windowSize; i++) {
			sum += frequencyBuffer [i];
		}

		// Calculate the mobile average


		return sum / windowSize;
	}

	void spawnEnemies () {
        if (!GameManager.instance.isLevel2State() && !GameManager.instance.isLevel3State()) {
            GameObject spawnPoint = spawnPoints [Random.Range(0, 5)];
			Instantiate(enemyPrefab.gameObject, spawnPoint.transform.position, Quaternion.identity);
		} 
		
		if (GameManager.instance.isLevel3State()) {
            GameObject spawnPoint = spawnPoints [Random.Range(6, 9)];
            Instantiate(enemyPrefab.gameObject, spawnPoint.transform.position, Quaternion.identity);
        }

    }

	void setSpawnTime () {
		spawnTime = frequencies [0];
		if (spawnTime > maxSpawnTime) {
			spawnTime = maxSpawnTime;
		}
		Debug.Log("Next object spawn in " + spawnTime + " seconds.");


	}

	public float setSpeedTime () {
		spawnTime = frequencies [0];
		if (spawnTime > maxSpawnTime) {
			spawnTime = maxSpawnTime;
		}

		return spawnTime;

	}

	
	IEnumerator startSpawning () {
		yield return new WaitForSeconds(5);
		firstTime = true;

	}

	IEnumerator startNewSpawn() {
		yield return new WaitForSeconds(10);

	}
}



/*
The frequency formula is given as,

Formula 1: The frequency formula in terms of time is given as:

f = 1/T

where,
f is the frequency in hertz measured in m/s, and
T is the time to complete one cycle in seconds

Promedio móvil calcula los valores medios de una ventana especificada y representa los valores en un gráfico de serie temporal. 
Un promedio móvil crea un efecto de suavizado y reduce el ruido de las fluctuaciones diarias. 
El promedio móvil también se puede utilizar para imputar datos que faltan con valores estimados.
*/

