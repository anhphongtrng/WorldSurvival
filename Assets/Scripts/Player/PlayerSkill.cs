using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class PlayerSkill : MonoBehaviour
{
    [Header("Skill Slot")]
    [SerializeField] protected SkillSlot[] skillSlots;

    [Header("Skill")]
    [SerializeField] protected GameObject skillPrefab;

    [Header("Aim")]
    [SerializeField] protected GameObject previewPrefab;

    [Header("Cooldown")]
    [SerializeField] protected float cooldown;

    [Header("UI")]
    [SerializeField] protected Image cooldownOverlay;

    protected float cooldownTimer;
    protected bool canUse = true;

    protected bool isAiming;
    protected GameObject currentPreview;

    protected Vector3 mousePos;

    void Update()
    {
        // Lấy vị trí chuột trong world
        mousePos = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );

        mousePos.z = 0;

        HandleCooldown();
        HandleAim();
    }

    public abstract void HandleAim();

    public void StartAim()
    {
        if (!canUse) return;

        // Tránh spawn nhiều preview
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        isAiming = true;

        currentPreview = Instantiate(
            previewPrefab,
            mousePos,
            Quaternion.identity
        );
    }

    public void UpdatePreviewPosition()
    {
        if (currentPreview == null) return;

        currentPreview.transform.position = mousePos;
    }

    public abstract void UseSkill();

    public void CancelAim()
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        isAiming = false;
    }

    public void HandleCooldown()
    {
        if (canUse)
        {
            cooldownOverlay.fillAmount = 0;
            return;
        }

        cooldownTimer -= Time.deltaTime;

        cooldownOverlay.fillAmount =
            cooldownTimer / cooldown;

        if (cooldownTimer <= 0)
        {
            canUse = true;
            cooldownTimer = 0;
        }
    }
}