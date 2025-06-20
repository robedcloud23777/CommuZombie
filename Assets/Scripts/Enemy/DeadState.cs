using UnityEngine;

public class DeadState : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.animator.SetBool("isDead", true);
        enemy.rb.linearVelocity = Vector2.zero;
        GameObject.Destroy(enemy.gameObject, 1.0f);
    }

    public void Update() { }

    public void Exit() { }
}
