using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class SaveParameters : MonoBehaviour {
    
    // Zapis do pliku danych

    string FILE_NAME = "SimulatorLog.txt"; // Nazwa pliku w naszym projekcie zawierający zapisane dane

    // skrypty z których będą pobierane dane
    public Text time;
    public Speedometer _Speedometer;
    public CompasText _CompasText;
    public ShowCoordinates _ShowCoordinates;
    public NOMOTO _NOMOTO;
    public PowerValue _PowerValue;
    public TurnValue _TurnValue;

    double saveTime = 2; // zapis co ile sekund (tu: 2)
    int stepZapis = 0, stepTime = 0;
    int hour = 0, min = 0, sec = 0;
    string hourText, minText, secText, enginePower, rudder;

    // Zmienne z innch skryptów
    double speed = 0, arrowRotation = 0, rot = 0, rotOrder = 0, cog = 0;
    string coordinates;


    // Use this for initialization
    void Start () {


        // Czyszczenie pliku i zapis na początku nazw zmiennych 

        File.WriteAllText(FILE_NAME, "Time");
        StreamWriter sw = File.AppendText(FILE_NAME); // otwarcie Stream Writera
        sw.Write("\t\t\t");
        sw.Write("SpeedKN");
        sw.Write("\t\t\t");
        sw.Write("SpeedKM");
        sw.Write("\t\t\t");
        sw.Write("Course");
        sw.Write("\t\t\t");
        sw.Write("Coordinates");
        sw.Write("\t\t\t");
        sw.Write("ROT");
        sw.Write("\t\t\t");
        sw.Write("OrderROT");
        sw.Write("\t\t\t");
        sw.Write("COG");
        sw.Write("\t\t\t");
        sw.Write("Engine");
        sw.Write("\t\t\t");
        sw.Write("Rudder");

        sw.Write(Environment.NewLine); // przejście do nowej lini
        sw.Close(); // zamknięcie Stream Writera
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        stepZapis++;

        // zapis czasu za pomocą wiedzy, iż FixedUpdate = 0.02 s
        if (stepZapis % 50 == 0) { sec++; }
        if (stepZapis % 3000 == 0) { sec = 0;  min++; }
        if (stepZapis % 180000 == 0) { min = 0; hour++; }

        
        // odpowiedni zapis czasu - 00:02:01 zamiast 00:2:1
        if (sec < 10) { secText = "0" + sec.ToString(); } else { secText = sec.ToString(); }
        if (min < 10) { minText = "0" + min.ToString(); } else { minText = min.ToString(); }
        if (hour < 10) { hourText = "0" + hour.ToString(); } else { hourText = hour.ToString(); }

        time.text = "Time: " + hourText + ":" + minText + ":" + secText;

        // pobieranie zmiennych z innych skryptów

        speed = _Speedometer.GiveSpeed();
        arrowRotation = _CompasText.GiveHeading();
        coordinates = _ShowCoordinates.GiveCoordinates();
        rot = _NOMOTO.GiveRot();
        rotOrder = _NOMOTO.GiveOrderRot();
        cog = _NOMOTO.GiveCog() - 1.6f;
        enginePower = _PowerValue.GetEnginePower();
        rudder = _TurnValue.GetRudder();



        // zapis co ustawioną liczbę sekund

        if ((stepZapis % (saveTime * 50)) == 0) 
        {
            StreamWriter sw = File.AppendText(FILE_NAME);


            /// if + 00, 
            sw.Write(hourText + ":" + minText + ":" + secText);
            sw.Write("\t\t");
            sw.Write(Math.Round(speed / 1.852, 3));
            sw.Write("\t\t");
            sw.Write(Math.Round(speed, 2));
            sw.Write("\t\t\t");
            sw.Write(Math.Round(arrowRotation, 2));
            sw.Write("\t\t\t");
            sw.Write(coordinates);
            sw.Write("\t\t\t");
            sw.Write(Math.Round(rot, 2));
            sw.Write("\t\t\t");
            sw.Write(Math.Round(rotOrder, 2));
            sw.Write("\t\t\t");
            sw.Write(Math.Round(cog, 2));
            sw.Write("\t\t\t");
            sw.Write(enginePower);
            sw.Write("\t\t\t");
            sw.Write(rudder);

            sw.Write( Environment.NewLine);
            sw.Close();
        }

        
    }
}
