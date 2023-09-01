using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smooting;  

    Vector3 offset; 
    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()   
    {
        Vector3 targetCamPos = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smooting * Time.deltaTime);
    }
}
