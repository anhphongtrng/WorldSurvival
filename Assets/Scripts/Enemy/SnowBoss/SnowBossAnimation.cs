using UnityEngine;

public class SnowBossAnimation : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("isAttacking");
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("isDead");
    }
}
