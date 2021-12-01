using UnityEngine;
using UnityEngine.UI;
using System;

public class CompasText : MonoBehaviour
{
    // Zapis kursu za pomocą "odwrotnosci" obrotu strzałki na kompasie
    
    public RectTransform arrow;
    private string rotation;
    private Text tekst;
    public double arrowRotation;


    // Use this for initialization
    void Start()
    {

       tekst = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // +/- 27 to poprawka na igłę kompasu, która po pobraniu z interentu była automatycznie obrócona o 27 stopni
        if (arrow.rotation.eulerAngles.z >= 27) { arrowRotation = (arrow.rotation.eulerAngles.z - 27); }
        if (arrow.rotation.eulerAngles.z < 27) { arrowRotation = 360 + (arrow.rotation.eulerAngles.z - 27); }

        arrowRotation = Math.Round(arrowRotation, 3);




        //   if (arrowRotation > 270) tekst.text = "Course: " + Math.Round(arrowRotation - 270, 3).ToString() + "°"; // Wypisanie na ekranie kursu
        //  else tekst.text = "Course: " + Math.Round(arrowRotation + 90, 3).ToString() + "°";

      
        tekst.text = "Course: " + Math.Round(arrowRotation, 3).ToString() + "°";




    }

    public double GiveHeading () // Wysłanie do zapisu kursu
    {
        if (arrowRotation > 270) return (arrowRotation - 270);
        else return (arrowRotation + 90);

    }
}
