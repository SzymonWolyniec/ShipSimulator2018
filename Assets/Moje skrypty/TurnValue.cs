using UnityEngine;
using UnityEngine.UI;
using System;

public class TurnValue : MonoBehaviour
{
    string rudderText = "0%"; // Tekst do przesłania
    Text textComponent;  // Wypisanie tekstu " Rudder x% Right/Left "

    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    public void SetSliderValue(float sliderValue)
    {
        if (sliderValue >= 0) 
        {
            textComponent.text = "Rudder: " + Math.Round(sliderValue, 3).ToString() + " % Right";
            rudderText = Math.Round(sliderValue, 2).ToString() + "%";
        }

        if (sliderValue < 0) 
        {
            textComponent.text = "Rudder: " + Math.Round(-sliderValue, 3).ToString() + " % Left";
            rudderText = Math.Round(sliderValue, 2).ToString() + "%";
        }
    }

    public string GetRudder ()       // Przesyłanie tekstu do zapisu
    { return rudderText; }
}