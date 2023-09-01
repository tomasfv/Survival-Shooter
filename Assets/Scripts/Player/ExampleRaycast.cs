using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleRaycast : MonoBehaviour
{
    public GameObject _object;
    public float rayLength;
    public LayerMask layerRay;

    Ray ray; 
    RaycastHit hit;  
 

    void Update()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;

        if (Physics.Raycast(ray, out hit, rayLength, layerRay))
        {
            _object = hit.collider.gameObject; 
            Debug.Log("estoy chocando con algo " + hit.collider.name);
        }

        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red );
    }
}
