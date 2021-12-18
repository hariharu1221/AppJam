using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int BankP1Gem;
    [SerializeField] int BankP2Gem;

    public Collider2D[] cols;

    private void Start()
    {
        cols = GetComponentsInChildren<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("P1"))
        {
            Player p1 = col.gameObject.GetComponent<Player>();
            BankP1Gem += p1.curGem;
            p1.curGem = 0;
        }
        else if(col.gameObject.CompareTag("P2"))
        {
            Player p2 = col.gameObject.GetComponent<Player>();
            BankP2Gem += p2.curGem;
            p2.curGem = 0;
        }
    }


}
