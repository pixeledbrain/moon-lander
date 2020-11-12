using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void selectScene(int num){
        switch (num){
            case 1:
                SceneManager.LoadScene("LevelSelect");
                break;
            case 2:
            SceneManager.LoadScene("Credits");
                break;
            case 0:
                Application.Quit();
                break;
        }
    }
}
