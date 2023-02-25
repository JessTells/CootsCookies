using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Plate : MonoBehaviour
{

    [SerializeField] string sceneName;


    public void nextLevel()
    {
        Debug.Log("Next scene");
        SceneManager.LoadScene(sceneName);
    }
}
