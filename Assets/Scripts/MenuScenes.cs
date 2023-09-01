using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScenes : MonoBehaviour
{
   
    public void NewScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
