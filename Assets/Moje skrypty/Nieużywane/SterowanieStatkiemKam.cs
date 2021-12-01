 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SterowanieStatkiemKam : MonoBehaviour
{

    float speed = 0f;
    float turnSpeed = 0f;
    float sigmaC = -0.0159f;
    float sigmaR = 0f;
    float T = 50.66f;
    float K = 0.07f;
    float rot = 0f;
    float x, z, v, cog;
    

    

    private Rigidbody rbody;


    // Use this for initialization
    void Start()
    {

        x = transform.position.x;
        z = transform.position.z;
        cog = transform.position.y;

    }



    // Update is called once per frame
    void Update()
    {
      

        rot += (((((K * (sigmaC + sigmaR)) - rot) * Time.deltaTime)  / T) );
        cog += rot * Time.deltaTime;

        x = x + speed * (Mathf.Sin(cog * Mathf.PI / 180));
        z = z + turnSpeed * (Mathf.Cos(cog * Mathf.PI / 180));


        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

        transform.rotation = Quaternion.Euler(0, cog, 0);



        /*
        rbody.AddForce(transform.forward * -x * Time.deltaTime);
       

        if ((-x * Time.deltaTime) !=0 )

        {
            transform.Rotate(0, y * Time.deltaTime, 0, Space.Self);
            
            

        }
        */


    }


    public void addSpeed (float newSpeed)
    {
        speed = newSpeed ;
    }


    public void addTurn(float newTurn)
    {
       turnSpeed = newTurn ;
    }


    }

