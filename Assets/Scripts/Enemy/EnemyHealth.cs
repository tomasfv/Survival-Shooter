using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;           
    public int currentHealth;
    public float sinkSpeed;         
    public int scoreValue;          
    public bool isDead;

    public AudioClip deathClip;

    public ParticleSystem hitParticles;

    AudioSource audioS;
    Animator anim;
    bool isSinking;                 
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
    }


    void Update()
    {
        if(isSinking == true)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

     
    public void TakeDamage(int amount, Vector3 point)
    {
        if (isDead) return;                 

        currentHealth -= amount;           
        audioS.Play();                      
        hitParticles.transform.position = point;   
        hitParticles.Play();                 
        if (currentHealth <= 0) Death();
    }

    void Death()
    {
        audioS.clip = deathClip;            
        audioS.Play();

        isDead = true;
        anim.SetTrigger("Death");  
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ScoreEnemy(scoreValue);                                   
    }

    public void StartSinking()
    {
        isSinking = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;   
        Destroy(gameObject, 2); 
    }
}
