using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance = null;
    [SerializeField] GameObject P1;
    [SerializeField] GameObject P2;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);

        if (P1 == null) P1 = GameObject.Find("P1");
        if (P2 == null) P2 = GameObject.Find("P2");
    }

    private void Update()
    {
        
    }
}
