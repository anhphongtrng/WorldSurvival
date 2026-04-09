using UnityEngine;

public class BeamDamageSender : DamageSender
{
    [Header("Attack")]
    [SerializeField] protected float range = 5f;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected Transform firePos;

    [Header("Line")]
    [SerializeField] 
    protected LineController linePrefab;
    protected LineController currentLine;
    protected Transform currentTarget;

    private void Awake()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void Reset()
    {
        damage = 1f;
    }

    void Update()
    {
        FindNearestTarget();

        if (currentTarget == null)
        {
            if (currentLine != null)
                currentLine.Hide();

            return;
        }

        SpawnLine();
        AttackTarget();
    }

    void SpawnLine()
    {
        if (currentLine != null) return;

        currentLine = Instantiate(linePrefab);
        currentLine.transform.SetParent(transform);
    }

    void FindNearestTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

        float minDist = Mathf.Infinity;
        Transform nearest = null;

        foreach (Collider2D hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }

        currentTarget = nearest;
    }

    void AttackTarget()
    {
        currentLine.Draw(firePos.transform.position, currentTarget.position);

        if (currentTarget.TryGetComponent<DamageReceiver>(out var dmg))
        {
            dmg.TakeDamage(damage * Time.deltaTime);
        }
    }
}