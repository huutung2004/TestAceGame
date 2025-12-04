using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private string _nameOfScene;
    public void LoadScene()
    {
        SceneManager.LoadScene(_nameOfScene);
    }
}
