using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour
{
    public AudioClip currentlyPlaying;    
    AudioSource AudioS;
    void Awake()
    {
        DontDestroyOnLoad (gameObject);
        AudioS = gameObject.GetComponent<AudioSource>() as AudioSource;
        AudioS.ignoreListenerPause=true;
    }
    public void playClip(AudioClip newClip,float newVolume){
        if(currentlyPlaying != newClip){
            currentlyPlaying = newClip;
            AudioS.Stop();
            AudioS.clip = newClip;
            AudioS.volume = newVolume;
            AudioS.Play();
        }
    } 

    public  void pause(){
        AudioS.Pause();
    }

    public void resume(){
        AudioS.UnPause();
    }

    public void restart(){
        AudioS.Stop();
        AudioS.Play();
    }
}
