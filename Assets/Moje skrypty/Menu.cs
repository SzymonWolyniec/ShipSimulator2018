// Skrypt odpowiedzialny za nawigację po menu przed zaczęciem symulacji

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public GameObject mainMenuHolder;
    public GameObject settingsHolder;
    public GameObject shipChooseHolder;
    public GameObject chooseModeKontenerowiecHolder;
    public GameObject choosePlaceKontenerowiecFree;
    public GameObject choosePlaceKontenerowiecPortFrom;
    public GameObject choosePlaceKontenerowiecPortTo;

    public GameObject kontenerowiec;
    public GameObject modelShip;

    public GameObject[] maps;

    public Light mainLight;

    public Toggle[] resolutionToggles;
    public Toggle fullscreenToggle;
    public Toggle[] statekWybor;
    public Toggle[] kontenerowiecTo;

    public int[] screenWidths;
    int activeScreenResIndex;


    public int chooseShip;
    public int chooseModeKontenerowiec;
    public int ChoosePlaceKontenerowiecFreeX;
    public int ChooseKontenerowiecToPort;



    // Skrypt z Menu Głównego


    public void Start()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false; // Sprawdzanie czy włączony jest cały ekran


        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenResIndex;
        }
        SetFullScreen(isFullscreen);

    }



    public void Play() // Ładowanie sceny gry
    {

        PlayerPrefs.SetInt("ModeKontenerowiec", chooseModeKontenerowiec);
        PlayerPrefs.SetInt("PlaceKontenerowiec", ChoosePlaceKontenerowiecFreeX);
        PlayerPrefs.SetInt("ToPORT", ChooseKontenerowiecToPort);

        SceneManager.LoadScene("Symulator Statku 2017 scena");
    }

    public void KontenerowiecTrasaPORT (int x)
    {
        ChooseKontenerowiecToPort = x;
    }
    // Przechodzenie między oknami opcji
    public void KontenerowiecFromTo()
    {
        choosePlaceKontenerowiecPortFrom.SetActive(false);
        choosePlaceKontenerowiecPortTo.SetActive(true);
        
        if (ChoosePlaceKontenerowiecFreeX == 0)
        {
            maps[0].SetActive(true);
            kontenerowiecTo[0].interactable = false;
            kontenerowiecTo[1].isOn = true;
        }

        if (ChoosePlaceKontenerowiecFreeX == 1)
        {
            maps[1].SetActive(true);
            kontenerowiecTo[1].interactable = false;
            kontenerowiecTo[0].isOn = true;
        }

        if (ChoosePlaceKontenerowiecFreeX == 2)
        {
            maps[2].SetActive(true);
            kontenerowiecTo[2].interactable = false;
            kontenerowiecTo[0].isOn = true;
        }
        
    }

    public void BackKontenerowiecFromTo()
    {
        

        choosePlaceKontenerowiecPortFrom.SetActive(true);
        choosePlaceKontenerowiecPortTo.SetActive(false);

        for (int u = 0; u <= 2; u++)
        {
            maps[u].SetActive(false);
            kontenerowiecTo[u].interactable = true;
        }
    }

    public void BackChoosePlaceKontenerowiecFree()
    {
        if (chooseModeKontenerowiec == 0)
        {
            choosePlaceKontenerowiecFree.SetActive(false);
            chooseModeKontenerowiecHolder.SetActive(true);
        }

        if (chooseModeKontenerowiec == 1)
        {
            chooseModeKontenerowiecHolder.SetActive(true);
            choosePlaceKontenerowiecPortFrom.SetActive(false);
        }
    }


    public void ChoosePlaceKontenerowiecFree(int x)
    {
        ChoosePlaceKontenerowiecFreeX = x;
    }
    

    public void modeKontenerowiecToChoosePlace()
    {

        ChoosePlaceKontenerowiecFreeX = 0;

        if (chooseModeKontenerowiec == 0)
        {
            chooseModeKontenerowiecHolder.SetActive(false);
            choosePlaceKontenerowiecFree.SetActive(true);

        }

        if(chooseModeKontenerowiec == 1)
        {
            chooseModeKontenerowiecHolder.SetActive(false);
            choosePlaceKontenerowiecPortFrom.SetActive(true);

        }

    }

    public void ChooseModeKontenerowiec(int x)
    {
        chooseModeKontenerowiec = x;
    }


    public void backChooseModeKontenerowiec()
    {
        chooseModeKontenerowiecHolder.SetActive(false);
        ChooseShip();
    }

    public void goChooseGameMode()
    {
        kontenerowiec.SetActive(false);
       

        if (chooseShip == 0)
        {
            shipChooseHolder.SetActive(false);
            chooseModeKontenerowiecHolder.SetActive(true);
        }

    }


    public void ChooseShip(int x)
    {
        chooseShip = x;
    }


    public void ChooseShip()
    {
        mainMenuHolder.SetActive(false);
        shipChooseHolder.SetActive(true);

        kontenerowiec.SetActive(true);
        
        modelShip.SetActive(false);
        mainLight.intensity = 0.25f;
    }

    public void BackChooseShip()
    {
        mainMenuHolder.SetActive(true);
        shipChooseHolder.SetActive(false);

        kontenerowiec.SetActive(false);
   
        modelShip.SetActive(true);

        mainLight.intensity = 1;
    }

    public void Quit() // Wyjście z symulatora
    {
        Application.Quit();
    }

    public void Settings() // Przełączanie między menu, a ustawieniami
    {
        mainMenuHolder.SetActive(false);
        settingsHolder.SetActive(true);
    }

    public void MainMenu()
    {
        mainMenuHolder.SetActive(true);
        settingsHolder.SetActive(false);
    }

    // Ustawianie rozdzielczości

    public void ScreenResoultion(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }




    public void SetFullScreen(bool isFUllScreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFUllScreen;
        }

        if (isFUllScreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            ScreenResoultion(activeScreenResIndex);
        }

        PlayerPrefs.SetInt("fullscreen", ((isFUllScreen) ? 1 : 0));
        PlayerPrefs.Save();

    }

    

}