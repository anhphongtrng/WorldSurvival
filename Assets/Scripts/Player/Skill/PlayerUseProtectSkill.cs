using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseProtectSkill : PlayerSkill
{
    [SerializeField] private float invincibleDuration = 3f;
    [SerializeField] private float flickerInterval = 0.1f; // Interval for flickering effect during invincibility

    [Header("Components")]
    [SerializeField] private PlayerDamageReceiver playerDamageReceiver;
    [SerializeField] private SpriteRenderer playerSprite;

    [Header("Music")]
    public AudioClip protectSkillClip;

    private void Awake()
    {
        playerDamageReceiver = GetComponent<PlayerDamageReceiver>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        cooldown = 10f;
    }

    public override void HandleAim()
    {
        if (!skillSlots[0].isUnlocked) return;

        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            UseSkill();
        }
    }

    public override void UseSkill()
    {
        if (!canUse) return;

        //AudioController.instance.PlaySFXWithDuration(protectSkillClip, invincibleDuration);

        StartCoroutine(ProtectRoutine());

        canUse = false;
        cooldownTimer = cooldown;
    }

    private IEnumerator ProtectRoutine()
    {
        playerDamageReceiver.SetInvincible(true);

        float elapsed = 0f;
        while (elapsed < invincibleDuration)
        {
            playerSprite.enabled = !playerSprite.enabled;
            yield return new WaitForSeconds(flickerInterval);
            elapsed += flickerInterval;
        }

        playerSprite.enabled = true;
        playerDamageReceiver.SetInvincible(false);
    }
}