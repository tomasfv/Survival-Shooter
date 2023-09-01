using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject panelGameOver;
    public TextMeshProUGUI textScore;

    [Header("Array Positions")]         
    public Transform[] positions;      
    [Header("Array Enemies")]
    public GameObject[] enemyPrefab;    
    [Space]                             
    public Transform parentEnemies;     
    
    [Range(2, 6)]                       
    [Tooltip("Tiempo entre enemigos")]  
    public float time;                  

    int score;                          
    void Start()
    {
        InvokeRepeating("CreateEnemy", time, time);
    }

   void CreateEnemy()
    {
        int pos = Random.Range(0, positions.Length);            
        int enemy = Random.Range(0, enemyPrefab.Length);
        GameObject cloneEnemy = Instantiate(enemyPrefab[enemy], positions[pos].position, positions[pos].rotation);
        cloneEnemy.transform.SetParent(parentEnemies);
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);
    }

    public void LoadScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public void ScoreEnemy(int scoreValue)
    {
        score += scoreValue;
        textScore.text = "Score: " + score.ToString(); 
    }
}
