using UnityEngine;

public class SkeletonBossMeleeAttack : EnemySkills
{
    protected SkeletonBossController skeletonBossController;

    protected override void Awake()
    {
        base.Awake();
        skeletonBossController = GetComponent<SkeletonBossController>();
    }

    protected override void Update()
    {
        MeleeAttack();
    }

    protected override void OnSkill()
    {
        Debug.Log("Skeleton Boss sử dụng kỹ năng đánh thường!");
        skeletonBossController.skeletonBossAnimation.TriggerAttack2();
    }

    protected void MeleeAttack()
    {
        float distanceToPlayer = Vector3.Distance(
            transform.position,
            PlayerController.instance.transform.position
        );

        // Player quá xa
        if (distanceToPlayer >= 10f || distanceToPlayer == 0f)
        {
            skeletonBossController.skeletonBossAnimation.SetWalking(false);
        }

        // Chạy tăng tốc để tiếp cận
        else if (distanceToPlayer < 10f && distanceToPlayer > 3f)
        {
            skeletonBossController.skeletonBossAnimation.SetWalking(true);
        }

        // Đủ gần để đánh
        else if (distanceToPlayer <= 3f)
        {
            UseSkill();
        }
    }
}
