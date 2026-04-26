using UnityEngine;

public class SandBossCircleShootSkill : EnemySkills
{
    [SerializeField] private GameObject enemyBulletPrefab;
    protected float bulletMoveSpeed = 5f;

    protected override void Awake()
    {
        base.Awake();
        skillCooldown = 8f;
    }

    protected override void Update()
    {
        CircleShoot();
    }


    protected override void OnSkill()
    {
        int bulletCount = 12;
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;

            Vector2 direction = new(Mathf.Cos(angle), Mathf.Sin(angle));

            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletMoveSpeed;
        }
    }

    protected void CircleShoot()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if (distanceToPlayer <= 10f)
        {
            UseSkill();
        }
    }
}
