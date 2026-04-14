using UnityEngine;

public class MummyAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    protected void Awake()
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

    public void SetDead(bool isDead)
    {
        animator.SetBool("isDead", isDead);
    }
}
