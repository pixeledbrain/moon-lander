using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmTrackSet : MonoBehaviour
{

    bgm Player;
    public AudioClip levelMusic;
    public GameObject myPrefab;

    // Start is called before the first frame update
    void Start()
    {   
        
        GameObject PlayerGO = GameObject.Find("MusicPlayer");
        if(PlayerGO != null){
            Player = PlayerGO.GetComponent<bgm>() as bgm;
        } else {
            Instantiate(myPrefab);
            Player = GameObject.Find("MusicPlayer(Clone)").GetComponent<bgm>() as bgm;
        }   
        Player.playClip(levelMusic,0.03f);
    }

}
