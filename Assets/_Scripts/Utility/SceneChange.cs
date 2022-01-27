using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    
    public void ChangeScene(int _scene)
    {
        SceneManager.LoadSceneAsync(_scene);
    }
}
