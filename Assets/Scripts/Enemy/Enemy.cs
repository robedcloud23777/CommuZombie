using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float moveSpeed = 2f;
    public float detectionRange = 10f;   // �ν� ���� ����
    public float lostRange = 20f;        // ���� ���� ���� (detectionRange���� Ŀ�� ��)


    public Rigidbody2D rb { get; private set; }
    public Transform player { get; private set; }

    private IEnemyState currentState;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;

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
}
