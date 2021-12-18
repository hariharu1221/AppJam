using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
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
        if (Bank1.BankGem >= 7) Debug.Log("P1 �̱�!");
        else if (Bank2.BankGem >= 7) Debug.Log("P2 �̱�!");
    }

    public int GetJam(string name)
    {
        if (name == "P1")
            return Bank1.BankGem;
        else if (name == "P2")
            return Bank2.BankGem;
        return 0;
    }
}
