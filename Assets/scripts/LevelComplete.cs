using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public int nextLevelNum;
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
