using UnityEngine;
using UnityEngine.UI;

public class ShowCoordinates : MonoBehaviour {

    public Text txt;
    double NS, NSStopien = 0, NSMinuta = 0, NSSekunda = 0;
    double WE, WEStopien = 0, WEMinuta = 0, WESekunda = 0;
    string NSStopienText, NSMinutaText, NSSekundaText, WEStopienText, WEMinutaText, WESekundaText;
    string coordinates, showCoordinates;

    // Wymiary mapy 3km x 2.5 km
    // Współrzędne Morza Norweskiego

    void FixedUpdate()
    {

        // Dla kierunków N/S wyliczanie realnego poruszania się tzn. 1 km = x stopni - jak w rzeczywistości  dla danych danych współrzędnych
        
        if (transform.position.x > 0)
        {
            NS = 67.421925 + ((0.02106744 / 2141) * transform.position.x);
        }
        else
        {
            NS = 67.421925 - ((0.00135792 / 138) * (-transform.position.x));
        }
        
        // Przeliczanie na stopnie, minuty, sekundy
        
        NSStopien = NS - (NS % 1);
        NS = (NS - NSStopien) * 60;

        NSMinuta = NS - (NS % 1);
        NS = (NS - NSMinuta) * 60;

        NSSekunda = NS - (NS % 1);



        // Dla kierunków W/E

        if (transform.position.z > 0)
        {
            WE = 5.250450 - ((0.0322436 / 2705) * (transform.position.z));
        }
        else
        {
            WE = 5.250450 + ((0.00147808 / 124) * (-transform.position.z));
        }


        WEStopien = WE - (WE % 1);
        WE = (WE - WEStopien) * 60;

        WEMinuta = WE - (WE % 1);
        WE = (WE - WEMinuta) * 60;

        WESekunda = WE - (WE % 1);

        // Zapisywanie w czasie zamiast np. 1:20:3 to:  01:20:03

        if (NSSekunda < 10) { NSSekundaText = "0" + NSSekunda.ToString(); } else { NSSekundaText = NSSekunda.ToString(); }
        if (NSMinuta < 10) { NSMinutaText = "0" + NSMinuta.ToString(); } else { NSMinutaText = NSMinuta.ToString(); }
        if (NSStopien < 10) { NSStopienText = "0" + NSStopien.ToString(); } else { NSStopienText = NSStopien.ToString(); }
        if (WESekunda < 10) { WESekundaText = "0" + WESekunda.ToString(); } else { WESekundaText = WESekunda.ToString(); }
        if (WEMinuta < 10) { WEMinutaText = "0" + WEMinuta.ToString(); } else { WEMinutaText = WEMinuta.ToString(); }
        if (WEStopien < 10) { WEStopienText = "0" + WEStopien.ToString(); } else { WEStopienText = WEStopien.ToString(); }

        // tekst wyswietlany
        showCoordinates = NSStopienText + "°" + NSMinutaText + "'" + NSSekundaText + "″N   " + WEStopienText + "°" + WEMinutaText + "'" + WESekundaText + "″E";

        // tekst wysyłany do zapisu
        coordinates = "N" + NSStopienText + "°" + NSMinutaText + "'" + NSSekundaText + "″   E" + WEStopienText + "°" + WEMinutaText + "'" + WESekundaText + "″";

        txt.text =  showCoordinates;


    }

    public string GiveCoordinates()
    {
        return coordinates;  // pobranie współrzędnych do zapisu
    }
}

