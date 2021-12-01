// Skrypt do obrotu statkiem przed zaczęciem symulacji 
using UnityEngine;

public class menuRotate : MonoBehaviour {

    float x, y, z;

	void Start () {
        x = transform.eulerAngles.x;
        z = transform.eulerAngles.z;      
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        y = transform.eulerAngles.y;

        y = y + 1;
        transform.eulerAngles = new Vector3(x, y , z);
    }
}
