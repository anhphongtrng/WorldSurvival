using UnityEngine;

public class SkeletonBossController : MonoBehaviour
{
    public DamageReceiver damageReceiver;
    public SkeletonBossAnimation skeletonBossAnimation;
    public EnemyBrain enemyBrain;

    private void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        skeletonBossAnimation = GetComponent<SkeletonBossAnimation>();
        enemyBrain = GetComponent<EnemyBrain>();
    }
}
