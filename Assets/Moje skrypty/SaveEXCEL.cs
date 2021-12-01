using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.UI;



public class SaveEXCEL : MonoBehaviour
{

    private List<string[]> rowData = new List<string[]>();
    string ruta = " SymulatorZapisCSV.csv";
    string datosCSV = "Time,SpeedKN,SpeednKM,Course,Coordinates,ROT,OrderROT,COG,Engine,Rudder\n";

    // skrypty z których będą pobierane dane
    public Text time;
    public Speedometer _Speedometer;
    public CompasText _CompasText;
    public ShowCoordinates _ShowCoordinates;
    public NOMOTO _NOMOTO;
    public PowerValue _PowerValue;
    public TurnValue _TurnValue;

    double saveTime = 2; // zapis co ile sekund (tu: 2)
    int stepZapis = 0;
    int hour = 0, min = 0, sec = 0;
    string hourText, minText, secText, enginePower, rudder;

    // Zmienne z innch skryptów
    double speed = 0, arrowRotation = 0, rot = 0, rotOrder = 0, cog = 0;
    string coordinates;


    void FixedUpdate()
    {

        stepZapis++;

        // zapis czasu za pomocą wiedzy, iż FixedUpdate = 0.02 s
        if (stepZapis % 50 == 0) { sec++; }
        if (stepZapis % 3000 == 0) { sec = 0; min++; }
        if (stepZapis % 180000 == 0) { min = 0; hour++; }

        // odpowiedni zapis czasu - 00:02:01 zamiast 00:2:1
        if (sec < 10) { secText = "0" + sec.ToString(); } else { secText = sec.ToString(); }
        if (min < 10) { minText = "0" + min.ToString(); } else { minText = min.ToString(); }
        if (hour < 10) { hourText = "0" + hour.ToString(); } else { hourText = hour.ToString(); }

        time.text = "Time: " + hourText + ":" + minText + ":" + secText;


        // zapis co ustawioną liczbę sekund

        if ((stepZapis % (saveTime * 50)) == 0)
        {

            


            

            // pobieranie zmiennych z innych skryptów

            speed = _Speedometer.GiveSpeed();
            arrowRotation = _CompasText.GiveHeading();
            coordinates = _ShowCoordinates.GiveCoordinates();
            rot = _NOMOTO.GiveRot();
            rotOrder = _NOMOTO.GiveOrderRot();
            cog = _NOMOTO.GiveCog() - 1.6f;
            enginePower = _PowerValue.GetEnginePower();
            rudder = _TurnValue.GetRudder();

            var sr = File.CreateText(ruta);



            datosCSV += hourText + ":" + minText + ":" + secText +",";
            datosCSV += Math.Round(speed / 1.852, 3) + ",";
            datosCSV += Math.Round(speed, 2) + ",";
            datosCSV += Math.Round(arrowRotation, 2) + ",";
            datosCSV += coordinates + ",";
            datosCSV += Math.Round(rot, 2) + ",";
            datosCSV += Math.Round(rotOrder, 2) + ",";
            datosCSV += Math.Round(cog, 2) + ",";
            datosCSV += enginePower + ",";
            datosCSV += rudder + "\n"; 
         
            sr.WriteLine(datosCSV);
            sr.Close();
        }

    }

}

