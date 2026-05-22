using UnityEngine;

public class SkeletonBossAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

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

    public void SetDead(bool isDead)
    {
        animator.SetBool("isDead", isDead);
    }
}
