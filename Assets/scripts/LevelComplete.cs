using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    public int nextLevelNum;

    private void Awake() {
        if(nextLevelNum == -1){
            (GameObject.Find("Next").GetComponent<Button>() as Button).interactable=false;
        }
    }

    public void selectScene(int num){
        switch (num){
            case 1:
                SceneManager.LoadScene("Level"+nextLevelNum);
                break;
            case 2: // Retry this level?
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case 0:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
