using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//SURVIVAL SHOOTER

public class GameManager : MonoBehaviour
{
    public GameObject panelGameOver;
    public TextMeshProUGUI textScore;

    [Header("Array Positions")]         //cabeceras en el inspector del componente.
    public Transform[] positions;       //array de posiciones
    [Header("Array Enemies")]
    public GameObject[] enemyPrefab;    //array de prefabs de enemigos.
    [Space]                             //Espacio vacio en el inspector del componente.
    public Transform parentEnemies;     //GO vacío padre de los clones de enemigos.
    
    [Range(2, 6)]                       //Creo un atributo range para el tiempo, me creará un slider en el inspector. 
    [Tooltip("Tiempo entre enemigos")]  //herramienta de hover en el inspector. 
    public float time;                  //cada cuanto tiempo voy a instanciar enemigos.

    int score;                          //puntuación total. 
    void Start()
    {
        InvokeRepeating("CreateEnemy", time, time);
    }

   void CreateEnemy()
    {
        int pos = Random.Range(0, positions.Length);            //valor random entre 0 y positions.length
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
