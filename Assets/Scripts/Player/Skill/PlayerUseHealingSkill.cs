using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseHealingSkill : PlayerSkill
{
    [Header("Music")]
    public AudioClip healingSkillClip;

    public override void HandleAim()
    {
        if (!skillSlots[0].isUnlocked) return;
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            UseSkill();
        }
    }

    public override void UseSkill()
    {
        if (!canUse) return;
        PlayerController.instance.playerDamageReceiver.ChangeHP(1f);
        GameObject heal = Instantiate(skillPrefab, transform.position, Quaternion.identity);
        heal.transform.SetParent(transform);
        AudioController.instance.PlaySFX(healingSkillClip);
        Destroy(heal, 1f);
        canUse = false;
        cooldownTimer = cooldown;
    }

    private void Start()
    {
        cooldown = 15f;
    }
}
