using System.Collections;
using UnityEngine;

public class SkeletonBossCircleAxe : EnemySkill
{
    [Header("Components")]
    [SerializeField] protected SkeletonBossAnimation skeletonBossAnimation;

    [Header("Prefab")]
    [SerializeField] protected GameObject axePrefab;

    [Header("Distance")]
    [SerializeField] protected float activeRange = 7f;
    [SerializeField] protected float radius = 4f;

    [Header("Timing")]
    [SerializeField] protected float throughDelay = 0.5f;
    [SerializeField] protected float skillDuration = 4f;

    [Header("Throw Point")]
    [SerializeField] protected Transform throwPoint;

    protected override void Awake()
    {
        base.Awake();
        skeletonBossAnimation = GetComponent<SkeletonBossAnimation>();
    }

    protected void Start()
    {
        stopMovementWhenUseSkill = true;
        cooldown = 10f;
    }

    public override bool CanUse()
    {
        if (!base.CanUse())
            return false;
        float distance = Vector2.Distance(transform.position, brain.target.position);
        return distance <= activeRange;
    }


    public override IEnumerator Execute()
    {
        StartCoroutine(StartCooldown());
        skeletonBossAnimation.TriggerAttack1();
        yield return new WaitForSeconds(throughDelay);
        SpawnCircleAxes();
    }

    public void SpawnCircleAxes()
    {
        int numberOfAxes = 8;
        for (int i = 0; i < numberOfAxes; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfAxes;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector3 spawnPos = throwPoint.position + new Vector3(x, y, 0);

            GameObject axe = Instantiate(axePrefab, spawnPos, Quaternion.identity);
            axe.transform.SetParent(transform);
            Destroy(axe, skillDuration);
        }
    }
}
