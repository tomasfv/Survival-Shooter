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
    
    public float flashSpeed;                
    public Color flashColor;

    public GameManager gameManager;

    public AudioClip deathClip;

    AudioSource audioS;
    Animator anim;                         
    PlayerMovement playerMovement;          
    PlayerShooting playerShooting;         

    bool isDead;
    bool damaged;
    void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        audioS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();     
        playerShooting = GetComponentInChildren<PlayerShooting>();  
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

   
    public void TakeDamage(int amount)
    {
        if (isDead) return;  

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
        anim.SetTrigger("Death");           
        playerMovement.enabled = false;     
        playerShooting.enabled = false;     
    }

   
    public void DeathComplete()
    {
        gameManager.GameOver();
    }
}
