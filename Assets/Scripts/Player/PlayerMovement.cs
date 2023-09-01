using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    public LayerMask layerFloor;  

    Rigidbody rb;
    Animator anim;
    Vector3 movement;  
    
    float horizontal;
    float vertical;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()                          
    {
        InputPlayer(); 
    }

    void FixedUpdate()                   
    {
        Move();
        Turning();
        Animating();
    }

    void InputPlayer()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void Move()
    {
        movement = new Vector3(horizontal, 0, vertical);   
        movement.Normalize();   
        rb.MovePosition(transform.position + (movement * speed * Time.deltaTime));
    }

    void Turning()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerFloor ))
        {
            Vector3 playerToMouse = hit.point - transform.position;
            playerToMouse.y = 0;

        
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse); 

            
            rb.MoveRotation(newRotation);
        }
        

    }
    void Animating()
    {
        if(horizontal !=0 || vertical != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}
