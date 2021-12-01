// Skrypt używany do nawigacji po menu w czasie symulacji
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuStart : MonoBehaviour
{

    public GameObject mainMenuHolder;
    public GameObject settingsHolder;
    public GameObject controlsHolder;
    public GameObject Rain;

    public Material skyboxDay;
    public Material skyboxNight;
    int skychange = 0;
    int miniMapCam = 1;
    public int rainX = 0;

    public Light Light;
   
    
    public Camera miniCam;




    public void BackMenu() // Ładowanie Menu głównego
    {
        SceneManager.LoadScene("SymulatoryLabo");
    }

    public void skyboxChangeNight()  // Zmiana dnia na noc
    {


        if (skychange == 0)
        {
            RenderSettings.skybox = skyboxNight;    // Ustawienie skyboxa nocnego
            skychange = 1;                          // Zmienna przetrzymująca informację czy panuje dzień czy noc
            Light.intensity = 0.001f;    // Włączanie / wyłączanie światła

            


            return;
        }

        if (skychange == 1)                          // Opcja dla przejścia z nocy na dzień
        {
            RenderSettings.skybox = skyboxDay;
            skychange = 0;
            Light.intensity = 1f;

            return;
        }

    }

    public void Quit()    // Wyjście z aplikacji
    {
        Application.Quit();
    }

    public void Settings(){   // Przejście z interfejsu symulacji do menu w czasie symulacji
        mainMenuHolder.SetActive(false);
        settingsHolder.SetActive(true);
    }

    public void MainMenu()  // Przejście z menu w czasie symulacji do interfejsu symulacji
    {
        mainMenuHolder.SetActive(true);
        settingsHolder.SetActive(false);
    }

    public void controlsToSettings()  // Przejście z menu w czasie symulacji do zakładki ze skrótami klawiszowymi
    {
        controlsHolder.SetActive(false);
        settingsHolder.SetActive(true);
    }

    public void settingsToControl()  // Przejście z zakładki ze skrótami klawiszowymi do menu w czasie symulacji
    {
        controlsHolder.SetActive(true);
        settingsHolder.SetActive(false);
    }



    public void MiniMapCamera() // Włączanie / wyłączanie małej mapy

    {
        if (miniMapCam == 0) { miniCam.enabled = true; miniMapCam = 1; return; }
        if (miniMapCam == 1) { miniCam.enabled = false; miniMapCam = 0; return; }

    }


 

    public void RainOnOff() // Włączanie / wyłączanie (obiektu ze skryptem deszczu) deszczu
    {
        if (rainX == 0) { Rain.SetActive(true); rainX = 1; return; }
        if (rainX == 1) { Rain.SetActive(false); rainX = 0; return; }
    }


}
