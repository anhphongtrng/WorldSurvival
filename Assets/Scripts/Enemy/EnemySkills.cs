using UnityEngine;

public abstract class EnemySkills : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] protected float skillCooldown;

    protected float lastSkillTime;

    protected virtual void Awake()
    {
        skillCooldown = 2f;
        lastSkillTime = 0f;
    }

    protected virtual void Update()
    {

    }

    public virtual void UseSkill()
    {
        if(Time.time >= lastSkillTime + skillCooldown)
        {
            OnSkill();
            lastSkillTime = Time.time;
        }
    }

    protected abstract void OnSkill();
}