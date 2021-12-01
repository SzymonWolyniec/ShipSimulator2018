// Skrypt wyliczający prędkość statku

using UnityEngine;
using UnityEngine.UI;
using System;

public class Speedometer : MonoBehaviour {

    public Text speedometer;
    double firstX = 0, firstZ = 0, secondX = 0, secondZ = 0;
    public double speed = 0;
    public int step = 0;
    // Use this for initialization

    void Start ()
    {
        firstX = transform.position.x;
        firstZ = transform.position.z;
        
    }

    // Update is called once per frame
    void FixedUpdate () {

        step++;
        

        secondX = transform.position.x;
        secondZ = transform.position.z;

        // przebyta trasa na podstawie X i Z z obecnej i poprzedniej klatki
        speed = Math.Sqrt(  (Math.Pow((secondX - firstX), 2)) + (Math.Pow((secondZ - firstZ), 2))  ) ; 

        firstX = secondX;
        firstZ = secondZ;

        speed = (speed * 50 * 3600) / 1000; // *50 (1sekunda)   *3600 (1 godzina) /1000 (1 jednostka = 1 m, a więc na 1 km)

        
        if (step == 25 ) // Prędkość podawana co 0,5 sekundy. 1 sek = 50 itd.
       {
            // Prędkość w węzłach oraz km/h
           speedometer.text = "Speed: " + Math.Round(speed / 1.852, 2).ToString() + " kn    " + Math.Round(speed, 2).ToString() + " km/h";          
           step = 0;        
       }
          
    }


  public  double GiveSpeed() // Zmienna wysyłana do zapisania
    {
        return speed;
    }
}
