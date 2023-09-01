using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public Slider slider;
    public Image damageImage;
    
    public float flashSpeed;                //Velocidad a la que va a desaparecer la imagen. 
    public Color flashColor;

    public GameManager gameManager;

    public AudioClip deathClip;

    AudioSource audioS;
    Animator anim;                          //variable privada tipo componente.
    PlayerMovement playerMovement;          //variable privada tipo componente.
    PlayerShooting playerShooting;          //variable privada tipo componente. 

    bool isDead;
    bool damaged;
    void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        audioS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();     //accedo al componente PlayerMovement que está en el GO que tiene este Script.
        playerShooting = GetComponentInChildren<PlayerShooting>();  //accedo al componente de un hijo del GO que tiene este script. 
    }

    void Update()
    {
        if(damaged == true)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    //función pública que voy a llamar desde el script del enemigo. 
    public void TakeDamage(int amount)
    {
        if (isDead) return;  //si el player ha muerto se sale de la función. 

        audioS.Play();
        damaged = true;
        currentHealth -= amount;
        slider.value = currentHealth;

        if (currentHealth <= 0) Death();
    }

    void Death()
    {
        audioS.clip = deathClip;
        audioS.Play();

        isDead = true;
        anim.SetTrigger("Death");           //ejecutamos la animación de muerte
        playerMovement.enabled = false;     //deshabilitamos el movimiento del player.
        playerShooting.enabled = false;     //deshabilitamos el disparo del player.
    }

    //Función pública que va como evento en la animación de Death del player.
    public void DeathComplete()
    {
        gameManager.GameOver();
    }
}
