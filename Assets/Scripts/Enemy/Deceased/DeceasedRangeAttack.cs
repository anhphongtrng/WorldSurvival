using System.Collections;
using UnityEngine;

public class DeceasedRangeAttack : EnemySkill
{
    [Header("Components")]
    [SerializeField] protected DeceasedAnimation deceasedAnimation;

    [Header("Prefab")]
    [SerializeField] protected GameObject deceasedBulletPrefab;

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
        deceasedAnimation = GetComponent<DeceasedAnimation>();
    }

    private void Start()
    {
        stopMovementWhenUseSkill = true;
        cooldown = 2f;
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
        deceasedAnimation.TriggerAttack();
        yield return new WaitForSeconds(throwDelay);
        ThrowBullet();
    }

    public void ThrowBullet()
    {
        Vector2 direction = (brain.target.position - throwPoint.position).normalized;
        GameObject bullet = Instantiate(deceasedBulletPrefab, throwPoint.position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 5f;
        Debug.Log("Throw Bullet");
    }
}
