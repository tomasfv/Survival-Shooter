using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot;
    public float timeBetweenBullets;
    public float range;  
    public LayerMask shootableMask; 

    float timer; 
    Ray ray; 
    RaycastHit hit;
    LineRenderer lineRenderer;
    AudioSource audioS;
    Light gunLight;
    float effectsDisplayTime = 0.2f;   
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;  
        
        if (Input.GetMouseButtonDown(0) && timer >= timeBetweenBullets)     
        {
            Shoot();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)  
        {
            lineRenderer.enabled = false;
            gunLight.enabled = false; 
        }

    }

    void Shoot()
    {
        timer = 0;

        audioS.Play();

        lineRenderer.enabled = true;                        
        gunLight.enabled = true;
        lineRenderer.SetPosition(0, transform.position);    

        ray.origin = transform.position;  
        ray.direction = transform.forward; 

        if(Physics.Raycast(ray, out hit, range, shootableMask))
        {
            GameObject _object = hit.collider.gameObject;

            if (_object.GetComponent<EnemyHealth>())                
            {
                _object.GetComponent<EnemyHealth>().TakeDamage(damagePerShot, hit.point);
            }

            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.origin + (ray.direction * range));
        }
    }
}
