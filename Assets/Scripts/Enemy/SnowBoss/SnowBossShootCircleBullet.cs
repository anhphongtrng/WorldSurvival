using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SnowBossShootCircleBullet : EnemySkill
{
    [Header("Components")]
    [SerializeField] protected SnowBossAnimation snowBossAnimation;

    [Header("Prefab")]
    [SerializeField] protected GameObject snowBossBulletPrefab;

    [Header("Distance")]
    [SerializeField] protected float minDistance = 1f;
    [SerializeField] protected float maxDistance = 8f;

    [Header("Timing")]
    [SerializeField] protected float throwDelay = 0.5f;

    [Header("Throw Point")]
    [SerializeField] protected Transform throwPoint;

    protected override void Awake()
    {
        base.Awake();
        snowBossAnimation = GetComponent<SnowBossAnimation>();
    }

    private void Start()
    {
        stopMovementWhenUseSkill = true;
        cooldown = 6f;
    }

    public override bool CanUse()
    {
        if (!base.CanUse())
            return false;
        float distance = Vector2.Distance(transform.position, brain.target.position);
        return distance >= minDistance && distance <= maxDistance;
    }

    public override IEnumerator Execute()
    {
        StartCoroutine(StartCooldown());
        snowBossAnimation.TriggerAttack();
        yield return new WaitForSeconds(throwDelay);
        StartCoroutine(CircleBullet());
    }

    public IEnumerator CircleBullet()
    {
        int bulletCount = 12;
        float angleStep = 360f / bulletCount;

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float anglee = i * angleStep * Mathf.Deg2Rad;

                Vector2 directionn = new(Mathf.Cos(anglee), Mathf.Sin(anglee));

                GameObject bullett = Instantiate(snowBossBulletPrefab, transform.position, Quaternion.identity);
                bullett.transform.rotation = Quaternion.Euler(0, 0, anglee * Mathf.Rad2Deg - 90f);
                bullett.GetComponent<Rigidbody2D>().linearVelocity = directionn * 5f;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
