using UnityEngine;

public class PlayerBeamAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] float range = 5f;
    [SerializeField] float damagePerSecond = 0.01f;
    [SerializeField] LayerMask enemyLayer;

    [Header("Line")]
    [SerializeField] LineController lineController;
    [SerializeField] LineController linePrefab;

    protected Transform currentTarget;

    private void Awake()
    {
        lineController = GameObject.Find("LaserLine").GetComponent<LineController>();
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        FindNearestTarget();
        //SpawnLine();

        if (currentTarget == null)
        {
            lineController.Hide();
            return;
        }

        AttackTarget();
    }

    public void SpawnLine()
    {
        if (lineController != null) return;

        lineController = Instantiate(linePrefab);
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
        Debug.Log("Current Target: " + (currentTarget != null ? currentTarget.name : "None"));
    }

    void AttackTarget()
    {
        lineController.Draw(transform.position, currentTarget.position);

        if (currentTarget.TryGetComponent<DamageReceiver>(out var dmg))
        {
            //damagePerSecond *= Time.deltaTime; // Scale damage by time
            dmg.TakeDamage(damagePerSecond);
        }
    }
}