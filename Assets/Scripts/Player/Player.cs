using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAnimatorController anim;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float attackCooldown;// 공격 쿨타임 (초)
    private bool canAttack = true;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimatorController>();
    }

    void Update()
    {
        Move();
        Jump();
        AnimationControl();
    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // 달리기 입력 처리
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * 2 : moveSpeed;

        // Rigidbody2D 이동 적용
        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);

        // 방향 전환
        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void AnimationControl()
    {
        // 이동 처리
        HandleMove();

        // 공격 처리
        HandleAttack();

        // 피격 처리
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetDamaged();
        }

        // 죽음 처리
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetDeath();
        }

        // 기타 처리
        if (Input.GetKeyDown(KeyCode.V))
        {
            anim.SetOther();
        }
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    System.Collections.IEnumerator AttackRoutine()
    {
        canAttack = false;
        anim.SetAttack();

        // 쿨타임 대기
        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    void HandleMove()
    {
        float move = Input.GetAxisRaw("Horizontal");
        anim.SetMove(move != 0);
    }
}
