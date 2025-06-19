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
        Debug.Log($"{enemy.name}: Idle 상태 진입");
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

        // 상태 전환
        if (timer >= currentDuration)
        {
            SetRandomIdleState();
        }

        // 플레이어 감지 시 추격 상태로 전환
        float dist = Vector2.Distance(enemy.player.position, enemy.transform.position);
        if (dist < enemy.detectionRange)
        {
            enemy.ChangeState(new ChaseState()); // 감지 시 추격 시작
        }
    }

    public void Exit()
    {
        enemy.rb.linearVelocity = Vector2.zero;
        Debug.Log($"{enemy.name}: Idle 상태 종료");
    }

    private void SetRandomIdleState()
    {
        timer = 0f;
        actionState = (Random.value < 0.5f) ? IdleActionState.Waiting : IdleActionState.Walking;
        currentDuration = Random.Range(1f, 3f); // 각 상태 지속 시간 랜덤

        if (actionState == IdleActionState.Walking)
        {
            direction = Random.value < 0.5f ? -1f : 1f;
            Debug.Log($"{enemy.name}: 걷기 시작 (방향: {direction}, 시간: {currentDuration:F1}s)");
        }
        else
        {
            Debug.Log($"{enemy.name}: 멈춤 상태 (시간: {currentDuration:F1}s)");
        }
    }
}
