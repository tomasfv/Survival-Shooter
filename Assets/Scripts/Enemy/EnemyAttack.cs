using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks;
    public int attackDamage;

    GameObject player;              //referencia al player
    PlayerHealth playerHealth;      //referencia a playerHealth
    EnemyHealth enemyHealth;

    bool playerInRange;
    float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    //me traigo el GO Player.
        playerHealth = player.GetComponent<PlayerHealth>();     // accedo al script PlayerHealth del GO Player.

        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.isDead == false)
        {
            Attack();
        }
    }

    void Attack()
    {
        timer = 0;
        playerHealth.TakeDamage(attackDamage);
    }
}
