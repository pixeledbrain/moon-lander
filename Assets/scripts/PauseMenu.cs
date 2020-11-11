using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pauseMenu;
    public GameObject menu;
    public GameObject howtoplay;

    bool menuOn = false;
    bool onHowToPlay = false;

    void Start(){
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(menuOn){
                pauseOff();
            } else {
                pauseOn();
            }
        }
    }

    public void pauseOn(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
        menuOn = !menuOn;
        return;
    }

    public void pauseOff(){
        if(!onHowToPlay){
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            AudioListener.pause = false;
            menuOn = !menuOn;
        } else {
            menu.SetActive(true);
            howtoplay.SetActive(false);
            onHowToPlay = false;
        }  
        return;
    }

    public void MenuClick(int value){
        switch (value)
        {
            case 0://Exit
                pauseOff();
                SceneManager.LoadScene("MainMenu");
                break;
            case 1://Show "How to play". Activate howtoplay, deactivate self.
                //Here
                menu.SetActive(false);
                howtoplay.SetActive(true);
                onHowToPlay = true;
                break;
            case 2: //Go back to menu. Deactivate self, activate menu.
                menu.SetActive(true);
                howtoplay.SetActive(false);
                onHowToPlay = false;
                break;
        }
    }
}
