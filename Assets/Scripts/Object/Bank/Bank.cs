using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int bankGem;

    public int BankGem { get { return bankGem; } }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == gameObject.tag)
        {
            Player p = col.gameObject.GetComponent<Player>();
            bankGem += p.CurGem;
            p.CurGem = 0;
        }
    }
}