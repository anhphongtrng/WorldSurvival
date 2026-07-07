using System.Collections;
using UnityEngine;

public class GoblinBossIceWallCircle : EnemySkill
{
    [Header("Components")]
    [SerializeField] protected GoblinBossAnimation goblinBossAnimation;
    [SerializeField] protected GoblinBossMovement goblinBossMovement;

    [Header("Prefab")]
    [SerializeField] protected GameObject iceWallPrefab;

    [Header("Distance")]
    [SerializeField] protected float activeRange = 8f;
    [SerializeField] protected float radius = 6f;

    [Header("Circle Settings")]
    [SerializeField] protected int numberOfWalls = 8;

    [Header("Timing")]
    [SerializeField] protected float castDelay = 0.5f; // delay từ lúc animation attack tới lúc IceWall xuất hiện

    [Header("Spawn Point")]
    [SerializeField] protected Transform castPoint; // thường là chính vị trí boss

    protected override void Awake()
    {
        base.Awake();
        goblinBossAnimation = GetComponent<GoblinBossAnimation>();
        goblinBossMovement = GetComponent<GoblinBossMovement>();
    }

    protected void Start()
    {
        stopMovementWhenUseSkill = true;
        cooldown = 10f;
    }

    public override bool CanUse()
    {
        if (!base.CanUse())
            return false;

        float distance = Vector2.Distance(transform.position, brain.target.position);
        return distance <= activeRange;
    }

    public override IEnumerator Execute()
    {
        StartCoroutine(StartCooldown());
        goblinBossAnimation.TriggerAttack1(); // đổi tên hàm animation trigger nếu khác
        yield return new WaitForSeconds(castDelay);
        SpawnCircleIceWalls();
    }

    public void SpawnCircleIceWalls()
    {
        for (int i = 0; i < numberOfWalls; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfWalls;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector3 spawnPos = castPoint.position + new Vector3(x, y, 0);

            float angleDeg = angle * Mathf.Rad2Deg;

            if (goblinBossMovement.isFacingRight)
            {
                angleDeg += 360f;
            }
            else
            {
                angleDeg += 180f;
            }
            Quaternion spawnRot = Quaternion.Euler(0, 0, angleDeg);

            GameObject wall = Instantiate(iceWallPrefab, spawnPos, spawnRot);
            wall.transform.SetParent(transform);
            Destroy(wall, 1f);
        }
    }
}