using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoScene : MonoBehaviour
{
    public string Scenename = "";
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Scenename);

        
    }
}
