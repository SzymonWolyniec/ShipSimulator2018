using UnityEngine;

 // Zmiana między kamerą za statkiem, na mostku i na nosie statku

public class Cams : MonoBehaviour {


    public GameObject MainCam;
    public GameObject BridgeCam;
    public GameObject NoseCam;
    

    public GameObject CamsObject;

    int x = 0;

    public void MainCamOn ()
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


    public void CamObjectOn() // Zamyknie i otwieranie okna wyboru kamer
    {
       if (x == 0) { CamsObject.SetActive(true); x = 1; return; }
       if (x == 1) { CamsObject.SetActive(false); x = 0; return; }

    }
}
