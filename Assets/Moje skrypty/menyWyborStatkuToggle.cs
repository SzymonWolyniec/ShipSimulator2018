// Skrypt umożliwiający dodanie wyboru innych statków do symulacji w przyszłości
using UnityEngine;
using UnityEngine.UI;


public class menyWyborStatkuToggle : MonoBehaviour {

    public Toggle kontenerowiec;
    public GameObject kontenerowiecShip;   
    public GameObject kontenerowiecLight;
    
    int i;

    void Update () {
		      
            kontenerowiecShip.GetComponent<menuRotate>().enabled = true;            
            kontenerowiecLight.SetActive(true);
                       
                  }

       
        }

