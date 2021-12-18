using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("스테이터스")]
    [SerializeField] private Status status;

    [Header("점프키")]
    [SerializeField] private OneKey jumpKey;

    [Header("이동키")]
    [SerializeField] private OneKey rightKey;
    [SerializeField] private OneKey leftKey;

    [Header("어택키")]
    [SerializeField] private OneKey attackKey;

    [Header("스킬키")]
    [SerializeField] private OneKey skillOneKey;
    [SerializeField] private OneKey skillTwoKey;
    [SerializeField] private OneKey skillThrKey;

    [Header("머터리얼")]
    public MaterialData MTData;

    private int curGem = 0;
    public int CurGem
    {
        get { return curGem; }
        set 
        {
            if (value < 0) curGem = 0;
            else curGem = value; 
        }
    }
    private int jumpCount = 0;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D attackRange;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attackRange = GetComponent<BoxCollider2D>();
        attackRange.enabled = false;
    }

    private float h;
    private void Update()
    {
        InputKey();
        isMove();
    }

    void InputKey()
    {
        if (IsStop) return;
        jump();
        move();
        attack();
        Skill();
    }

    void jump()
    {
        if (Input.GetKeyDown(jumpKey.onekey) && jumpCount < status.MaxJumpCount)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
            rigid.AddForce(Vector2.up * jumpKey.value, ForceMode2D.Impulse);
            animator.SetTrigger("doJumping");
            jumpCount++;
        }
    }

    void move()
    {
        if (Input.GetKeyDown(rightKey.onekey) || Input.GetKeyDown(leftKey.onekey)) //velocity 값 조정
            rigid.velocity = new Vector2(0.5f * rigid.velocity.normalized.x, rigid.velocity.y);
        if (Input.GetKey(rightKey.onekey)) //움직임
        {
            spriteRenderer.flipX = rightKey.value == 1 ? false : true;
            h = rightKey.value;
        }
        else if (Input.GetKey(leftKey.onekey))
        {
            spriteRenderer.flipX = leftKey.value == 1 ? false : true;
            h = leftKey.value;
        }
        else h = 0;
    }

    void attack()
    {
        if (Input.GetKeyDown(attackKey.onekey))
        {
            StartCoroutine(attackKey.Cool());
            animator.SetTrigger("doAttack");
        }
    }

    void isMove()
    {
        if (Mathf.Abs(rigid.velocity.x) < 0.2)      //velocity 값이 0.2 이상이면 애니메이션
            animator.SetBool("isRunning", false);
        else
            animator.SetBool("isRunning", true);
    }

    void Skill()
    {
        SkillOne();
        SkillTwo();
        SkillThr();
    }

    void SkillOne()
    {
        if (Input.GetKeyDown(skillOneKey.onekey))
        {
            StartCoroutine(skillOneKey.Cool());
            StartCoroutine(SkillOneCor());
        }
    }

    IEnumerator SkillOneCor()
    {
        spriteRenderer.material = MTData.materials[1];
        yield return new WaitForSeconds(0.15f);
        Vector3 vec = Vector2.right * (spriteRenderer.flipX ? -1 : 1) * skillOneKey.value;
        transform.position += vec;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = MTData.materials[0];
    }

    void SkillTwo()
    {
        if (Input.GetKeyDown(skillTwoKey.onekey))
        {
            StartCoroutine(skillTwoKey.Cool());
            status.Hp += skillTwoKey.value;
        }
    }

    void SkillThr()
    {
        if (Input.GetKeyDown(skillThrKey.onekey))
        {
            StartCoroutine(skillThrKey.Cool());
            StartCoroutine(SkillThrCor());
        }
    }

    IEnumerator SkillThrCor()
    {
        spriteRenderer.material = MTData.materials[2];
        status.Attack *= skillThrKey.value;
        status.Speed *= skillThrKey.value;
        status.MaxJumpCount *= (int)skillThrKey.value;
        yield return new WaitForSeconds(skillThrKey.getcool / 3f);
        status.Attack /= skillThrKey.value;
        status.Speed /= skillThrKey.value;
        status.MaxJumpCount /= (int)skillThrKey.value;
        spriteRenderer.material = MTData.materials[0];
    }

    private void FixedUpdate()
    {
        if (IsStop) return;

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); //이동

        if (rigid.velocity.x > status.Speed)    //최대 움직임 속도 처리
            rigid.velocity = new Vector2(status.Speed, rigid.velocity.y);
        else if (rigid.velocity.x < status.Speed * (-1))
            rigid.velocity = new Vector2(status.Speed * (-1), rigid.velocity.y);

        RaycastHit2D[] rayHit = new RaycastHit2D[3];    // 바닥에 닿았는지 확인
        rayHit[0] = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("Ground"));
        rayHit[1] = Physics2D.Raycast(rigid.position + Vector2.right * 0.5f, Vector3.down, 1f, LayerMask.GetMask("Ground"));
        rayHit[2] = Physics2D.Raycast(rigid.position + Vector2.right * -0.5f, Vector3.down, 1f, LayerMask.GetMask("Ground"));
        var check = Array.Exists(rayHit, x => x.collider != null);

        if (rigid.velocity.y < 0 && check) jumpCount = 0; //닿았으면 점프 카운트 초기화
    }

    private bool isStop = false;
    public bool IsStop
    {
        get { return isStop; }
        set { isStop = value; }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Gem")) //잼에 닿았으면 CurGem++;
        {
            Destroy(other.gameObject);
            CurGem += 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (other.isTrigger == false) return;
            var p = other.GetComponent<Player>();
            float damage = p.ColTriggerDamage(other);
            status.Hp -= CheckDefense(damage, status.Defense);
            Debug.Log(gameObject.name + status.Hp);
        }
    }
    
    float CheckDefense(float damage, float defense)
    {
        if ((damage - defense) <= 0) return 0;
        return damage - defense; 
    }

    float ColTriggerDamage(Collider2D col)
    {
        if (col == attackRange) return 1f * status.Attack;
        return 0;
    }
}

[System.Serializable]
public class OneKey
{
    public float value;
    public KeyCode onekey
    {
        get
        {
            if (!canPress) return KeyCode.None;
            else
            {
                return key;
            }
        }
        set { key = value; }
    }
    [SerializeField] private KeyCode key;
    [SerializeField] private float cool = 1f;
    public float getcool { get { return cool; } }

    bool canPress = true;
    public IEnumerator Cool()
    {
        canPress = false;
        yield return new WaitForSeconds(cool);
        canPress = true;
    }
}

[System.Serializable]
public class DoubleKey
{
    public KeyCode onekey;
    public KeyCode twokey;
    public float value;
}