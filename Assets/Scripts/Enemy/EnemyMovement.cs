using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;    //añado esta librería para usar el Nav Mesh Agent. 

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    Animator anim;
    EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && enemyHealth.isDead == false)
        {
            agent.SetDestination(player.transform.position);
        }
        Animating();
    }

    void Animating()
    {
        if (agent.velocity.magnitude != 0) 
        { 
        
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
}
