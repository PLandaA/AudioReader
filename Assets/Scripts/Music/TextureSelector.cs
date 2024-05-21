using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSelector : MonoBehaviour {
	public Material [] materials;
	public int _band7;
	public int _band8;
	public float frequency;

	MeshRenderer meshRenderer;
	Material mainMat;
	int scaleMultiplier;
	float currentFrequency;


	void Start () {
		scaleMultiplier = 30;
		meshRenderer = GetComponent<MeshRenderer>();
		InvokeRepeating("saveFrequency", 1, 2);
		StartCoroutine(wait());

	}

	public void materialProbablitySelector () {
		for (int i = 0; i < materials.Length; i++) {
			if (Random.value < currentFrequency) {
				selectRandomMaterial(0);

			} else {
				selectRandomMaterial(2);

			}
		}

		meshRenderer.material = mainMat;

	}

	void saveFrequency () {
		frequency =  Random.Range(AudioPeer._freqBand [_band7], AudioPeer._freqBand [_band8]);
		currentFrequency = frequency * scaleMultiplier;
	}

	void selectRandomMaterial (int indexStart) {
		mainMat = materials [Random.Range(indexStart, materials.Length)];
	}

	IEnumerator wait () {
		yield return new WaitForSeconds(3);
		materialProbablitySelector();
	}
}
