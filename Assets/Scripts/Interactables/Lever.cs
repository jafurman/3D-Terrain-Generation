using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class Lever : Interactable
{
    public string scene;
    private FadeManager fadeManager;

    void Start()
    {
        fadeManager = FindObjectOfType<FadeManager>();
    }

    public void BaseInteract()
    {
        Interact();
    }

    protected override void Interact()
    {
        if (fadeManager != null)
        {
            fadeManager.FadeToScene(scene);
        }
        else
        {
            Debug.LogWarning("FadeManager not found. Loading scene without fade.");
            SceneManager.LoadScene(scene);
        }
    }
}

