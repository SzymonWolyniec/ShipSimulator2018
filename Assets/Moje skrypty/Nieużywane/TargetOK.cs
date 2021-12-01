using UnityEngine;

public class TargetOK : MonoBehaviour
{
    Collider m_Collider;
    Vector3 m_Center;
    Vector3 m_Size, m_Min, m_Max;
    Vector3 minY, maxY;
    float x;

    public GameObject kontenerowiec;
    public GameObject targetGREEN;
  



    void Start()
    {
       
        OutputData();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(kontenerowiec.transform.position, targetGREEN.transform.position) < 8 ) { Debug.Log("Elo"); }
        else { Debug.Log("No"); }

        
    }

    void OutputData()
    {

        
      
    }
}