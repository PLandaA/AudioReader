using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pato was here

public class Create512Cubes : MonoBehaviour {
    public GameObject _samplesCubesPrefab;
    GameObject[] _sampleCube = new GameObject [512];
    public float _maxScale;
    // Start is called before the first frame update
    void Start () {
        for (int i = 0; i < 512; i++) {
            GameObject _instanteSampleCube = (GameObject)Instantiate(_samplesCubesPrefab);
            _instanteSampleCube.transform.position = this.transform.position;
            _instanteSampleCube.transform.parent = transform;
            _instanteSampleCube.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703225f * i, 0);
            _instanteSampleCube.transform.position = Vector3.forward * 150;
            _sampleCube [i] = _instanteSampleCube;

        }
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < 512; i++) {
            if (_sampleCube != null) {
                _sampleCube [i].transform.localScale = new Vector3(10, (AudioPeer._samples [i] * _maxScale) + 2, 10);
            }

        }
    }
}
