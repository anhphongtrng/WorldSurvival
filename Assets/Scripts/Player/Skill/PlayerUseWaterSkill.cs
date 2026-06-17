using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseWaterSkill : PlayerSkill
{
    [Header("Music")]
    public AudioClip waterSkillClip; // Music played when using the water skill

    private void Start()
    {
        cooldown = 4f;
    }

    public override void HandleAim()
    {
        if (!skillSlots[0].isUnlocked) return;
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartAim();
        }

        if (!isAiming) return;

        UpdatePreviewPosition();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            UseSkill();
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            CancelAim();
        }
    }

    public override void UseSkill()
    {
        if (!canUse) return;

        if (currentPreview == null) return;

        AudioController.instance.PlaySFXWithDuration(waterSkillClip, 0.5f);
        GameObject skill = Instantiate(skillPrefab, currentPreview.transform.position, Quaternion.identity);

        Destroy(currentPreview);

        isAiming = false;

        canUse = false;
        cooldownTimer = cooldown;
    }
}
