using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


// Forgive me for the spaghetti
// Forgive me for the spaghetti
// Forgive me for the spaghetti
// Forgive me for the spaghetti
// Forgive me for the spaghetti
// Forgive me for the spaghetti
// Forgive me for the spaghetti
// Forgive me for the spaghetti

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

    public bool adjustThrust = false;
    public bool thrustingRightNow = false;
    public float adjustThrustScale = 1;

    public Sprite SpaceShipNormal;
    public Sprite SpaceShipTransparent;

    Rigidbody2D rb;

    GameObject fire1,fire2,fire3;
    AudioSource fire1AS,fire2AS,fire3AS;

    float fuelLeft;

    
    float realPushForce = 0;

    bool tinyState = false;

    //bool upPressDown=false,leftPressDown=false,rightPressDown=false;
    bool upPress=false,leftPress=false,rightPress=false,resetPress=false,tinyPress=false,canThrust=true;
    //bool upPressUp=true,leftPressUp=true,rightPressUp=true;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(StartingX,StartingY);
        rb = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        fuelLeft = 1;
        fire1 = gameObject.transform.Find("Fire1").gameObject;
        fire2 = gameObject.transform.Find("Fire2").gameObject;
        fire3 = gameObject.transform.Find("Fire3").gameObject;
        if(adjustThrust){
            //MOON GRAVSCALE = 0.166666
            float gravScale = rb.gravityScale;
            //realPushForce = (4f/0.166666f) * gravScale;

            //other alternative
            float dampForce = 9.81f*gravScale;
            float wantedUpwardForce = (4f-(9.81f/6f));
            wantedUpwardForce = wantedUpwardForce + wantedUpwardForce*(gravScale-0.166666f)*adjustThrustScale;
            realPushForce = dampForce+wantedUpwardForce;
        } else {
            realPushForce = pushForce;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get inputs
        upPress = Input.GetKey("up")||Input.GetKey("space");
        leftPress = Input.GetKey("left");
        rightPress = Input.GetKey("right");
        resetPress = Input.GetKeyDown("r")||resetPress;
        tinyPress = Input.GetKeyDown("t")||tinyPress;

        //upPressDown = Input.GetKeyDown("up")||upPressDown;leftPressDown=Input.GetKeyDown("left")||leftPressDown;rightPressDown=Input.GetKeyDown("right")||rightPressDown;
        //upPressUp = Input.GetKeyUp("up")||upPressUp;leftPressUp=Input.GetKeyUp("left")||leftPressUp;rightPressUp=Input.GetKeyUp("right")||rightPressUp;

    }

    void FixedUpdate() {

        var startFuel = fuelLeft;

        //Want fuel to regen when it has run out, while the buttons are pressed, but not trigger thrust until keys have been re-pressed
        bool thrustReset = !upPress && !leftPress && !rightPress;

        canThrust = fuelConsumption == 0 && fuelConsumptionRotate==0  || canThrust ||startFuel>0 && thrustReset;

        if(tinyPress){

            SpriteRenderer spriteRen = gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            spriteRen.sprite = tinyState?SpaceShipNormal:SpaceShipTransparent;
            tinyState = !tinyState;
            tinyPress = false;
        }

        if(resetPress){
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        } else {
            
            bool regenFuel = true;
            thrustingRightNow = false;

            if(upPress && canThrust){
                fire1.SetActive(true);
                var rot = Quaternion.AngleAxis(rb.rotation,Vector3.forward);
                var angledir = rot*Vector3.up;
                fuelLeft = Mathf.Max(0,fuelLeft-fuelConsumption);
                rb.AddForce(angledir*realPushForce);
                regenFuel = false;
                thrustingRightNow = true;
            } else {
                fire1.SetActive(false);
            }

            if(leftPress && canThrust){
        
                fuelLeft = Mathf.Max(0,fuelLeft-fuelConsumptionRotate);
                regenFuel = false;
                rb.AddTorque(torqueForce);
                fire2.SetActive(true);
                thrustingRightNow = true;
            }else {
                fire2.SetActive(false);
            }

            if(rightPress && canThrust){
                fuelLeft = Mathf.Max(0,fuelLeft-fuelConsumptionRotate);
                regenFuel = false;
                rb.AddTorque(-1*torqueForce);
                fire3.SetActive(true);
                thrustingRightNow = true;
            } else {
                fire3.SetActive(false);
            }

            //if(!upPress && !leftPress && !rightPress){
            if(regenFuel){
                fuelLeft = Mathf.Min(1,fuelLeft+fuelRegen);
            }
            canThrust=fuelLeft==0?false:canThrust;
        }

        fuelIm.rectTransform.localScale = new Vector3(1, fuelLeft,1);
    }

    public void disableFlames(){
            fire1.SetActive(false);
            fire2.SetActive(false);
            fire3.SetActive(false);
        }
}
