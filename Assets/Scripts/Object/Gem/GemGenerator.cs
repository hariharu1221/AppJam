using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GemGenerator : MonoBehaviour
{

    //#region GemGenerator
    public float gemTimer;
    public GameObject[] gems;
    public Animator anim;
    //#endregion

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(SpawnGem(gemTimer));
    }

    IEnumerator SpawnGem(float T)
    {
        while(true)
        {
           // Debug.Log("보석소환");
            int r = UnityEngine.Random.Range(0, gems.Length);
            Instantiate(gems[r], new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
            anim.Play("PrinterAnim");

            yield return new WaitForSeconds(T);
        }
    }
}
