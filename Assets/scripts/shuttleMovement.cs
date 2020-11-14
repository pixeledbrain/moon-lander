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
    float realTorqueForce =1;
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
    SpriteRenderer fire1SR,fire2SR,fire3SR;
    float fire1ASvol,fire2ASvol,fire3ASvol;
    bool fire1On=false,fire2On=false,fire3On=false;

    float fuelLeft;

    
    float realPushForce = 0;

    bool tinyState = false;

    //bool upPressDown=false,leftPressDown=false,rightPressDown=false;
    bool upPress=false,leftPress=false,rightPress=false,resetPress=false,tinyPress=false,canThrust=true;
    //bool upPressUp=true,leftPressUp=true,rightPressUp=true;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        gameObject.transform.position = new Vector3(StartingX,StartingY);
        rb = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        fuelLeft = 1;
        fire1 = gameObject.transform.Find("Fire1").gameObject;
        fire1AS = fire1.GetComponent<AudioSource>() as AudioSource;
        fire1SR = fire1.GetComponent<SpriteRenderer>() as SpriteRenderer;
        fire1ASvol = fire1AS.volume;
        fire1AS.volume=0;
        fire2 = gameObject.transform.Find("Fire2").gameObject;
        fire2AS = fire2.GetComponent<AudioSource>() as AudioSource;
        fire2SR = fire2.GetComponent<SpriteRenderer>() as SpriteRenderer;
        fire2ASvol = fire2AS.volume;
        fire2AS.volume=0;
        fire3 = gameObject.transform.Find("Fire3").gameObject;
        fire3AS = fire3.GetComponent<AudioSource>() as AudioSource;
        fire3SR = fire3.GetComponent<SpriteRenderer>() as SpriteRenderer;
        fire3ASvol = fire3AS.volume;
        fire3AS.volume=0;
        if(adjustThrust){
            //MOON GRAVSCALE = 0.166666
            float gravScale = rb.gravityScale;
            //realPushForce = (4f/0.166666f) * gravScale;

            if(gravScale <= 0.166666){
                float dampForce = 9.81f*gravScale;
                float wantedUpwardForce = (pushForce-(9.81f/6f));
                wantedUpwardForce = wantedUpwardForce + wantedUpwardForce*(gravScale-0.166666f)*(adjustThrustScale/2);
                realPushForce = dampForce+wantedUpwardForce;
                realTorqueForce = torqueForce;
            } else {
            //other alternative
                float dampForce = 9.81f*gravScale;
                float wantedUpwardForce = (pushForce-(9.81f/6f));
                wantedUpwardForce = wantedUpwardForce + wantedUpwardForce*(gravScale-0.166666f)*adjustThrustScale;
                realPushForce = dampForce+wantedUpwardForce;
                realTorqueForce = torqueForce + (torqueForce*((gravScale-0.166666f)/0.166666f))*(3f/4f);
            }
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
                fire1SR.enabled=true;
                fire1AS.volume = fire1ASvol;
                fire1AS.UnPause();
                fire1On=true;

                var rot = Quaternion.AngleAxis(rb.rotation,Vector3.forward);
                var angledir = rot*Vector3.up;
                fuelLeft = Mathf.Max(0,fuelLeft-fuelConsumption);
                rb.AddForce(angledir*realPushForce);
                regenFuel = false;
                thrustingRightNow = true;
            } else {
                if(fire1On){
                    StartCoroutine(VolumeFade(fire1AS, 0f, 0.2f));
                    fire1SR.enabled=false;
                    fire1On=false;
                }
            }

            if(leftPress && canThrust){

                fire2SR.enabled=true;
                fire2AS.volume = fire2ASvol;
                fire2AS.UnPause();
                fire2On=true;
        
                fuelLeft = Mathf.Max(0,fuelLeft-fuelConsumptionRotate);
                regenFuel = false;
                //rb.AddTorque(torqueForce);
                rb.AddTorque(realTorqueForce);
                thrustingRightNow = true;
            }else {
                if(fire2On){
                    StartCoroutine(VolumeFade(fire2AS, 0f, 0.2f));
                    fire2SR.enabled=false;
                    fire2On=false;
                }
            }

            if(rightPress && canThrust){
                fire3SR.enabled=true;
                fire3AS.volume = fire3ASvol;
                fire3AS.UnPause();
                fire3On=true;

                fuelLeft = Mathf.Max(0,fuelLeft-fuelConsumptionRotate);
                regenFuel = false;
                //rb.AddTorque(-1*torqueForce);
                rb.AddTorque(-1*realTorqueForce);
                thrustingRightNow = true;
            } else {
                if(fire3On){
                    StartCoroutine(VolumeFade(fire3AS, 0f, 0.2f));
                    fire3SR.enabled=false;
                    fire3On=false;
                }
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

    //Use StartCoroutine();
    IEnumerator VolumeFade(AudioSource _AudioSource, float _EndVolume, float _FadeLength)
    {
        float _StartVolume = _AudioSource.volume;
        float _StartTime = Time.time;
        while (Time.time < _StartTime + _FadeLength)
        {
            _AudioSource.volume = _StartVolume + ((_EndVolume - _StartVolume) * ((Time.time - _StartTime) / _FadeLength));
            yield return null;
        }
        if (_EndVolume == 0) {_AudioSource.Pause();}
    }
}
