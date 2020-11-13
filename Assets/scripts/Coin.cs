using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    GameObject ScoreBoard;
    void Start()
    {
        ScoreBoard = GameObject.FindWithTag("Score");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            ScoreBoard.GetComponent<ScoreCounter>().score += 100;
            AudioSource aSource = gameObject.transform.parent.gameObject.GetComponent<AudioSource>() as AudioSource;
            aSource.Play();
            gameObject.SetActive(false);
        }
    }

}
