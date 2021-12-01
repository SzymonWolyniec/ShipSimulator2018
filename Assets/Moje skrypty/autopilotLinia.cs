// Skrypt służący do rysowania lini od statku do celu ustawionego na autopilocie

using UnityEngine;

public class autopilotLinia : MonoBehaviour
{

    public GameObject gameObject1;          
    public GameObject gameObject2;         

    private LineRenderer line;                         

    // Use this for initialization
    void Start()
    {
        // Dodanie LineRenderer do statku
        line = this.gameObject.AddComponent<LineRenderer>();
        // Ustawienie szerokości linii
        line.SetWidth(1f, 5f);
        // Ustawienie liczby verteksów dla Line Renderer
        line.SetVertexCount(2);
        line.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));  // "Particles/Additive" - półprzezroczyste
        line.SetColors(Color.blue, Color.blue);
    }

    void Update()
    {
    
        if (gameObject1 != null && gameObject2 != null)
        {
            
            line.SetPosition(0, gameObject1.transform.position);
            line.SetPosition(1, gameObject2.transform.position);
        }
    }
}