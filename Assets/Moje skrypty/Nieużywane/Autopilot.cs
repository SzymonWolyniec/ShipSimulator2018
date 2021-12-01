using UnityEngine;
using UnityEngine.UI;


public class Autopilot : MonoBehaviour {

    public Transform target, ship;
    public Slider turnSlider;
    public float kurs, kursStatku;
    float klik = 1, wychylenie = 100, wyborFunkcji = 0;

    private void Start()
    {
        right();
    }

    void Update()
    {



        transform.LookAt(target);

        kurs = transform.rotation.eulerAngles.y; // kurs oczekiwany
        kursStatku = ship.transform.rotation.eulerAngles.y; // kurs obecny

        if ((kursStatku >= kursStatku - 2) || (kursStatku <= kursStatku + 2)) ;
        {
            if (wyborFunkcji == 0) { left();  wyborFunkcji = 1; return; }
            if (wyborFunkcji == 1) { left();  wyborFunkcji = 0; return; }
        }



        //if (target.position.y > 90 && target.position.y < 180) target.position = new Vector3(target.position.x, target.position.y - 360, target.position.z);

    }

    public void right() 
    {
        turnSlider.value = (float)(klik * -wychylenie);
        klik = (float)(klik / 2); 
    }

    public void left() 
    {
        turnSlider.value = (float)(klik * wychylenie);
        klik = (float)(klik / 2);
    }
}


