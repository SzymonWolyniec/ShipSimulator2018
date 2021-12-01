using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
   

    // Use this for initialization
   public void zmiana ()
    {
       

        Debug.Log("Nacisk START");
        SceneManager.LoadScene("Symulator Statku 2017 Scena", LoadSceneMode.Single);
        //SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void wyjdz()
    {
        Debug.Log("Nacisk WYJDZ");
        Application.Quit();
       
    }



}