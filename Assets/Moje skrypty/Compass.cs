using UnityEngine;

public class Compass : MonoBehaviour {

    public GameObject rotationFrom;
    public GameObject rotationTo;

    // Obrót kompasu = odwrtonosci obrotu statku
    
	
	
	void FixedUpdate () {

        rotationTo.transform.eulerAngles = new Vector3(0,0 ,  (rotationFrom.transform.eulerAngles.y - 63F));


    }
}
