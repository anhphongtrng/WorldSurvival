using UnityEngine;

public class BossHPBarUI : MonoBehaviour
{
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.worldCamera = Camera.main;
        }
    }
}
