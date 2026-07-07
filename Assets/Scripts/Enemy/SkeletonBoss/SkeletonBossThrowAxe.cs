using System.Collections;
using UnityEngine;

public class SkeletonBossThrowAxe : EnemySkill
{
    [Header("Components")]
    [SerializeField] protected SkeletonBossAnimation skeletonBossAnimation;

    [Header("Prefab")]
    [SerializeField] protected GameObject axePrefab;

    [Header("Distance")]
    [SerializeField] protected float minDistance = 3f;
    [SerializeField] protected float maxDistance = 8f;

    [Header("Timing")]
    [SerializeField] protected float throwDelay = 0.5f;

    [Header("Throw Point")]
    [SerializeField] protected Transform throwPoint;

    protected override void Awake()
    {
        base.Awake();
        skeletonBossAnimation = GetComponent<SkeletonBossAnimation>();
    }

    private void Start()
    {
        stopMovementWhenUseSkill = true;
        cooldown = 5f;
    }

    public override bool CanUse()
    {
        if(!base.CanUse())
            return false;
        float distance = Vector2.Distance(transform.position, brain.target.position);
        return distance >= minDistance && distance <= maxDistance;
    }

    public override IEnumerator Execute()
    {
        StartCoroutine(StartCooldown());
        skeletonBossAnimation.TriggerAttack1();
        yield return new WaitForSeconds(throwDelay);
        ThrowAxe();
    }

    public void ThrowAxe()
    {
        Vector2 direction = (brain.target.position - throwPoint.position).normalized;
        GameObject axe = Instantiate(axePrefab, throwPoint.position, Quaternion.identity);
        axe.GetComponent<Rigidbody2D>().linearVelocity = direction * 5f;
        Debug.Log("Throw Axe");
    }
}
