using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;

    private float timer;
    private float currentDuration;

    private enum IdleActionState { Waiting, Walking }
    private IdleActionState actionState;

    private float direction = 1f;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        timer = 0f;
        SetRandomIdleState();
        Debug.Log($"{enemy.name}: Idle ���� ����");
    }

    public void Update()
    {
        timer += Time.deltaTime;

        switch (actionState)
        {
            case IdleActionState.Waiting:
                enemy.rb.linearVelocity = new Vector2(0, enemy.rb.linearVelocity.y);
                break;

            case IdleActionState.Walking:
                enemy.rb.linearVelocity = new Vector2(direction * enemy.moveSpeed * 0.5f, enemy.rb.linearVelocity.y);
                break;
        }

        // ���� ��ȯ
        if (timer >= currentDuration)
        {
            SetRandomIdleState();
        }

        // �÷��̾� ���� �� �߰� ���·� ��ȯ
        float dist = Vector2.Distance(enemy.player.position, enemy.transform.position);
        if (dist < enemy.detectionRange)
        {
            enemy.ChangeState(new ChaseState()); // ���� �� �߰� ����
        }
    }

    public void Exit()
    {
        enemy.rb.linearVelocity = Vector2.zero;
        Debug.Log($"{enemy.name}: Idle ���� ����");
    }

    private void SetRandomIdleState()
    {
        timer = 0f;
        actionState = (Random.value < 0.5f) ? IdleActionState.Waiting : IdleActionState.Walking;
        currentDuration = Random.Range(1f, 3f); // �� ���� ���� �ð� ����

        if (actionState == IdleActionState.Walking)
        {
            direction = Random.value < 0.5f ? -1f : 1f;
            Debug.Log($"{enemy.name}: �ȱ� ���� (����: {direction}, �ð�: {currentDuration:F1}s)");
        }
        else
        {
            Debug.Log($"{enemy.name}: ���� ���� (�ð�: {currentDuration:F1}s)");
        }
    }
}
