using UnityEngine;
using UnityEngine.UI;
using System;
using DLL_NOMOTO1;

public class NOMOTO : MonoBehaviour
{

    public Slider speedSlider, turnSlider, autopilotUPDOWN, autopilotLEFTRIGHT;
    public Text rotText, cogText;
    public GameObject targetOnMap, takeCourseElement;

    float speed = 0;
    double x, z;
    // NOMOTO zmienne  
    float SigmaR = 0, rot = 0, T = 30.66F, K = 0.5F, sigmaC = 0.0F, cog, speedValue = 0, turnSpeedValue = 0;
    float wypiszCOG, autopilotFunkcja = 0;
    bool autopilotONOFF = false, showTargetONOFF = false;


    //---------------------------------------------------------------- Zmienne autopilot ---------------------------------------------------------------------------------------------------------
    public Transform target, namierzanie, ship;
    float kurs, kursStatku, kursObliczony = 180F, kursStatkuObliczony;
    float wyborFunkcji = 0, start = 0, targetHeight, targetX, targetZ;
    float roznica;





    void Start()
    {
        GetComponent<SetCoordinates>().SetModeCoordinates(); // Włączenie skryptu ustalającego tryb symulacji, statek etc.      
        cog = NOMOTOuser.COGStart(transform); // pobranie wartości COG, jako wartość RotationY statku    
    }

    void FixedUpdate()
    {

        // dążenie do wartości z opóźnieniem (aktualna wartość, do której chcemy dążyć, zmiana w czasie)
        speed = NOMOTOuser.speedCalc(speed, speedValue);



        // Zaokrąglanie ROTa, IFy potrzebne w przypadkach, gdy np. rot dażąc do 1 zapisywał pokolei: 0,9845, 0,99845, 0,999845... dążąc do wartości, ale jej nie osiągając

        #region Zaokrąglanie ROTA
        if ((rot < 0.001 && rot > 0) || (rot > -0.001 && rot < 0))
        {
            rot = NOMOTOuser.ROTzero();
        }

        if (((rot > ((turnSpeedValue * K) - 0.001)) && (rot < turnSpeedValue * K)) || ((rot < ((turnSpeedValue * K) + 0.001)) && (rot > turnSpeedValue * K)))
        {
            rot = NOMOTOuser.ROTmax(turnSpeedValue, K);
        }
        #endregion


        // skręt możliwy tylko w wypadku, gdy łódź płynie 
        if (speed != 0)
        {
            SigmaR = NOMOTOuser.SigmaRCalc(SigmaR, turnSpeedValue);
            rot = NOMOTOuser.ROTCalc(cog, sigmaC, SigmaR, rot, K, T);
            cog = NOMOTOuser.COGCalc(cog, rot);  // COG = obecny COG + obecny ROT/min;          
        }



        NOMOTOuser.nadanieSzybkosci(speed, transform); // ruch do przodu o szybkości "speed", dla lokalnych osi
        transform.rotation = NOMOTOuser.obrot(cog, transform); // obrót do wartości COG

        #region Ustawienia COG do wypisania   
        if ((cog > 360)) { cog = 0; }
        if (cog < 0) { cog = 360 + cog; }


        if (cog >= 0 && cog < 90) { wypiszCOG = 270 + cog; }
        else { wypiszCOG = cog - 90; }


        // wypisanie wartości na ekranie
        rotText.text = "ROT: " + Math.Round(rot * 60, 3).ToString() + " ° / min";
        cogText.text = "COG: " + Math.Round(wypiszCOG, 3).ToString() + " °";
        #endregion





        //------------------------------------------------------------------- AUTOPILOT ----------------------------------------------------------------------------------------

        if (autopilotONOFF)
        {

            NOMOTOautopilot.namierzanie(namierzanie, target);

            kurs = NOMOTOautopilot.kurs(namierzanie);
            kursStatku = NOMOTOautopilot.kursStatku(ship);

            targetHeight = NOMOTOautopilot.targetHeight(takeCourseElement);
            targetX = NOMOTOautopilot.targetX(target);
            targetZ = NOMOTOautopilot.targetZ(target);

            target.position = NOMOTOautopilot.targetPosition(targetX, targetHeight, targetZ); // Ustawienie wysokości celu autopilota taka jak statek, który unosi się na fali

            // Wyliczanie nowego układu odniesienia, dla którego nasz cel autopilota ma stałą wartość -/+ 180, elementy po lewej od niego przyjmują wartość (-180 do 0), a po prawej (180 do 0)
            // Upraszczając, gdy mamy układ standardowy na kompasie NSWE, to naszym N jest +/- 180, S = 0, W = -90, E = 90

            if (kursStatku >= kurs) { kursStatkuObliczony = (kursStatku - kurs); }
            else { kursStatkuObliczony = (360 - kurs) + kursStatku; }

            if (kursStatkuObliczony >= 0 && kursStatkuObliczony <= 180) kursStatkuObliczony = 180 - kursStatkuObliczony;
            else kursStatkuObliczony = -(kursStatkuObliczony - 180);

            if (kursStatkuObliczony >= 0) roznica = -(kursObliczony - kursStatkuObliczony);    // kursObliczony zawsze 180 (nowa współrzedna celu) 
            else roznica = kursObliczony + kursStatkuObliczony;


            if (kursStatkuObliczony >= 0) left();
            else right();

            if (Vector3.Distance(transform.position, target.transform.position) < 30) { autopilotStartStop(); } // Jeśli nasz statek jest o 30 jednostek od celu autopilot kończy pracę, a nastawy statku są zerowane

            showTargetONOFF = true;

        }
    }

    #region Operacje dla autopilota

    public void showHideTarget()
    {
        showTargetONOFF = !showTargetONOFF;

        if (showTargetONOFF == true || autopilotONOFF == true) { targetOnMap.SetActive(true); takeCourseElement.SetActive(true); autopilotUPDOWN.interactable = true; autopilotLEFTRIGHT.interactable = true; }
        else { targetOnMap.SetActive(false); takeCourseElement.SetActive(false); autopilotUPDOWN.interactable = false; autopilotLEFTRIGHT.interactable = false; }


    }

    public void autopilotStartStop()
    {
        autopilotONOFF = !autopilotONOFF;


        if (showTargetONOFF == true || autopilotONOFF == true) { targetOnMap.SetActive(true); takeCourseElement.SetActive(true); autopilotUPDOWN.interactable = true; autopilotLEFTRIGHT.interactable = true; }
        else { targetOnMap.SetActive(false); takeCourseElement.SetActive(false); autopilotUPDOWN.interactable = false; autopilotLEFTRIGHT.interactable = false; }

        if (!autopilotONOFF)
        {
            speedSlider.value = 0;
            turnSlider.value = 0;
            start = 0;
        }
    }

    public void left()
    {
        if (start == 0)
        {
            speedSlider.value = 1;
            start++;
        }

        turnSlider.value = roznica * 2.5f;
    }

    public void right()
    {
        if (start == 0)
        {
            speedSlider.value = 1;
            start = 1;
        }

        turnSlider.value = roznica * 2.5f;
    }


    #endregion

    #region Operacje dla sterowania ręcznego
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