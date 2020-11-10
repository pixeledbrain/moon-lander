using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public GameObject Player;

    //Values between 0 and 1 "dampen" movement
    public float parallaxVal =0.5f;
    public bool fromInitial=false;
    public bool reversed=false;
    Vector3 initialPosition;
    Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = Player.transform.position;
        lastPosition = Player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int multiplier = reversed?-1:1;
        float parallaxValNew = reversed?parallaxVal-1:parallaxVal;
        if(fromInitial){
            Vector3 difference = Player.transform.position - initialPosition;
            gameObject.transform.position = parallaxValNew*difference*multiplier;
        } else {
            Vector3 difference = Player.transform.position - lastPosition;
            lastPosition = Player.transform.position;
            gameObject.transform.position += parallaxValNew*difference*multiplier;
        }

        
    }
}
