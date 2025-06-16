using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public Animator animator;

    public void SetMove(bool isMoving)
    {
        animator.SetBool("1_Move", isMoving);
    }

    public void SetAttack()
    {
        animator.SetTrigger("2_Attack");
    }

    public void SetDamaged()
    {
        animator.SetTrigger("3_Damaged");
    }

    public void SetDeath()
    {
        animator.SetTrigger("4_Death");
        animator.SetBool("isDeath", true);
    }

    public void SetOther()
    {
        animator.SetTrigger("6_Other");
    }

    public void ResetAllTriggers()
    {
        animator.ResetTrigger("2_Attack");
        animator.ResetTrigger("3_Damaged");
        animator.ResetTrigger("4_Death");
        animator.ResetTrigger("6_Other");
    }
}
