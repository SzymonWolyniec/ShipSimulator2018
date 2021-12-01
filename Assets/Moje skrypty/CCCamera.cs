// Skrypt obsługujący sterowanie za pomocą przycisków

using UnityEngine;


public class CCCamera : MonoBehaviour {

    public GameObject MainCam;
    public GameObject MainCamFree;
    public GameObject BridgeCam;
    public GameObject NoseCam;
    public GameObject Sonar;

    public Camera maxCam;
    public Light[] LightsShip;
    public GameObject DataPanel;

    public GameObject driveHolder;
    public GameObject settingsHolder;
    public GameObject autopilotHolder;

    //  public GameObject CamsObject;

    int camFree = 0;
    int camX = 0; // zmienna odpowiedzialna za wybór kamery
    int maxMapX = 0; // zmienna odpowiedzialnca za włączenie / wyłączenie mapy
    int panelX = 1; // zmienna odpowiedzialna za włączenie / wyłączenie panelu z danymi
    public int menuX = 0; // zmienna odpowiedzialna za przechodzenie między interfejsem symulacji, a menu w czasie symulacji
    int sonarONOFF = 1;
    
   


    void Update () {



        if (Input.GetKeyDown(KeyCode.X)) // Wł/Wył sonar
        {
            if (sonarONOFF == 0) { Sonar.SetActive(true); sonarONOFF = 1; return; }
            if (sonarONOFF == 1) { Sonar.SetActive(false); sonarONOFF = 0; return; }
        }



        if (Input.GetKeyDown(KeyCode.M))
        {
            if (maxMapX == 0) { maxCam.enabled = true; autopilotHolder.SetActive(true); maxMapX = 1; return; }
            if (maxMapX == 1) { maxCam.enabled = false; autopilotHolder.SetActive(false); maxMapX = 0; return; }
        }

        if (Input.GetKeyDown(KeyCode.C))

        {

            if (camX == 3)
            { camX = 0; }



            if (camX == 2) MainCamOn();
            if (camX == 0) BridgeCamOn();
            if (camX == 1) NoseCamOn();

            camX++;


            return;

        }

        if (Input.GetKeyDown(KeyCode.F)) // Wł/Wył kamery sterowanej myszą

        {
            if (MainCam.activeSelf || MainCamFree.activeSelf)
            {
                if (camFree == 1) { MainCam.SetActive(true); MainCamFree.SetActive(false); camFree = 0; return; }
                if (camFree == 0) { MainCamFree.SetActive(true); MainCam.SetActive(false); camFree = 1; return; }
            }


            return;

        }

        if (Input.GetKeyDown(KeyCode.L)) // Wł/Wył światła na statku
        {
            for (int i = 0; i < LightsShip.Length; i++)
            {
                LightsShip[i].enabled = !LightsShip[i].enabled;
            }



            return;
        }

        if (Input.GetKeyDown(KeyCode.D)) // Wł/Wył informacji takich jak COG,ROT etc.
        {
            if (panelX == 0) { DataPanel.SetActive(true); panelX = 1; return; }
            if (panelX == 1) { DataPanel.SetActive(false); panelX = 0; return; }
        }


    }

    public void MainCamOn()
    {
        MainCam.SetActive(true);
        BridgeCam.SetActive(false);
        NoseCam.SetActive(false);

    }

    public void BridgeCamOn()
    {
        MainCam.SetActive(false);
        BridgeCam.SetActive(true);
        NoseCam.SetActive(false);

    }


    public void NoseCamOn()
    {
        MainCam.SetActive(false);
        BridgeCam.SetActive(false);
        NoseCam.SetActive(true);

    }
}