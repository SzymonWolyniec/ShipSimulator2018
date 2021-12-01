using UnityEngine;
using UnityEngine.UI;
using System;

public class PowerValue : MonoBehaviour
{
    string engineText = "0%"; // Tekst do przesłania
    Text textComponent;  // Wypisanie tekstu " Engine order telegraph: x% Ahead / Astern "

    void Start()
    {
        textComponent = GetComponent<Text>();
    }


    public void SetSliderValue(float sliderValue)
    {
        if (sliderValue >= 0) 
            {
            textComponent.text = " Engine order telegraph: " + Math.Round(sliderValue, 3).ToString() + " % Ahead";
            engineText = Math.Round(sliderValue, 2).ToString()+ "%";
            }

        if (sliderValue < 0)
        {
            textComponent.text = " Engine order telegraph: " + Math.Round(-sliderValue, 3).ToString() + " % Astern";
            engineText = Math.Round(sliderValue, 2).ToString()+ "%";
        }
    }


    public string GetEnginePower() // Przesyłanie tekstu do zapisu
    { return engineText; }


}