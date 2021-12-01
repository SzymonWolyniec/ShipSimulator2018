// Skrypt do poruszania innym statkiem w czasie symulacji, niezależnie od nas
using UnityEngine;

public class MoveTransportShip : MonoBehaviour {


    float speed = 5, turn = 0, firstZ, secondZ;


    private void Start()
    {
        firstZ = transform.position.z;
    }

    void FixedUpdate()
    {
        

        secondZ = transform.position.z;

        turn = turn + (secondZ - firstZ);

        firstZ = secondZ;

        if (turn > 1588) // obliczcanie przebytej drogi wzglęem osi z i w momencie pokonanaia odpowiedniej drogi zawrórcenie statku
        {
            transform.eulerAngles = new Vector3(-90, 90, 0);
            transform.Translate(Vector3.up * 152);
        }

        if (turn < 0) // ponowne zawrócenie - statek pływa w tą i z powrtorem 
        {
            transform.eulerAngles = new Vector3(-90, - 90 , 0);
            transform.Translate(Vector3.up * (152));
        }

        transform.Translate(Vector3.right * Time.deltaTime * speed);

    }
}
