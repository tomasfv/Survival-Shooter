using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;           //máxima salud del enemigo.
    public int currentHealth;
    public float sinkSpeed;         //Velocidad de hundimiento del enemigo cuando se muere.
    public int scoreValue;          //puntos que va a dar al player una vez destruido el enemy.
    public bool isDead;

    public AudioClip deathClip;

    public ParticleSystem hitParticles;

    AudioSource audioS;
    Animator anim;
    bool isSinking;                 //para saber si el enemigo se esta hundiendo. 
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

    //Función public por que voy a llamarla desde el script de disparo del Player. 
    public void TakeDamage(int amount, Vector3 point)
    {
        if (isDead) return;                 //si isDead es true, me salgo de la función. 

        currentHealth -= amount;            // equivale a currentHealth = currentHealth - amount.
        audioS.Play();                      //play al sonido que tengo en el audio Source.
        hitParticles.transform.position = point;  //situo el sistema de particulas en el punto de impacto del raycast. 
        hitParticles.Play();                //play al VFX de sistema de particulas. 
        if (currentHealth <= 0) Death();
    }

    void Death()
    {
        audioS.clip = deathClip;            //le cambio el clip de audio al componente Audio Source.
        audioS.Play();

        isDead = true;
        anim.SetTrigger("Death");  //ejecuta la animacion de Death.
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ScoreEnemy(scoreValue);                                   
    }

    public void StartSinking()
    {
        isSinking = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;   //Deshabilitá el componente Nav Mesh Agent. 
        Destroy(gameObject, 2); //Destruí el GO en 2 segundos. 
    }
}
