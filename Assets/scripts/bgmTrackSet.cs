using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmTrackSet : MonoBehaviour
{

    public bgm Player;
    public AudioClip levelMusic;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("MusicPlayer").GetComponent<bgm>() as bgm;
        Player.playClip(levelMusic,0.03f);
    }

}
