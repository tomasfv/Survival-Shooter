using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    public LayerMask layerFloor;   //Capa donde va a estar el suelo de la escena. 

    Rigidbody rb;
    Animator anim;
    Vector3 movement;  //vamos a guardar la dirección de movimiento.
    
    float horizontal;
    float vertical;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()                           //los Input siempre van en el Update. 
    {
        InputPlayer(); 
    }

    void FixedUpdate()                     //Los movimientos por físicas van en el FixedUpdate. 
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
        movement = new Vector3(horizontal, 0, vertical);   //dirección de movimiento a traves de los input. 
        movement.Normalize();   //Normalizar el Vector, para que su módulo (longitud) valga 1.
        rb.MovePosition(transform.position + (movement * speed * Time.deltaTime));
    }

    void Turning()
    {
        //Raycast que va desde el cursor del mouse en coordenadas de pantalla y con dirección hacia la escena. 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerFloor ))
        {
            Vector3 playerToMouse = hit.point - transform.position;
            playerToMouse.y = 0;

            //Calculamos la rotación.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse); //Quaternion es usado para rotaciones. 

            //Aplicamos la rotación.
            rb.MoveRotation(newRotation);
        }
        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);

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
