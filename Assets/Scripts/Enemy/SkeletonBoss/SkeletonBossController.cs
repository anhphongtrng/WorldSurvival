using UnityEngine;

public class SkeletonBossController : MonoBehaviour
{
    public DamageReceiver damageReceiver;
    public SkeletonBossAnimation skeletonBossAnimation;

    private void Awake()
    {
        damageReceiver = GetComponent<DamageReceiver>();
        skeletonBossAnimation = GetComponent<SkeletonBossAnimation>();
    }
}
