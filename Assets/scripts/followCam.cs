using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class followCam : MonoBehaviour
{

    public Transform following;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(following.position.x,following.position.y,transform.position.z);
    }
}
