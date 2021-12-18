using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status
{
    [SerializeField] float hp;
    [SerializeField] float maxHp;
    [SerializeField] float attack;
    [SerializeField] float defense;
    [SerializeField] float speed;
    [SerializeField] int maxJumpCount = 2;

    public float Hp
    {
        get { return hp; }
        set 
        {
            if (value >= maxHp) hp = maxHp;
            else if (value <= 0) hp = 0;
            else hp = value; 
        }
    }
    public float Maxhp
    {
        get { return maxHp; }
        set 
        { 
            if (value <= 0) maxHp = 0;
            else maxHp = value; 
        }
    }
    public float Attack
    {
        get { return attack; }
        set 
        {
            if (value <= 0) attack = 0;
            else attack = value; 
        }
    }
    public float Defense
    {
        get { return defense; }
        set 
        {
            if (value <= 0) defense = 0;
            else defense = value; 
        }
    }
    public float Speed
    {
        get 
        { 
            return speed; 
        }
        set 
        {
            if (value <= 0) speed = 0;
            else speed = value; 
        }
    }
    public int MaxJumpCount
    {
        get { return maxJumpCount; }
        set
        {
            if (value <= 1) maxJumpCount = 1;
            else maxJumpCount = value;
        }
    }
}
