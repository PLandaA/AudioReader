using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour {
    public int _band1, _band2, _band3;
    public Vector3 [] positions;
    public float speed;
    float _scaleMultiplier = 50.0f;
    int index = 0;
    bool reversePath, isMoving = false;
    float currentPosX, currentPosY, currentPosZ;
    [SerializeField] MovingState position;

    EnemySpawner enemySpawner;


    // Start is called before the first frame update
    void Start () {
        setupPositions();
        InvokeRepeating("savePosition", 1, 2);
        StartCoroutine(wait());

        enemySpawner = FindObjectOfType<EnemySpawner>();
        positions = new Vector3 [5];
    }

    // Update is called once per frame
    void Update () {
        if (isMoving) {
            movePosition(positions);
        }

    }


    void savePosition () {
        finalBossState();
        switch (position) {
            case MovingState.MoveX:
                selectState((AudioPeer._freqBand [_band1]  * _scaleMultiplier), currentPosY, currentPosZ);
                break;
            case MovingState.MoveY:
                selectState(currentPosX, (AudioPeer._freqBand [_band2]  * _scaleMultiplier), currentPosZ);
                break;
            case MovingState.MoveZ:
                selectState(currentPosX, currentPosY, (AudioPeer._freqBand [_band3]  * _scaleMultiplier));
                break;  
        }

    }


    public void movePosition (Vector3 [] pathPoints) {

        var step = speed * Time.deltaTime;

        Vector3 currentPointInPath = pathPoints [index];

        transform.position = Vector3.MoveTowards(transform.position, currentPointInPath, step);

        if (Vector3.Distance(transform.position, currentPointInPath) < 1) {
            if (reversePath) {
                if (index == 0) {
                    index = 0;
                    reversePath = false;
                } else {
                    index--;
                }
            } else {
                if (index == pathPoints.Length - 1) {
                    index = pathPoints.Length - 1;
                    reversePath = true;
                } else {
                    index++;
                }
            }
        }
    }

    float changeSpeed () {
        float maxTime = 14.0f;
        float changeSpeed = enemySpawner.setSpeedTime();

        if (changeSpeed <= maxTime) {
            changeSpeed = Random.Range(1,3);
		} else {
            changeSpeed = Random.Range(4,6);
        }

        return changeSpeed;
    }

    void setupPositions () {
        currentPosX = transform.position.x;
        currentPosY = transform.position.y;
        currentPosZ = transform.position.z;
    }

    void selectState (float xPos, float yPos, float zPos) {
        positions [index] = new Vector3(xPos,yPos, zPos);
        index++;
        if (index >= positions.Length) {
            revertPosition();
            index = 0;
        }

    }

    void revertPosition () {
        bool isFirstTime = false;
        isFirstTime = !isFirstTime;
        if (isFirstTime) {
            _scaleMultiplier = -_scaleMultiplier;

        } else {
            _scaleMultiplier = 30.0f;
        }

    }
        
    IEnumerator wait () {
        yield return new WaitUntil(() => index >= 3);
        StartCoroutine(changeSpeedTimer());
        isMoving = true;
    }

    IEnumerator changeSpeedTimer () {
        yield return new WaitForSeconds(5);
        changeSpeed();
        speed = changeSpeed();
    }

    void finalBossState () {
        if (GameManager.instance.isLevel3State()) {
            InvokeRepeating("setRandomPos", 10, 20);
        }
    }

    void setRandomPos() {
        position = (MovingState)Random.Range(0,3);
    }


}

public enum MovingState {
    MoveX,
    MoveY,
    MoveZ
}



