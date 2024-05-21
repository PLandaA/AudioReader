using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneChanger : MonoBehaviour {
    [SerializeField] GameObject songImage, confirmationImage, songOptions;
    

 
    public void sceneChange (string nextScene) {
        SceneManager.LoadScene(nextScene);
    }

    public void activateConfirmationScrreen () {
        confirmationImage.SetActive(true);
        songImage.SetActive(false);
        songOptions.SetActive(false);

    }

    public void activateSongOptionsScreen () {
        songOptions.SetActive(false);
        confirmationImage.SetActive(false);
        songImage.SetActive(true);


    }

    public void deactivateConfirmationScrreen () {
        songImage.SetActive(true);
        confirmationImage.SetActive(false);

    }

    public void returnToSongMenu () {
        songOptions.SetActive(true);
        songImage.SetActive(false);
        confirmationImage.SetActive(false);


    }



}
