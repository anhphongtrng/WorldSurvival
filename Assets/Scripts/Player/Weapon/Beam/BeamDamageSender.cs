using System.Collections.Generic;
using UnityEngine;

public class BeamDamageSender : DamageSender
{
    [Header("Attack")]
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected Transform firePos;

    [Header("Line")]
    [SerializeField] protected LineController linePrefab;
    [SerializeField] protected List<LineController> currentLines = new();
    [SerializeField] protected List<Transform> currentTargets = new();

    protected override void Awake()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        FindNearestTarget();

        if (currentTargets.Count == 0)
        {
            HideAllLine();
            return;
        }

        SpawnLine();
        AttackTarget();
    }

    public void SpawnLine()
    {
        // tao them line neu chua du
        while (currentLines.Count < currentTargets.Count)
        {
            LineController line = Instantiate(linePrefab, transform);
            currentLines.Add(line);
        }

        // hide line du thua
        for (int i = currentTargets.Count; i < currentLines.Count; i++)
        {
            currentLines[i].Hide();
        }
    }

    public void FindNearestTarget()
    {
        currentTargets.Clear();
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, StatsController.instance.beamRangeAttack, enemyLayer);

        List<Collider2D> enemies = new(hits);

        // sap xep enemy theo khoang cach gan nhat
        enemies.Sort((a, b) =>
        {
            float distA = Vector2.Distance(transform.position, a.transform.position);
            float distB = Vector2.Distance(transform.position, b.transform.position);

            return distA.CompareTo(distB);
        });

        // lay ra beamCount enemy gan nhat
        for (int i = 0; i < Mathf.Min(StatsController.instance.beamLineCount, enemies.Count); i++)
        {
            currentTargets.Add(enemies[i].transform);
        }
    }

    public void AttackTarget()
    {
        for (int i = 0; i < currentTargets.Count; i++)
        {
            Transform target = currentTargets[i];
            LineController line = currentLines[i];

            line.Draw(firePos.position, target.position);

            if (target.TryGetComponent<DamageReceiver>(out var dmg))
            {
                dmg.TakeDamage(
                    StatsController.instance.beamWeaponDamage * Time.deltaTime
                );
            }
        }
    }

    public void HideAllLine()
    {
        foreach (var line in currentLines)
        {
            line.Hide();
        }
    }
}