using UnityEngine;

public class GoblinBossAnimation : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWalking(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }

    public void TriggerAttack1()
    {
        animator.SetTrigger("isAttacking1");
    }

    public void TriggerAttack2()
    {
        animator.SetTrigger("isAttacking2");
    }

    public void SetSpeedBoosting(bool value)
    {
        animator.SetBool("isSpeedBoosting", value);
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("isDead");
    }
}
