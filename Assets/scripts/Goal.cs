using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject finishScreen;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<shuttleMovement>().disableFlames();
            other.gameObject.GetComponent<shuttleMovement>().enabled = false;
            //Play music here.
            StartCoroutine(CoroutineMethod());
        }
    }

    IEnumerator CoroutineMethod(){
        yield return new WaitForSeconds(0.5f);
        finishScreen.SetActive(true);
        
    }
}
