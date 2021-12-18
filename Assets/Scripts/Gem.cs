using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gem : MonoBehaviour
{
    [SerializeField]
    SpawnForce spawnForce;

    Rigidbody2D rigid;


    void Start()
    {

        spawnForce.ForceX = UnityEngine.Random.Range(spawnForce.ForceXmin, spawnForce.ForceXmax); 


        rigid = GetComponent<Rigidbody2D>();

        WhenSpawnGem(spawnForce.ForceX,spawnForce.ForceY);
        //Debug.Log(spawnForce.ForceX);
    }


    void WhenSpawnGem(float x,float y)
    {
        
        rigid.velocity = new Vector2(x, y);
    }
}

[Serializable]
public class SpawnForce
{
    public float ForceX;
    [Range(-10,0)]
    public float ForceXmin;
    [Range(0,10)]
    public float ForceXmax;
    [Range(0,10)]
    public float ForceY;
}
