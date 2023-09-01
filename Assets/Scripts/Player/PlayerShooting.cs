using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot;
    public float timeBetweenBullets;
    public float range;  //Longitud del Raycast. 
    public LayerMask shootableMask; //capa de objetos a la que vamos a poder disparar.

    float timer; //variable contador de tiempo.
    Ray ray; //variable para el Raycast.
    RaycastHit hit;
    LineRenderer lineRenderer;
    AudioSource audioS;
    Light gunLight;
    float effectsDisplayTime = 0.2f;   //tiempo que van a estar los fx en pantalla 
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;  //contador de tiempo. 
        
        if (Input.GetMouseButtonDown(0) && timer >= timeBetweenBullets)     //0 es el botón izq del mouse. 
        {
            Shoot();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)  //Dehabilitamos los fx.
        {
            lineRenderer.enabled = false;
            gunLight.enabled = false; 
        }

    }

    void Shoot()
    {
        timer = 0;

        audioS.Play();

        lineRenderer.enabled = true;                        //habilito el componente LineRenderer. 
        gunLight.enabled = true;
        lineRenderer.SetPosition(0, transform.position);    //Establezco el primer punto del LineRenderer. 

        ray.origin = transform.position;  //defino el origen del raycast. 
        ray.direction = transform.forward; 

        if(Physics.Raycast(ray, out hit, range, shootableMask))
        {
            //me guardo en una variable local el GO con el que estoy choccando. 
            GameObject _object = hit.collider.gameObject;

            //compruebo si ese GO es el enemigo.
            if (_object.GetComponent<EnemyHealth>())                //si el GO tiene el componente EnemyHealth...
            {
                _object.GetComponent<EnemyHealth>().TakeDamage(damagePerShot, hit.point);
            }

            //Estoy estableciendo el segundo punto del line renderer.  
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.origin + (ray.direction * range));
        }
    }
}
