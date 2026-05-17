using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseDustSkill : PlayerSkill
{
    [SerializeField] private int dustCount = 8;
    [SerializeField] private float radius = 4f;
    private void Start()
    {
        cooldown = 5f;
    }

    public override void HandleAim()
    {
        if (!skillSlots[0].isUnlocked) return;

        // Nhấn R dùng skill luôn
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            UseSkill();
        }
    }

    public override void UseSkill()
    {
        if (!canUse) return;

        for (int i = 0; i < dustCount; i++)
        {
            // Góc hiện tại
            float angle =
                i * Mathf.PI * 2 / dustCount;

            // Tính vị trí trên vòng tròn
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector3 spawnPos =
                transform.position + new Vector3(x, y, 0);

            Instantiate(
                skillPrefab,
                spawnPos,
                Quaternion.identity
            );
        }

        canUse = false;
        cooldownTimer = cooldown;
    }
}