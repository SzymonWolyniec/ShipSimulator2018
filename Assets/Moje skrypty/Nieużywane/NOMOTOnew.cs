using UnityEngine;
using UnityEngine.UI;
using System;

public class NOMOTOnew : MonoBehaviour
{

    public Slider speedSlider, turnSlider;
    public Text rotText, cogText;


    float speed = 0;
    double x, z, klik = 1, wychylenie = 100, raz = 0;
    // NOMOTO zmienne  
    float SigmaR = 0, T = 30.66F, K = 0.5F, sigmaC = 0.0F, rot = 0, cog, speedValue = 0, turnSpeedValue = 0;
    float wypiszCOG, autopilotFunkcja = 0;

    public int mode, coordinates;


    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public Transform target, namierzanie, ship;
    public float kurs, kursStatku;
    float wyborFunkcji = 0, start = 0;


    void Start()
    {
        GetComponent<SetCoordinates>().SetModeCoordinates(); // Włączenie skryptu ustalającego tryb symulacji, statek etc.

        cog = transform.eulerAngles.y; // pobranie wartości COG, jako wartość RotationY statku
        Time.timeScale = 1;
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    }

    void FixedUpdate()
    {

        // dążenie do wartości z opóźnieniem (aktualna wartość, do której chcemy dążyć, zmiana w czasie)
        speed = Mathf.MoveTowards(speed, speedValue, 0.5F * Time.deltaTime);



        // IFy potrzebne w przypadkach, gdy np. rot dażąc do 1 zapisywał pokolei: 0,9845, 0,99845, 0,999845... dążąc do wartości, ale jej nie osiągając

        #region Zaokrąglanie ROTa

        if ((rot < 0.001 && rot > 0) || (rot > -0.001 && rot < 0))
        {
            rot = 0;
        }

        if (((rot > ((turnSpeedValue * K) - 0.001)) && (rot < turnSpeedValue * K)) || ((rot < ((turnSpeedValue * K) + 0.001)) && (rot > turnSpeedValue * K)))
        {
            rot = turnSpeedValue * K;
        }
        #endregion

        // skręt możliwy tylko w wypadku, gdy łódź płynie 


        if (speed != 0)
        {
            SigmaR = Mathf.MoveTowards(SigmaR, turnSpeedValue, 0.1F * Time.deltaTime);
            cog = cog + rot * Time.deltaTime; // COG = obecny COG + obecny ROT/min;
            rot = rot + ((K * (sigmaC + SigmaR) - rot) * Time.deltaTime * 10) / T;

        }


        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self); // ruch do przodu o szybkości "speed", dla lokalnych osi
        transform.rotation = Quaternion.Euler(0, cog, 0); // obrót do wartości COG

        #region Ustawienia COG do wypisania   
        if ((cog > 360)) { cog = 0; }
        if (cog < 0) { cog = 360 + cog; }


        if (cog >= 0 && cog < 90) { wypiszCOG = 270 + cog; }
        else { wypiszCOG = cog - 90; }


        // wypisanie wartości na ekranie
        rotText.text = "ROT: " + Math.Round(rot * 60, 3).ToString() + " ° / min";
        cogText.text = "COG: " + Math.Round(wypiszCOG, 3).ToString() + " °";
        #endregion


        // if (Input.GetKeyDown(KeyCode.O)) { turnSlider.value = (float)(klik * -wychylenie); klik = (float)(klik / 2); return; }
        // if (Input.GetKeyDown(KeyCode.P)) { turnSlider.value = (float)(klik * wychylenie); klik = (float)(klik / 2); return; }


        //------------------------------------------------------------------- AUTOPILOT ----------------------------------------------------------------------------------------

        if (start == 0) { left(); }

        namierzanie.transform.LookAt(target);

        kurs = namierzanie.transform.rotation.eulerAngles.y; // kurs oczekiwany
        kursStatku = ship.transform.rotation.eulerAngles.y; // kurs obecny

        if ((kursStatku >= (kurs - 2)) && (kursStatku <= (kurs + 2)))   // jeśli kurs statku +/- 2 (granica) oczekiwany kurs
        {
            if (autopilotFunkcja == 0) //jeśli statek wpłynął z zzagranicy 
            {
                Debug.Log("autopilotNastaw"); autopilotNastaw(); // zmiana na nastaw przeciwny lewy lub prawy
            }
        }
        else
        {
            autopilotFunkcja = 0; Debug.Log("autopilot zerowanie"); // jeśli kurs jest poza granicą
        }


    }

    #region Operacja na sliderach dla autopilota
    public void autopilotNastaw()
    {
        if (wyborFunkcji == 0) { Debug.Log("right"); right(); wyborFunkcji = 1; autopilotFunkcja = 1; return; }
        if (wyborFunkcji == 1) { Debug.Log("left"); left(); wyborFunkcji = 0; autopilotFunkcja = 1; return; }
    }

    public void left()
    {
        if (start == 0) { speedSlider.value = 1; start = 1; }

        turnSlider.value = (float)(klik * -wychylenie);
        klik = (float)(klik / 2);

        Debug.Log("FLeft");
    }

    public void right()
    {
        if (start == 0) { speedSlider.value = 1; start = 1; }

        turnSlider.value = (float)(klik * wychylenie);
        klik = (float)(klik / 2);

        Debug.Log("FRight");


    }


    #endregion

    #region Operacje na sliderach dla sterowania ręcznego
    public void addSpeed(float newSpeed)  // pobranie  wartości szybkości ze slidera, który ma zakres od -100 do 100 (%), a więc należało je odpowiednio obniżyć
                                          // przy czym ustalona wartośc statku w tył = ~ 25 % mocy statku w przód ( m.in. opory)
    {
        if (newSpeed < 0) speedValue = newSpeed / 48;
        else
        { speedValue = newSpeed / 12; }
    }


    public void addTurn(float newTurn) // pobranie  wartości skrętu ze slidera
    {
        turnSpeedValue = newTurn / 24;
    }


    public void resetSpeed() // zresetowanie wartosci slidera szybkości
    {
        speedSlider.value = 0;
    }


    public void resetTurn() // zresetowanie wartosci slidera skrętu
    {
        turnSlider.value = 0;
    }



    #endregion

    #region Wysyłanie parametrów do zapisu

    // wysyłanie danych do skryptu, który je zapisuje
    public float GiveRot()
    {
        return rot;
    }

    public float GiveOrderRot()
    {
        return (turnSpeedValue / 2);
    }

    public float GiveCog()
    {
        return cog;
    }

    public float GiveSpeed()
    {
        return speed;
    }
    #endregion

}