using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [Header("Skill")]
    [SerializeField] private GameObject waterSkillPrefab;

    [Header("Aim")]
    [SerializeField] private GameObject previewPrefab;

    [Header("Cooldown")]
    [SerializeField] private float cooldown = 5f;

    [Header("UI")]
    [SerializeField] private Image cooldownOverlay;

    private float cooldownTimer;
    private bool canUse = true;

    private bool isAiming;
    private GameObject currentPreview;

    private Vector3 mousePos;

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

    void HandleAim()
    {
        // Nhấn E để bắt đầu aim
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartAim();
        }

        if (!isAiming) return;

        // Preview đi theo chuột
        UpdatePreviewPosition();

        // Chuột trái -> cast skill
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            UseSkill();
        }

        // Chuột phải -> huỷ aim
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            CancelAim();
        }
    }

    void StartAim()
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

    void UpdatePreviewPosition()
    {
        if (currentPreview == null) return;

        currentPreview.transform.position = mousePos;
    }

    void UseSkill()
    {
        if (!canUse) return;

        if (currentPreview == null) return;

        GameObject skill = Instantiate(
            waterSkillPrefab,
            currentPreview.transform.position,
            Quaternion.identity
        );

        Debug.Log("Cast skill at: " + currentPreview.transform.position);
        Debug.Log("Spawned skill position: " + skill.transform.position);

        Destroy(currentPreview);

        isAiming = false;

        canUse = false;
        cooldownTimer = cooldown;
    }

    void CancelAim()
    {
        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        isAiming = false;
    }

    void HandleCooldown()
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