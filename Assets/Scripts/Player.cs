using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    [Header("스테이터스")]
    [SerializeField] private Status status;

    [Header("점프키")]
    [SerializeField] OneKey jumpKey;

    [Header("이동키")]
    [SerializeField] OneKey rightKey;
    [SerializeField] OneKey leftKey;

    public int CurGem = 0;

    int jumpCount = 0;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    float h;
    void Update()
    {
        if (IsStop) return;

        if (Input.GetKeyDown(jumpKey.onekey) && jumpCount < status.MaxJumpCount)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(Vector2.up * jumpKey.value, ForceMode2D.Impulse);
            animator.SetTrigger("doJumping");
            jumpCount++;
        }

        if (Input.GetKeyDown(rightKey.onekey) || Input.GetKeyDown(leftKey.onekey))
            rigid.velocity = new Vector2(0.5f * rigid.velocity.normalized.x, rigid.velocity.y);

        if (Input.GetKey(rightKey.onekey))
        {
            spriteRenderer.flipX = rightKey.value == 1 ? false : true;
            h = rightKey.value;
        }
        else if (Input.GetKey(leftKey.onekey))
        {
            spriteRenderer.flipX = leftKey.value == 1 ? false : true;
            h = leftKey.value;
        }
        else
            h = 0;

        if (Mathf.Abs(rigid.velocity.x) < 0.2)
            animator.SetBool("isRunning", false);
        else
            animator.SetBool("isRunning", true);
    }

    void FixedUpdate()
    {
        if (IsStop) return;

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > status.Speed)
            rigid.velocity = new Vector2(status.Speed, rigid.velocity.y);

        else if (rigid.velocity.x < status.Speed * (-1))
            rigid.velocity = new Vector2(status.Speed * (-1), rigid.velocity.y);

        RaycastHit2D[] rayHit = new RaycastHit2D[3];
        rayHit[0] = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("Ground"));
        rayHit[1] = Physics2D.Raycast(rigid.position + Vector2.right * 0.5f, Vector3.down, 1f, LayerMask.GetMask("Ground"));
        rayHit[2] = Physics2D.Raycast(rigid.position + Vector2.right * -0.5f, Vector3.down, 1f, LayerMask.GetMask("Ground"));
        var check = Array.Exists(rayHit, x => x.collider != null);

        if (rigid.velocity.y < 0 && check)
            jumpCount = 0;
    }

    bool isStop = false;
    public bool IsStop
    {
        get { return isStop; }
        set { isStop = value; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            Destroy(collision.gameObject);
            CurGem += 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if(collision.gameObject.CompareTag("Gem"))
            {
                Destroy(collision.gameObject);
                
            if(this.gameObject.CompareTag("P1"))
            {
                GameManager.Instance.P1Gem += 1;
            }
            else if(this.gameObject.CompareTag("P2"))
            {
                GameManager.Instance.P2Gem += 1;
            }
            }
    }




}

[System.Serializable]
public class OneKey
{
    public KeyCode onekey;
    public float value;
}

[System.Serializable]
public class DoubleKey
{
    public KeyCode onekey;
    public KeyCode twokey;
    public float value;
}