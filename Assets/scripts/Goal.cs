using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject finishScreen;
    public GameObject pauseController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<shuttleMovement>().disableFlames();
            other.gameObject.GetComponent<shuttleMovement>().enabled = false;
            pauseController.GetComponent<PauseMenu>().enabled = false;
            AudioSource aSource = gameObject.GetComponent<AudioSource>() as AudioSource;
            aSource.Play();
            StartCoroutine(CoroutineMethod());
        }
    }

    IEnumerator CoroutineMethod(){
        yield return new WaitForSeconds(0.5f);
        finishScreen.SetActive(true);
        
    }
}
