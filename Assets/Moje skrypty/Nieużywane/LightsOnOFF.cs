using UnityEngine;

public class LightsOnOFF : MonoBehaviour
{

    // Włączanie / Wyłączanie światła na statku

    //int change = 0;
    public Light[] LightsShip;


   public void TurnOnOff()

    {

        for (int i = 0; i < LightsShip.Length; i++)
        {
            LightsShip[i].enabled = !LightsShip[i].enabled;
        }
        return;
    }
}
