using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float comboResetTime = 0.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    private bool isRun;
    private int attackClick = 0;
    private float lastClickTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        isRun = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRun ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveX * currentSpeed, rb.linearVelocity.y);

        // 애니메이션: 속도 설정
        animator.SetFloat("Speed", Mathf.Abs(moveX));
        animator.SetBool("isRun", isRun);

        // 방향 전환
        if (moveX > 0) transform.localScale = new Vector3(5, 5, 1);
        else if (moveX < 0) transform.localScale = new Vector3(-5, 5, 1);

        // 바닥 체크
        isGrounded = Physics2D.OverlapCircle(transform.position, 2.7f, groundLayer);
        animator.SetBool("isJump", !isGrounded);

        // 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 우클릭 공격
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastClickTime > comboResetTime)
            {
                attackClick = 0; // 시간 초과 시 초기화
            }

            attackClick++;
            attackClick = Mathf.Clamp(attackClick, 0, 2); // 최대 2연타까지

            animator.SetInteger("attackClick", attackClick);
            lastClickTime = Time.time;
        }
    }

    // 애니메이션 이벤트에서 호출할 수 있음
    public void ResetAttack()
    {
        attackClick = 0;
        animator.SetInteger("attackClick", 0);
    }
}
