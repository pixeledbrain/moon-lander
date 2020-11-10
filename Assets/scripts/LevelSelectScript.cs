using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void selectScene(int num){
        if(num == -1){
            SceneManager.LoadScene("SampleLevel");
        } else if(num == 0){
            SceneManager.LoadScene("MainMenu");
        } else {
            string levelName = "Level"+num;
            SceneManager.LoadScene(levelName);
        }
    }
}
