using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShipScript : MonoBehaviour
{

    // proste poruszanie innym statkiem niezależnym od nas

    
    public float speed = 5;

  
    void FixedUpdate()
    {
               

        transform.Translate(Vector3.right * Time.deltaTime * (speed));

    }
}
