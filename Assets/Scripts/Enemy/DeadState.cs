using UnityEngine;

public class DeadState : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        Debug.Log($"{enemy.name}: Dead 상태 진입");
        enemy.rb.linearVelocity = Vector2.zero;

        // 죽는 애니메이션 재생 or 이펙트
        GameObject.Destroy(enemy.gameObject, 1.0f); // 1초 후 제거
    }

    public void Update()
    {
        // 사망 상태에서는 아무것도 하지 않음
    }

    public void Exit()
    {
        // Dead 상태에서는 Exit가 호출되지 않음
    }
}
