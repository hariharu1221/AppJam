using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] Bank Bank1;
    [SerializeField] Bank Bank2;
    [SerializeField] Player P1Gem;
    [SerializeField] Player P2Gem;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);

        if (Bank1 == null) Bank1 = GameObject.Find("Bank1").GetComponent<Bank>();
        if (Bank2 == null) Bank2 = GameObject.Find("Bank2").GetComponent<Bank>();

        if (P1Gem == null) P1Gem = GameObject.Find("P1").GetComponent<Player>();
        if (P2Gem == null) P2Gem = GameObject.Find("P2").GetComponent<Player>();
    }

    private void Update()
    {
    }

    public int GetJam(string name)
    {
        if (name == "P1")
            return Bank1.BankGem;
        else if (name == "P2")
            return Bank2.BankGem;
        return 0;
    }

    public int PlayerGem(string name)
    {
        if (name == "P1")
            return P1Gem.CurGem;
        else if (name == "P2")
            return P2Gem.CurGem;
        return 0;
    }


}
