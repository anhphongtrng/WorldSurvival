using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseFireSkill : PlayerSkill
{

    private void Start()
    {
        cooldown = 3f;
    }
    public override void HandleAim()
    {
        if (!skillSlots[0].isUnlocked) return;
        if (Keyboard.current.qKey.wasPressedThisFrame)
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
        Vector3 startPos = transform.position;

        Vector3 targetPos = currentPreview.transform.position;

        Vector3 direction =
            (targetPos - startPos).normalized;

        StartCoroutine(
            SpawnFireLine(startPos, direction)
        );

        Destroy(currentPreview);

        isAiming = false;

        canUse = false;
        cooldownTimer = cooldown;
    }

    IEnumerator SpawnFireLine(Vector3 startPos, Vector3 direction)
    {
        int fireCount = 8;
        float spacing = 2f;

        for (int i = 1; i < fireCount; i++)
        {
            Vector3 spawnPos =
                startPos + i * spacing * direction;

            Instantiate(
                skillPrefab,
                spawnPos,
                Quaternion.identity
            );

            yield return new WaitForSeconds(0.5f);
        }
    }

}
