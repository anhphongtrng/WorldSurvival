using UnityEngine;

public class MummyMeleeSkill : EnemySkills
{
    protected MummyController mummyController;

    protected override void Awake()
    {
        base.Awake();
        mummyController = GetComponent<MummyController>();
    }

    protected override void Update()
    {
        MeleeAttack();
    }
    protected override void OnSkill()
    {
        mummyController.mummyAnimation.TriggerAttack();
    }

    protected void MeleeAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if (distanceToPlayer >= 5f || distanceToPlayer == 0f)
        {
            mummyController.mummyAnimation.SetRunning(false);
        }
        else if (distanceToPlayer < 5f && distanceToPlayer > 1.5f)
        {
            mummyController.mummyAnimation.SetRunning(true);
        }
        else if (distanceToPlayer <= 1.5f)
        {
            UseSkill();
        }
    }

}
