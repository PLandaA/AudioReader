using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    [SerializeField]
    GameObject winScreenImage, pauseScreenImage, gameOverScreenImage,
        chargesIcon, doorClosed, doorOpened, level2Instructions, 
        showKeyText, rocketLauncherObject, rocketLauncherInstructionsText, 
        finalLevelDoor, finalBossText, finalBoss, enemyBar, cooldownBar;
    GameObject AudioPeer;
    UIControllerTargetIndicator uIControllerTargetIndicator;
    AudioPeer ap;
    [SerializeField] AudioClip winSong, gameOverSong;

    public GameObject [] groundEnemyPrefab, allTexts;

    [SerializeField] GameObject [] finalPlatforms;

    [SerializeField] GameState state;


    private void Awake () {
        instance = this;
    }
    void Start () {
        state = GameState.Level1;
        AudioPeer = GameObject.FindGameObjectWithTag("Songs"); ;
        allTexts = GameObject.FindGameObjectsWithTag("UiTexts");
        uIControllerTargetIndicator = GetComponent<UIControllerTargetIndicator>();
        ap = FindObjectOfType<AudioPeer>();
        Time.timeScale = 1;
        state = GameState.Level1;



    }

    public void gameOver () {
        gameOverScreenImage.SetActive(true);
        SoundManager.instance.setSoundDifferentVolume(gameOverSong, .4f);
        manageScreens();

    }

    public void winScreen () {
        winScreenImage.SetActive(true);
        SoundManager.instance.setSoundDifferentVolume(winSong, .4f);
        manageScreens();

    }

    public void returnToMenu () {
        SceneManager.LoadScene("SongSelection");
        Destroy(AudioPeer);

    }


    public void Quit () {
        Application.Quit();

    }

    public void resetGameUI () {
        SceneManager.LoadScene("Playground");
        Time.timeScale = 1;
        winScreenImage.SetActive(false);

    }

    public void controlPauseGame () {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        pauseScreenImage.SetActive(true);
        chargesIcon.SetActive(false);
        ap.pauseMusic();

    }

    public void continueGame () {
        pauseScreenImage.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        chargesIcon.SetActive(true);
        ap.playMusic();
        Time.timeScale = 1;
    }

    public void changeState () {
        state++;
    }

    public bool isLevel2State () {
        if (state == GameState.Level2) {
            doorClosed.SetActive(true);
            return true;
        } else {
            return false;
        }
    }

    public bool isLevel3State () {
        if (state == GameState.Level3) {
            return true;
        } else {
            return false;
        }
    }



    public void openDoor () {
        doorOpened.SetActive(false);
    }

    public void activateEnemies () {
        if (isLevel2State()) {
            foreach (var enemy in groundEnemyPrefab) {
                if (enemy != null) {
                    enemy.SetActive(true);
                }
            }
        }
    }

    public void showLevel2Instructions () {
        level2Instructions.SetActive(true);
        StartCoroutine(deactivateLevel2InstructionsText());
    }

    public void showKeyTextInstructionsAndRocketLauncher () {
        showKeyText.SetActive(true);
        rocketLauncherObject.SetActive(true);
        StartCoroutine(deactivateLevel2InstructionsText());
        destroyAllLevel2Enemies();

    }


    void destroyAllLevel2Enemies () {
        foreach (var enemy in groundEnemyPrefab) {
            Destroy(enemy);
        }
    }

    public void showRocketLauncherInstructionsText () {
        rocketLauncherInstructionsText.SetActive(true);
        StartCoroutine(deactivateLevel2InstructionsText());

    }

    public void hideFinalDoor () {
        finalLevelDoor.SetActive(true);
    }



    IEnumerator deactivateLevel2InstructionsText () {
        yield return new WaitForSeconds(3);
        level2Instructions.SetActive(false);
        rocketLauncherInstructionsText.SetActive(false);
        finalBossText.SetActive(false);
        showKeyText.SetActive(false);
    }

    void manageScreens () {
        Cursor.lockState = CursorLockMode.None;
        enemyBar.SetActive(false);
        foreach (var text in allTexts) {
            text.SetActive(false);
        }
        Time.timeScale = 0;

    }

    public void cooldownEnable () {
        cooldownBar.SetActive(true);
	}

    public IEnumerator showFinalBoss () {
        finalBossText.SetActive(true);
        StartCoroutine(deactivateLevel2InstructionsText());
        yield return new WaitForSeconds(5);
        foreach (var platfroms in finalPlatforms) {
            platfroms.SetActive(true);
        }
        enemyBar.SetActive(true);
        finalBoss.SetActive(true);
    }


}

enum GameState {
    Level1,
    Level2,
    Level3,
}


