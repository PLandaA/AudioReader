using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelector : MonoBehaviour {
    public const string audioName = "song.wav";

    AudioSource audioSource;
    AudioClip song;
    public string soundPath;



    void Start () {
        // song = (AudioClip)Resources.Load("Assets/Resources/Music/NeverGonnaGiveYouUp.mp3", typeof(AudioClip));
        //song = Resources.Load<AudioClip>("Music/NeverGonnaGiveYouUp");
        //soundPath = "file://" + Application.streamingAssetsPath;
        //audioSource.clip = song;
       // audioSource.Play();

      //  StartCoroutine(loadMusic());


    }


    private void Awake () {
        audioSource = GetComponent<AudioSource>();
        song = Resources.Load<AudioClip>("Assets/Feel/NiceVibrations/HapticSamples/Music/Tom2.wav");
        
        soundPath = "file://" + Application.streamingAssetsPath + "/";
        setupMusicFeatures();
        
    }

    void Update () {

    }

    public void FastSong () {
        // song = (AudioClip)Resources.Load("Music/Pump it up - Winter - Antonio Vilvaldi(Banya) (320 kbps).mp3", typeof(AudioClip));
        song = Resources.Load<AudioClip>("Music/Pump it up - Winter - Antonio Vilvaldi(Banya) (320 kbps)");
        setupMusicFeatures();
    }

    public void SlowSong () {
        // song = (AudioClip)Resources.Load("Assets/Resources/Music/SomethingAboutYou.mp3", typeof(AudioClip));
        song = Resources.Load<AudioClip>("Music/SomethingAboutYou");
        setupMusicFeatures();

    }

    public void MediumSong () {
        //song = (AudioClip)Resources.Load("Assets/Resources/Music/NeverGonnaGiveYouUp.mp3", typeof(AudioClip));
        song = Resources.Load<AudioClip>("Music/NeverGonnaGiveYouUp");
        setupMusicFeatures();

    }


    public void uploadSong () {
        StartCoroutine(loadAudio());
    }


    void setupMusicFeatures () {
        audioSource.clip = song;
        audioSource.Play();
        audioSource.loop = true;

    }


    [System.Obsolete]
    IEnumerator loadAudio () {
        WWW request = GetAudioFromFile(soundPath, audioName);
        yield return request;

        song = request.GetAudioClip();
        song.name = audioName;


        setupMusicFeatures();

    }

    [System.Obsolete]
    WWW GetAudioFromFile (string path, string filename) {
        string audioToLoad = string.Format(path + "{0}", filename);
        WWW request = new WWW(audioToLoad);
        return request;

    }


}
