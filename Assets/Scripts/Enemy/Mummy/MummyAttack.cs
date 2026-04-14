using UnityEngine;

public class MummyAttack : EnemyAttack
{
    protected MummyController mummyController;

    protected override void Awake()
    {
        base.Awake();
        mummyController = GetComponent<MummyController>();
    }

    protected override void Update()
    {
        TryAttack();
    }
    protected override void OnAttack()
    {
        mummyController.mummyAnimation.TriggerAttack();
    }

    protected void TryAttack()
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
            Attack();
        }
    }

}
