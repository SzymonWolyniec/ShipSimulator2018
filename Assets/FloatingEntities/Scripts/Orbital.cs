using UnityEngine;
using System.Collections;

public class Orbital : MonoBehaviour
{
  public Transform target;
  Vector3 lastPosition;
  Vector3 direction;
  float distance;

  Vector3 movement;
  Vector3 rotation;

	void Awake ()
  {
    direction = new Vector3(0, 0, 0);
    transform.SetParent(target);
        transform.rotation = target.transform.rotation;
    lastPosition = Input.mousePosition;

  }
	
	void Update ()
    {
    Vector3 mouseDelta = Input.mousePosition - lastPosition;
        
     if (Input.GetMouseButton(0))
    movement = movement + new Vector3(mouseDelta.x * 0.1f, mouseDelta.y * 0.05f, 0F);
    movement.z = movement.z + Input.GetAxis("Mouse ScrollWheel") * -2.5F;
        
    rotation = rotation + movement;
    rotation.x = rotation.x % 360.0f;
    rotation.y = Mathf.Clamp(rotation.y, -80F, -10F);
        
    direction.z = Mathf.Clamp(direction.z + movement.z * 10 , 50F, 200F); // w nawiasach: szybkość przybliżania/oddalania, minimalna odległość, maksymalna odległość
    transform.position = target.position + Quaternion.Euler(rotation.y, rotation.x, 0) * direction;
    transform.LookAt(target.position);

    lastPosition = Input.mousePosition;
    movement = movement* 0.9F;

        
    }
}
