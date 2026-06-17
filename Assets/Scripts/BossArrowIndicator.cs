using UnityEngine;
using UnityEngine.UI;

public class BossArrowIndicator : MonoBehaviour
{
    public Transform boss;
    public Transform player;
    public Camera mainCamera;
    public RectTransform arrowRect;
    public float radius = 300f;

    void Update()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (boss == null) { gameObject.SetActive(false); return; }

        Vector3 screenPos = mainCamera.WorldToScreenPoint(boss.position);
        bool isOnScreen = screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height;

        if (isOnScreen)
        {
            arrowRect.gameObject.SetActive(false);
            return;
        }

        arrowRect.gameObject.SetActive(true);

        Vector3 dir = (boss.position - player.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrowRect.rotation = Quaternion.Euler(0, 0, angle - 90f);

        Vector3 playerScreenPos = mainCamera.WorldToScreenPoint(player.position);

        Vector2 dirOnScreen = (Vector2)dir.normalized;

        Vector3 arrowPos = playerScreenPos + (Vector3)(dirOnScreen * radius);
        arrowRect.position = arrowPos;
    }
}