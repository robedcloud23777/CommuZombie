using UnityEngine;

public class AttackState : IEnemyState
{
    private Enemy enemy;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.animator.SetTrigger("2_Attack");
    }

    public void Update()
    {
        float distance = Vector2.Distance(enemy.player.position, enemy.transform.position);
        if (distance > 1.5f)
        {
            enemy.ChangeState(new ChaseState());
            return;
        }

        if (Time.time - lastAttackTime > attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        Debug.Log($"{enemy.name}: 플레이어 공격!");
        // 플레이어에 데미지 주는 로직 필요
    }

    public void Exit()
    {
        Debug.Log($"{enemy.name}: Attack 상태 종료");
    }
}
