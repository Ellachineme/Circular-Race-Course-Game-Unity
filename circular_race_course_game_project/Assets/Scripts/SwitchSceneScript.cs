using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneScript : MonoBehaviour
{
    [SerializeField] private string nameOfScene;
    
    
    public void SwitchScene()
    {
        SceneManager.LoadScene(nameOfScene);
    }
}
