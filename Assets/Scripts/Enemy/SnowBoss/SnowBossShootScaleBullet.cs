using System.Collections;
using UnityEngine;

public class SnowBossShootScaleBullet : EnemySkill
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
        cooldown = 3f;
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
        GameObject enemyBullet = Instantiate(snowBossBulletPrefab, transform.position, Quaternion.identity);
        Vector3 direction = (PlayerController.instance.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyBullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        enemyBullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f;
        StartCoroutine(ScaleBulletOverTime(enemyBullet.transform, 2f, 2f));
    }

    public IEnumerator ScaleBulletOverTime(Transform bullet, float duration, float multiplier)
    {
        Vector3 startScale = bullet.localScale;
        Vector3 targetScale = startScale * multiplier;

        float time = 0f;

        while (time < duration)
        {
            if (bullet == null) yield break;

            time += Time.deltaTime;
            float t = time / duration;

            bullet.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        bullet.localScale = targetScale;
    }
}
