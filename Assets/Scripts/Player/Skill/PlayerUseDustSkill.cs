using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseDustSkill : PlayerSkill
{
    [SerializeField] private int dustCount = 8;
    [SerializeField] private float radius = 4f;
    [SerializeField] private float skillDuration = 3f;

    [Header("Music")]
    public AudioClip dustSkillClip; // Music played when using the dust skill

    private void Start()
    {
        cooldown = 5f;
    }

    public override void HandleAim()
    {
        if (!skillSlots[0].isUnlocked) return;

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            UseSkill();
        }
    }

    public override void UseSkill()
    {
        if (!canUse) return;
        
        AudioController.instance.PlaySFXWithDuration(dustSkillClip, skillDuration);
        for (int i = 0; i < dustCount; i++)
        {          
            float angle = i * Mathf.PI * 2 / dustCount;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector3 spawnPos = transform.position + new Vector3(x, y, 0);

            GameObject dust = Instantiate(skillPrefab, spawnPos, Quaternion.identity);
            dust.transform.SetParent(transform);
            Destroy(dust, skillDuration);
        }

        canUse = false;
        cooldownTimer = cooldown;
    }
}