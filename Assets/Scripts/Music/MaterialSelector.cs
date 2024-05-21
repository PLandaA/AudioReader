using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSelector : MonoBehaviour {
	Texture texture;
	Material material;
	// Start is called before the first frame update
	void Start () {

		material = Resources.Load("Assets/Resources/Textures/Water", typeof(Material)) as Material;
		GetComponent<Renderer>().material = material;
		Debug.Log(GetComponent<Renderer>().material.name);
		textureSelector();

	}

	// Update is called once per frame
	void Update () {

	}


	public void textureSelector () {
		// song = (AudioClip)Resources.Load("Music/Pump it up - Winter - Antonio Vilvaldi(Banya) (320 kbps).mp3", typeof(AudioClip));
		
	}
}
