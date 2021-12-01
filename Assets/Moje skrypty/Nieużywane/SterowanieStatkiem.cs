using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SterowanieStatkiem : MonoBehaviour
{
   
    public float accellerateSpeed = 100f;
    public float turnSpeed = 100f;

    private Rigidbody rbody;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        
        
        rbody.AddForce(transform.forward * v * accellerateSpeed * Time.deltaTime);
        rbody.AddTorque(0f, h * turnSpeed * Time.deltaTime, 0f);
    }
}