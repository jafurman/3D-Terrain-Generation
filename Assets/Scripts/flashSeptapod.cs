using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MimicSpace;

public class flashSeptapod : Interactable
{
    public void BaseInteract()
    {
        Interact();
    }

    protected override void Interact()
    {
        gameObject.GetComponent<Movement>().enabled = false;
        
        
    }
}
