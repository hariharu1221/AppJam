using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject P1;
    [SerializeField] GameObject P2;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);

        if (P1 == null) P1 = GameObject.Find("P1");
        if (P2 == null) P2 = GameObject.Find("P2");
    }

    private void Update()
    {
        
    }

}
