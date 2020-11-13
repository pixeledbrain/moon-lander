using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleSound : MonoBehaviour
{

    AudioSource aSource;
    Rigidbody2D rb;

    public float thresholdMin=0;
    public float thresholdMinVolume=1;
    public float thresholdMax=100;
    public float thresholdMaxVolume=1;

    public float fire1Volume=1;
    public float fireSmallVolume=1;
    
    // Start is called before the first frame update
    void Start()
    {
        aSource = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
        rb = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {   

        ContactPoint2D[] listContacts = new ContactPoint2D[10];
        rb.GetContacts(listContacts);
        foreach(ContactPoint2D item in listContacts){
            if(item.normalImpulse>=thresholdMin){
                float fixedImpulse = Mathf.Min(item.normalImpulse,thresholdMax);
                aSource.volume = (((item.normalImpulse - thresholdMin)/thresholdMax)*(thresholdMaxVolume-thresholdMinVolume))+thresholdMinVolume;
                aSource.Play();
            }
        }
    }
}
