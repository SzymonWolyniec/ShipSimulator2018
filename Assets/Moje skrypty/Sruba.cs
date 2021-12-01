// Skrypt do obracania śrubą (w lewą lub prawą stronę, zależnie od tego jak nastawiony jest silnik) oraz do obracania płetwą sterowną
using UnityEngine;
using UnityEngine.UI;

public class Sruba : MonoBehaviour {


    public NOMOTO _NOMOTO;
    public Slider turnSlider;
    public GameObject sruba;
    public GameObject ster;

    float  speed, turn;
       	
	
	void Update () {

        speed = _NOMOTO.GiveSpeed() * 5; // pobrenie prędkości ze skryptu NOMOTO
        turn = turnSlider.value / 2.222f; // pobranie wartości z slidera do skręcanie podzielona przez ( 100 / 45 ), 45 - maksymalny wychył płetwy

        sruba.transform.eulerAngles = sruba.transform.eulerAngles - new Vector3(0, 0, (speed));   // rotacja osi Z wg. szybkosci   
        ster.transform.eulerAngles = new Vector3(0, ((-turn)+180), 0); // skręt płetwy


    }
}
