using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    //[Header("Components")]
    //public SkeletonBossMovement movement;
    //public SkeletonBossAnimation animations;

    [Header("Skills")]
    public EnemySkill[] skills;
    private EnemySkill lastSkill;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            target = player.transform;
        }
    }

    protected virtual void Update()
    {
        CheckState();
    }

    protected virtual void CheckState()
    {
        if (currentState == EnemyState.Dead)
            return;

        if (target == null)
            return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance >= chaseRange)
        {
            Idle();
            return;
        }

        if (currentState != EnemyState.Busy)
        {
            Chase();
        }

        if (!canThink)
            return;

        if (currentState == EnemyState.Busy)
            return;

        EnemySkill skill = ChooseSkill();

        if (skill != null)
        {
            StartCoroutine(UseSkill(skill));
        }
    }

    protected virtual void StopMovement()
    {
        //movement.StopMove();
    }

    protected virtual void SetWalkingAnimation(bool isWalking)
    {
        //animations.SetWalking(isWalking);
    }

    protected virtual void StartMoveToTarget()
    {
        //movement.MoveToTarget();
    }

    protected virtual void Idle()
    {
        currentState = EnemyState.Idle;
        //movement.StopMove();
        StopMovement();
        //animations.SetWalking(false);
        SetWalkingAnimation(false);
    }

    protected virtual void Chase()
    {
        currentState = EnemyState.Chase;
        //movement.MoveToTarget();
        StartMoveToTarget();
    }

    protected virtual EnemySkill ChooseSkill()
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

        int randomValue = Random.Range(0, totalWeight);

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

    protected virtual IEnumerator UseSkill(EnemySkill skill)
    {
        currentState = EnemyState.Busy;

        if(skill.stopMovementWhenUseSkill)
        {
            Debug.Log("Stop movement to use skill: " + skill.skillName);
            //movement.StopMove();
            StopMovement();
        }

        lastSkill = skill;

        yield return StartCoroutine(skill.Execute());

        currentState = EnemyState.Idle;

        yield return StartCoroutine(ThinkDelay(skill.recoveryTime));
    }

    protected virtual IEnumerator ThinkDelay(float recovery)
    {
        canThink = false;

        float randomDelay = Random.Range(thinkDelayRange.x, thinkDelayRange.y);

        yield return new WaitForSeconds(randomDelay + recovery);

        canThink = true;
    }

    public virtual void Dead()
    {
        currentState = EnemyState.Dead;
        //movement.StopMove();
        StopMovement();
        enabled = false;
    }

}
