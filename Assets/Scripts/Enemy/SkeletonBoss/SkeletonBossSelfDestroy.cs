using System.Collections;
using UnityEngine;

public class SkeletonBossSelfDestroy : SelfDestroy
{
    [SerializeField] protected float delayBeforeEndGame = 3f;

    protected SkeletonBossController skeletonBossController;
    protected SkeletonBossAnimation skeletonBossAnimation;

    protected void Awake()
    {
        skeletonBossController = GetComponent<SkeletonBossController>();
        skeletonBossAnimation = GetComponent<SkeletonBossAnimation>();
    }

    void Update()
    {
        DestroySelf();
    }

    public override void DestroySelf()
    {
        if (isDead) return;

        if (skeletonBossController.damageReceiver.IsDead())
        {
            isDead = true;
            skeletonBossController.enemyBrain.Dead();
            skeletonBossAnimation.TriggerDeath();
            DropItems();
            StartCoroutine(GameController.instance.OnFinalBossDeadOrWinGame(gameObject));
        }
    }
}