using UnityEngine;
using UnityEngine.UI;
using System;

public class FrozenValueText : MonoBehaviour
{
    // Ustawianie efetu zamrożenia na kamerze za pomocą slidera

    Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    public void SetSliderValue(float sliderValue)
    {
        
        textComponent.text = " Freeze : " + Math.Round(sliderValue, 0).ToString() + " %";

    }
}