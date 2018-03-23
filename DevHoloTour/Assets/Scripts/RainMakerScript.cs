using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMakerScript : MonoBehaviour {

    private ParticleSystem rain;
    private AudioSource audio;
    private float fadeTime = 3f;
    private float beginningAudioVolumeSetting;
    private bool isRaining = false;

    public void StartRaining()
    {
        if (!isRaining)
        {
            rain.Play();
            StopAllCoroutines();
            StartCoroutine(FadeInAudio());
            isRaining = true;
        }
    }

    public void StopRaining()
    {
        if (isRaining)
        {
            rain.Stop();
            StopAllCoroutines();
            StartCoroutine(FadeOutAudio());
            isRaining = false;
        }
    }

    private IEnumerator FadeInAudio()
    {
        audio.volume = 0;
        audio.Play();

        while (audio.volume < beginningAudioVolumeSetting)
        {
            audio.volume += beginningAudioVolumeSetting * Time.deltaTime / fadeTime;

            yield return null;
        }
        audio.volume = beginningAudioVolumeSetting;
    }

    private IEnumerator FadeOutAudio()
    {
        while (audio.volume > 0)
        {
            audio.volume -= beginningAudioVolumeSetting * Time.deltaTime / fadeTime;

            yield return null;
        }
        audio.Stop();
        audio.volume = beginningAudioVolumeSetting;
    }
    
    public void Start()
    {
        rain = GetComponentInChildren<ParticleSystem>();
        rain.Stop();
        audio = GetComponent<AudioSource>();
        beginningAudioVolumeSetting = audio.volume;
    }

    // Update is called once per frame
    void Update () {
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
	}
}
