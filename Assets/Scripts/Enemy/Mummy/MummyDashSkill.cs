using UnityEngine;

public class MummyDashSkill : EnemySkills
{
    [SerializeField] float dashDistance = 1f;

    protected override void Awake()
    {
        base.Awake();
        skillCooldown = 10f; // Set a longer cooldown for the dash skill
    }

    protected override void Update()
    {
        DashSkill();
    }

    protected override void OnSkill()
    {
        Vector2 direction = (PlayerController.instance.transform.position - transform.position).normalized;
        transform.position += (Vector3)(direction * dashDistance);
    }

    protected void DashSkill()
    {
        UseSkill();
    }
}
