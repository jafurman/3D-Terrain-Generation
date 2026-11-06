using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void BaseInteract()
    {
        Interact();
    }

    protected override void Interact()
    {
        float currentPercent = PlayerPrefs.GetFloat("BatteryPercent");
        if (currentPercent < 100f)
        {
            PlayerPrefs.SetFloat("BatteryPercent", currentPercent + 10f);
        }
        Destroy(gameObject);
    }
}
