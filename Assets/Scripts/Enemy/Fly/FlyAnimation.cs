using UnityEngine;

public class FlyAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerEnhance()
    {
        animator.SetTrigger("isEnhancing");
    }

    public void SetDead(bool isDead)
    {
        animator.SetBool("isDead", isDead);
    }
}
