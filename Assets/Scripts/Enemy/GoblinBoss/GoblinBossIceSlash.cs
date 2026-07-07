using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GoblinBossIceSlash : EnemySkill
{
    [Header("Components")]
    [SerializeField] protected GoblinBossAnimation goblinBossAnimation;

    [Header("Prefab")]
    [SerializeField] protected GameObject iceSlashPrefab;

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
        goblinBossAnimation = GetComponent<GoblinBossAnimation>();
    }

    private void Start()
    {
        stopMovementWhenUseSkill = true;
        cooldown = 5f;
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
        goblinBossAnimation.TriggerAttack1();
        yield return new WaitForSeconds(throwDelay);
        ThrowIceSlash();
    }

    public void ThrowIceSlash()
    {
        Vector2 direction = (brain.target.position - throwPoint.position).normalized;
        GameObject iceSlash = Instantiate(iceSlashPrefab, throwPoint.position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        iceSlash.transform.rotation = Quaternion.Euler(0, 0, angle);
        iceSlash.GetComponent<Rigidbody2D>().linearVelocity = direction * 5f;
        Debug.Log("Throw Ice Slash");
    }
}
