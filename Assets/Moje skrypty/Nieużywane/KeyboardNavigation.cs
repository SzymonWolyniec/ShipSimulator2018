using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardNavigation : MonoBehaviour
{

    public GameObject MainCam;
    public GameObject BridgeCam;
    public GameObject NoseCam;


   public int x = 0; // zmienna odpowiedzialna za wybór kamery
   int time = 0;

    void Update()
    {



        if (time == 0)
        {

            if (Input.GetKeyDown(KeyCode.M))

            {

                time = 50;
                if (x == 3)
                { x = 0; }



                if (x == 2) MainCamOn();
                if (x == 0) BridgeCamOn();
                if (x == 1) NoseCamOn();

                x++;
                //time = 0;  // odliczanie czasu

            }
        }
        else time--;

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
