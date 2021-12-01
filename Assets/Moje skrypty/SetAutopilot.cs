// Skrypt do ustawiania pozycji celu dla autopilota
using UnityEngine;

public class SetAutopilot : MonoBehaviour {

    public float x, y, z, nx, nz;



    void Start () {

        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        nx = x;
        nz = z;
    }

    public void setUPDOWN(float sliderValueX)
    {       
        transform.position = new Vector3((x + sliderValueX), y, nz);
        nx = transform.position.x;
    }

    public void setLEFTRIGHT(float sliderValueZ)
    {     
        transform.position = new Vector3(nx, y, (z + (-sliderValueZ)));
        nz = transform.position.z;
    }

}
