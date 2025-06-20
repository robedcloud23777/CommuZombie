using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float lostRange = 7f;

    public Rigidbody2D rb { get; private set; }
    public Transform player { get; private set; }
    public Animator animator { get; private set; }

    private IEnemyState currentState;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();

        ChangeState(new IdleState());
    }

    void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            ChangeState(new DeadState());
        }
    }

    public void FaceToPlayer()
    {
        if (player == null) return;

        float direction = player.position.x - transform.position.x;
        if (Mathf.Abs(direction) > 0.1f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(direction) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

}