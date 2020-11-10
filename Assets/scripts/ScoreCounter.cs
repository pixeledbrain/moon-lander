using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    // Start is called before the first frame update
    
    public int score = 0;


    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "" + score;
    }
}
