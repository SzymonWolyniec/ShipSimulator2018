// Skrypt do ustawiania statku w danym punkcie dla wybranego w menu przed symulacją trybu PORT oraz FREE
// Dla trybu PORT określany jest port docelowy oraz w momencie, gdy statek jest w wyznaczonym celu, a jego prędkość jest nieduża symulacja zostaje zatrzymana
using UnityEngine;

public class SetCoordinates : MonoBehaviour
{

    public int mode, coordinates, PORTto;
    public GameObject targetBLUE;
    public GameObject targetGREEN;
    public GameObject targetRED;
    public GameObject targetAreaBLUE;
    public GameObject targetOK;
    GameObject OnOffTargetObject;
    GameObject TargetObject;
    int ONOFF = 0;

    public Speedometer _Speedometer; // pobranie szybkości statku
    public double speed;

    public void SetModeCoordinates()
    {

        mode = PlayerPrefs.GetInt("ModeKontenerowiec");
        coordinates = PlayerPrefs.GetInt("PlaceKontenerowiec", coordinates);
        PORTto  = PlayerPrefs.GetInt("ToPORT");

        if (mode == 0) kontenerowiecFREE(); // Mode FREE
        if (mode == 1) kontenerowiecPORT(); // Mode PORT

        
    }

    public void TargetOption() // On/Off Target, Ustalanie odległości od konkrentego portu docelowego
    {
        speed = _Speedometer.GiveSpeed();

        if ((Vector3.Distance(transform.position, TargetObject.transform.position) < 8) && (speed < 5)) // jeśli statek jest odpowiednio blisko obiektu && jego szybkość jest mniejsza od 5
        {

            Time.timeScale = 0;
            OnOffTargetObject.SetActive(true);
            targetOK.SetActive(true);
        }

        if (ONOFF == 0) { OnOffTargetObject.SetActive(true); ONOFF = 1; return; }
        if (ONOFF == 1) { OnOffTargetObject.SetActive(false); ONOFF = 0; return; }


    }

    public void kontenerowiecPORT() // Ustawianie statku w porcie wyjściowym, określanie portu docelowego (w trybie PORT)
    {
        if (coordinates == 0) // Start Port : BLUE
        {

            // Testowy, dla wybranego portu startowego niebieskiego, docelowego zielonego, statek jest chwilę przed wpłynięciem do portu zielonego
           // transform.position = new Vector3(132.63f, 3, 2245.9f);
           // transform.eulerAngles = new Vector3(0, 0, 0);


             transform.position = new Vector3(1854.9f, 3, 2491.3f);
             transform.eulerAngles = new Vector3(0, 220.2f, 0);

            if (PORTto == 1) { OnOffTargetObject = targetGREEN; TargetObject = targetGREEN; }       // End Port : GREEN
            if (PORTto == 2) { OnOffTargetObject = targetRED; TargetObject = targetRED; }         // End Port : RED
        }

        if (coordinates == 1) // Start Port : GREEN
        {
            transform.position = new Vector3(136, 3, 2404.9f);
            transform.eulerAngles = new Vector3(0, 180, 0);

            if (PORTto == 0) { OnOffTargetObject = targetBLUE; TargetObject = targetAreaBLUE; }    // End Port : BLUE
            if (PORTto == 2) { OnOffTargetObject = targetRED; TargetObject = targetRED; }         // End Port : RED
        }

        if (coordinates == 2) // Start Port : RED
        {
            transform.position = new Vector3(1635, 3, 248.7f);
            transform.eulerAngles = new Vector3(0, -90, 0);

            if (PORTto == 0) { OnOffTargetObject = targetBLUE; TargetObject = targetAreaBLUE; }    // End Port : BLUE
            if (PORTto == 1) { OnOffTargetObject = targetGREEN; TargetObject = targetGREEN; }       // End Port : GREEN
        }


        InvokeRepeating("TargetOption", 0, 1); // start funkcji natychmiast (0s), powtarzanie co 1s
    }


    public void kontenerowiecFREE() // Ustawianie statku w danym punkcie (dla trybu FREE)
    {
        if (coordinates == 0) // Kontenerowiec, FREE, Place: A
        {
            transform.position = new Vector3(1854.9f, 3, 2491.3f);
            transform.eulerAngles = new Vector3(0, 220.2f, 0);
        }

        if (coordinates == 1)  // Kontenerowiec, FREE, Place: B
        {
            transform.position = new Vector3(915.6f, 3, 2466);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (coordinates == 2)  // Kontenerowiec, FREE, Place: C
        {
            transform.position = new Vector3(136, 3, 2404.9f);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (coordinates == 3)  // Kontenerowiec, FREE, Place: D
        {
            transform.position = new Vector3(1861, 3, 1758.2f);
            transform.eulerAngles = new Vector3(0, 270, 0);
        }

        if (coordinates == 4)  // Kontenerowiec, FREE, Place: E
        {
            transform.position = new Vector3(977.9f, 3, 1758.2f);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (coordinates == 5)  // Kontenerowiec, FREE, Place: F
        {
            transform.position = new Vector3(638.1f, 3, 1629.8f);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (coordinates == 6)  // Kontenerowiec, FREE, Place: G
        {
            transform.position = new Vector3(1434, 3, 1228);
            transform.eulerAngles = new Vector3(0, 270, 0);
        }

        if (coordinates == 7)  // Kontenerowiec, FREE, Place: H
        {
            transform.position = new Vector3(865, 3, 1075);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (coordinates == 8)  // Kontenerowiec, FREE, Place: I
        {
            transform.position = new Vector3(1260, 3, 819);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (coordinates == 9)  // Kontenerowiec, FREE, Place: J
        {
            transform.position = new Vector3(60, 3, 889);
            transform.eulerAngles = new Vector3(0, 90, 0);
        }

        if (coordinates == 10)  // Kontenerowiec, FREE, Place: K
        {
            transform.position = new Vector3(407, 3, 417);
            transform.eulerAngles = new Vector3(0, 90, 0);
        }

        if (coordinates == 11)  // Kontenerowiec, FREE, Place: L
        {
            transform.position = new Vector3(59, 3, 149);
            transform.eulerAngles = new Vector3(0, 90, 0);
        }

        if (coordinates == 12)  // Kontenerowiec, FREE, Place: M
        {
            transform.position = new Vector3(1635, 3, 248.7f);
            transform.eulerAngles = new Vector3(0, -90, 0);
        }

    }

}




