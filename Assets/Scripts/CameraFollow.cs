using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smooting;  //velocidad de seguimiento de la cámara al player

    Vector3 offset; //distancia inicial entre camara y player. 
    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()   //LateUpdate es "lo último que se ejecuta antes de abandonar el frame"
    {
        Vector3 targetCamPos = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smooting * Time.deltaTime);
    }
}
