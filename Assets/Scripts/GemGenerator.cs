using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GemGenerator : MonoBehaviour
{

    #region GemSpawn
    public float gemTimer;

    public GameObject[] gems;
    #endregion

    void Start()
    {

        StartCoroutine(SpawnGem(gemTimer));
    }

    IEnumerator SpawnGem(float T)
    {
        while(true)
        {
           // Debug.Log("보석소환");
            int r = UnityEngine.Random.Range(0, gems.Length);
            Instantiate(gems[r], new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
            



            yield return new WaitForSeconds(T);



        }
    }
}
