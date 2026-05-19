using UnityEngine;

public class PlayerSelfDestroy : SelfDestroy
{
    protected PlayerController playerController;

    protected void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        DestroySelf();
    }

    public override void DestroySelf()
    {
        if (isDead) return;

        if (playerController.playerDamageReceiver.IsDead())
        {
            isDead = true;

            GameController.instance.OnPlayerDead();
        }
    }
}
