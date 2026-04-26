using UnityEngine;

public class FlyEnhanceSkill : EnemySkills
{
    protected FlyController flyController;
    protected override void Awake()
    {
        base.Awake();
        skillCooldown = 5f;
        flyController = GetComponent<FlyController>();
    }

    protected override void Update()
    {
        EnhanceAttack();
    }
    protected override void OnSkill()
    {
        if (flyController.flyRangeSkill.bulletCount == 5) return;
        flyController.flyAnimation.TriggerEnhance();
        flyController.flyRangeSkill.bulletCount += 1;
    }

    protected void EnhanceAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if (distanceToPlayer <= 7f)
        {
            UseSkill();
        }
    }

}
