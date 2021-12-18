using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GemGenerator : MonoBehaviour
{

    #region GemSpawn
    public float gemTimer;

    public GameObject gem;
    #endregion

    void Start()
    {
        StartCoroutine(SpawnGem(gemTimer));
    }

    IEnumerator SpawnGem(float T)
    {
        while(true)
        {
            Debug.Log("보석소환");
            Instantiate(gem, new Vector3(0, 0, 0), Quaternion.identity);




            yield return new WaitForSeconds(T);



        }
    }
}
