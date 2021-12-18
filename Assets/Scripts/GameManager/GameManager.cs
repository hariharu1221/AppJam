using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance = null;
    [SerializeField] Bank Bank1;
    [SerializeField] Bank Bank2;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);

        if (Bank1 == null) Bank1 = GameObject.Find("Bank1").GetComponent<Bank>();
        if (Bank2 == null) Bank2 = GameObject.Find("Bank2").GetComponent<Bank>();
    }

    private void Update()
    {
        if (Bank1.BankGem >= 7) Debug.Log("P1 ÀÌ±ט!");
        else if (Bank2.BankGem >= 7) Debug.Log("P2 ÀÌ±ט!");
    }
}
