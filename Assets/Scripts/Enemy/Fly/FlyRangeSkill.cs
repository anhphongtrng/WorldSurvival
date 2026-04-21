using System.Collections;
using UnityEngine;

public class FLyRangeSkill : EnemySkills
{
    [SerializeField] protected GameObject enemyBulletPrefab;
    protected float bulletMoveSpeed = 5f;

    [Header("Enhance")]
    public int bulletCount = 1;
    public float bulletDelay = 0.1f;

    protected override void Update()
    {
        RangeAttack();
    }

    protected override void OnSkill()
    {
        StartCoroutine(FireMultipleBullets());
    }

    IEnumerator FireMultipleBullets()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            ShootOneBullet();
            yield return new WaitForSeconds(bulletDelay);
        }
    }

    protected void ShootOneBullet()
    {
        GameObject enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        Vector3 direction = (PlayerController.instance.transform.position - transform.position).normalized;
        enemyBullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletMoveSpeed;
    }

    protected void RangeAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
    
        if (distanceToPlayer <= 7f)
        {
            UseSkill();
        }
    }
}
