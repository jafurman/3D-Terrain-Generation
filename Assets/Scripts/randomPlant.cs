using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPlant : MonoBehaviour
{
    public GameObject p1, p2, p3;

    // Start is called before the first frame update
    void Start()
    {
        int choice = Random.Range(0, 3);

        if (choice == 0)
        {
            p1.SetActive(true);
            Destroy(p2);
            Destroy(p3);
        }
        else if (choice == 1)
        {
            p2.SetActive(true);
            Destroy(p1);
            Destroy(p3);
        }
        else
        {
            p3.SetActive(true);
            Destroy(p1);
            Destroy(p2);
        }
    }
}

