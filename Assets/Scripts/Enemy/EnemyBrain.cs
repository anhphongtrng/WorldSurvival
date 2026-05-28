using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("State")]
    public EnemyState currentState;

    [Header("Distance")]
    public float chaseRange = 10f;

    [Header("Think Delay")]
    [SerializeField] Vector2 thinkDelayRange = new(2.5f, 3.5f);
    private bool canThink = true;

    [Header("Components")]
    public SkeletonBossMovement movement;
    public SkeletonBossAnimation anim;

    [Header("Skills")]
    public EnemySkill[] skills;
    private EnemySkill lastSkill;

    private void Awake()
    {
        movement = GetComponent<SkeletonBossMovement>();
        anim = GetComponent<SkeletonBossAnimation>();
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            target = player.transform;
        }
    }

    private void Update()
    {
        if (currentState == EnemyState.Dead)
            return;

        if (target == null)
            return;

        float distance =
            Vector2.Distance(transform.position, target.position);

        // ngoài range -> idle
        if (distance >= chaseRange)
        {
            Idle();
            return;
        }

        // nếu không busy -> chase player
        if (currentState != EnemyState.Busy)
        {
            Chase();
        }

        // nếu chưa được nghĩ skill
        if (!canThink)
            return;

        // nếu đang busy thì không cast skill
        if (currentState == EnemyState.Busy)
            return;

        EnemySkill skill = ChooseSkill();

        if (skill != null)
        {
            StartCoroutine(UseSkill(skill));
        }
    }

    private void Idle()
    {
        currentState = EnemyState.Idle;
        movement.StopMove();
        anim.SetWalking(false);
    }

    private void Chase()
    {
        currentState = EnemyState.Chase;
        movement.MoveToTarget();
    }

    private EnemySkill ChooseSkill()
    {
        List<EnemySkill> availableSkills = new();

        foreach (EnemySkill skill in skills)
        {
            if (skill.CanUse())
            {
                availableSkills.Add(skill);
            }
        }

        if (availableSkills.Count == 0)
            return null;
        int totalWeight = 0;

        foreach (EnemySkill skill in availableSkills)
        {
            totalWeight += skill.weight;
        }

        int randomValue =
            Random.Range(0, totalWeight);

        int currentWeight = 0;

        foreach (EnemySkill skill in availableSkills)
        {
            currentWeight += skill.weight;

            if (randomValue < currentWeight)
            {
                return skill;
            }
        }

        return null;
    }

    private IEnumerator UseSkill(EnemySkill skill)
    {
        currentState = EnemyState.Busy;

        if(skill.stopMovementWhenUseSkill)
        {
            Debug.Log("Stop movement to use skill: " + skill.skillName);
            movement.StopMove();
        }

        lastSkill = skill;

        yield return StartCoroutine(skill.Execute());

        currentState = EnemyState.Idle;

        yield return StartCoroutine(ThinkDelay(skill.recoveryTime));
    }

    private IEnumerator ThinkDelay(float recovery)
    {
        canThink = false;

        float randomDelay = Random.Range(thinkDelayRange.x, thinkDelayRange.y);

        yield return new WaitForSeconds(randomDelay + recovery);

        canThink = true;
    }

    public void Dead()
    {
        currentState = EnemyState.Dead;
        movement.StopMove();
        enabled = false;
    }


}
