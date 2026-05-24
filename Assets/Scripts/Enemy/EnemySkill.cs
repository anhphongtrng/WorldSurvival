using System.Collections;
using UnityEngine;

public abstract class EnemySkill : MonoBehaviour
{
    [Header("Skill Info")]
    public string skillName;

    [Header("AI")]
    public int weight = 10;

    [Header("Cooldown")]
    public float cooldown;

    [Header("Recovery")]
    public float recoveryTime = 1f; // Thoi gian nghi sau khi su dung skill, truoc khi co the suy nghi dung skill tiep theo

    protected bool isCooldown;
    public bool stopMovementWhenUseSkill;

    protected EnemyBrain brain;

    protected virtual void Awake()
    {
        brain = GetComponent<EnemyBrain>();
    }

    public virtual bool CanUse()
    {
        if (isCooldown)
            return false;

        if (brain == null)
            return false;

        if (brain.target == null)
            return false;

        return true;
    }

    protected IEnumerator StartCooldown()
    {
        isCooldown = true;

        yield return new WaitForSeconds(cooldown);

        isCooldown = false;
    }

    public abstract IEnumerator Execute();
}