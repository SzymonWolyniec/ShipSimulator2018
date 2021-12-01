// Skrypt do obsługi włączania alarmu w przypadku zbliżania się statku do przeszkody

using UnityEngine;
using UnityEngine.UI;
using System;

public class AlarmRaycast : MonoBehaviour {

    public Light Alarm;
    public Text Powiadomienie;
    float distance = 139.3f;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward); // tworzenie promienia, który wysyłany jest na przód

        RaycastHit hit;  // informacja zwrotna nt. raycasta 
        
            Debug.DrawRay(transform.position, transform.forward * distance, Color.green); // rysowanie Raycasta na odpowiednią odległość


        {
            if (Physics.Raycast(ray, out hit, distance)) // jeśli odległość promienia raycast'a zaczynającego się w wybranym przez nas miejsu będzie mniejsza niz
                                                         // ustalona zostaną wykonane instrukcje
            {
                Alarm.enabled = true;            
                Powiadomienie.text = "Warning! Obstacle for " + (Math.Round(hit.distance - 39.3, 1) ) + " m";
            }

            else   // w przeciwnym wypadku
            {
                Powiadomienie.text = " ";
                Alarm.enabled = false;
            }
        }


        
    }
}
