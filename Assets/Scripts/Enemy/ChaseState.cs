using UnityEngine;

public class ChaseState : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.animator.SetBool("1_Move", true);
    }

    public void Update()
    {
        if (enemy == null || enemy.player == null) return;

        float distance = Vector2.Distance(enemy.player.position, enemy.transform.position);

        // 플레이어가 일정 거리 이상 멀어졌을 때만 Idle로 복귀
        if (distance > enemy.lostRange)
        {
            enemy.ChangeState(new IdleState());
            return;
        }

        // X축만 추적
        Vector2 targetPosition = new Vector2(enemy.player.position.x, enemy.transform.position.y);
        Vector2 direction = (targetPosition - (Vector2)enemy.transform.position).normalized;

        enemy.rb.linearVelocity = new Vector2(direction.x * enemy.moveSpeed, enemy.rb.linearVelocity.y);
        enemy.FaceToPlayer();

        if (distance < 1.5f)
        {
            enemy.ChangeState(new AttackState());
        }
    }

    public void Exit()
    {
        enemy.animator.SetBool("isWalking", false);
        enemy.rb.linearVelocity = Vector2.zero;
    }
}