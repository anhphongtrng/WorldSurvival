using UnityEngine;
using System.Collections;

public class SandBossRangeSkill : EnemySkills
{
    [SerializeField] private GameObject enemyBulletPrefab;
    protected float bulletMoveSpeed = 5f;

    protected override void Awake()
    {
        base.Awake();
        skillCooldown = 4f;
    }

    protected override void Update()
    {
        RangeAttack();
    }

    private IEnumerator ScaleBulletOverTime(Transform bullet, float duration, float multiplier)
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


    protected override void OnSkill()
    {
        GameObject enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        Vector3 direction = (PlayerController.instance.transform.position - transform.position).normalized;
        enemyBullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletMoveSpeed;
        StartCoroutine(ScaleBulletOverTime(enemyBullet.transform, 2f, 2f));
    }

    protected void RangeAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
    
        if (distanceToPlayer <= 10f)
        {
            UseSkill();
        }
    }

}
