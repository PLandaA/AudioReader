using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
//Pato was here

public class AudioPeer : MonoBehaviour {
    AudioSource _audioSource;
    public static float [] _samples = new float [512];
    public static float [] _freqBand = new float [8];
    
    void Start () {

        DontDestroyOnLoad(this);

        _audioSource = GetComponent<AudioSource>();


        _audioSource.Play();



    }

    
    void Update () {
        GetSpectrumAudioSource();
        MakeFrequencyBands();


    }

    void GetSpectrumAudioSource () {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);

    }

    void MakeFrequencyBands () {

        /*
         * 22050 / 512 = 43hertz per sample
         * 20 - 60hz
         * 60 - 250hz 
         * 250 - 500hz
         * 500 - 2000hz
         * 2000 - 4000hz
         * 4000 - 6000hz
         * 6000 - 20000hz
         * 
         * 0 - 2 = 86 hertz
         *                  
         * 1 - 4 = 172 hertz - 87 - 257
         * 2 - 8 = 344 hertz - 259 - 602 
         * 3 - 16 = 688 hertz - 603 - 1290 
         * 4 - 32 = 1376 hertz - 1291 - 2666
         * 5 - 64 = 2752 hertz - 2667 - 5418
         * 6 - 128 = 5504 hertz - 5419 - 10922
         * 7 - 256 = 11008 hertz - 10923 - 21930
         */

        int count = 0;
        for (int i = 0; i < 8; i++) {
			count = calulateFrequencies(count, i);

		}
	}

	private static int calulateFrequencies (int count, int i) {
		float average = 0;
		int sampleCount = (int)Mathf.Pow(2, i) * 2;

		if (i == 7) {
			sampleCount += 2;
		}

		for (int j = 0; j < sampleCount; j++) {
			average += _samples [count] * (count + 1);
			count++;
		}

		average /= count;

		_freqBand [i] = average * 10;
		return count;
	}

	private void OnLevelWasLoaded (int level) {
        if (level != 0) {
            if (_audioSource != null) {
                _audioSource.Play();
            }
        }
    }

    public void pauseMusic () {
        _audioSource.Pause();

    }

    public void playMusic () {
        _audioSource.Play();

    }
}






