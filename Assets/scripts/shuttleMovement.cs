
using System.Runtime.ExceptionServices;
using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class shuttleMovement : MonoBehaviour
{

    public float StartingX;
    public float StartingY;

    public float torqueForce = 1;
    public float pushForce = 10;
    public float fuelConsumption;
    public float fuelConsumptionRotate;
    public float fuelRegen;
    public Image fuelIm;

    Rigidbody2D rb;

    GameObject fire1,fire2,fire3;

    float fuelLeft;

    bool upPress=false,leftPress=false,rightPress=false,resetPress=false;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(StartingX,StartingY);
        rb = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        fuelLeft = 1;
        fire1 = gameObject.transform.Find("Fire1").gameObject;
        fire2 = gameObject.transform.Find("Fire2").gameObject;
        fire3 = gameObject.transform.Find("Fire3").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        //Get inputs
        upPress = Input.GetKey("up")||Input.GetKey("space");
        leftPress = Input.GetKey("left");
        rightPress = Input.GetKey("right");
        resetPress = Input.GetKeyDown("r")||resetPress;
    }

    void FixedUpdate() {

        var startFuel = fuelLeft;

        if(resetPress){
            
            // Reset position and rigidbody momentum
            resetPress = false;
            rb.velocity = new Vector2(0,0);
            rb.angularVelocity = 0;
            gameObject.transform.position = new Vector3(StartingX,StartingY);
            gameObject.transform.rotation = new Quaternion(0,0,0,0);
            fuelLeft = 1;
            
        } else {

            if(upPress && (startFuel > 0||fuelConsumption==0)){
                fire1.SetActive(true);
                var rot = Quaternion.AngleAxis(rb.rotation,Vector3.forward);
                var angledir = rot*Vector3.up;
                fuelLeft = Mathf.Max(0,fuelLeft-fuelConsumption);
                rb.AddForce(angledir*pushForce);
            } else {
                fire1.SetActive(false);
            }

            if(!upPress && !leftPress && !rightPress){
                fuelLeft = Mathf.Min(1,fuelLeft+fuelRegen);
            }

            if(leftPress && (startFuel > 0||fuelConsumptionRotate==0)){
        
                fuelLeft = Mathf.Max(0,fuelLeft-fuelConsumptionRotate);
                rb.AddTorque(torqueForce);
                fire2.SetActive(true);
            }else {
                fire2.SetActive(false);
            }

            if(rightPress && (startFuel > 0||fuelConsumptionRotate==0)){
                fuelLeft = Mathf.Max(0,fuelLeft-fuelConsumptionRotate);
                rb.AddTorque(-1*torqueForce);
                fire3.SetActive(true);
            } else {
                fire3.SetActive(false);
            }

        }

        fuelIm.rectTransform.localScale = new Vector3(1, fuelLeft,1);
    }

    public void disableFlames(){
            fire1.SetActive(false);
            fire2.SetActive(false);
            fire3.SetActive(false);
        }
}
