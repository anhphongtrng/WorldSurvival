using UnityEngine;

public class FLyRangeSkill : EnemySkills
{
    [SerializeField] protected GameObject enemyBulletPrefab;
    protected float bulletMoveSpeed = 5f;

    protected override void Update()
    {
        RangeAttack();
    }

    protected override void OnSkill()
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
