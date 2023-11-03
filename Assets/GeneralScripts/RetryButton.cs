using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RetryButton : MonoBehaviour
{
    private GameObject SceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        SceneLoader = GameObject.Find("SceneManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        // Reload the current scene
        SceneLoader.GetComponent<Transition>().ReloadPreviosLevel();
    }
}
