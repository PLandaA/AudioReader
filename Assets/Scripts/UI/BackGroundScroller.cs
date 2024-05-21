using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundScroller : MonoBehaviour {
	[SerializeField] RawImage img;
	[SerializeField] float x, y;
	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);

	}
}
