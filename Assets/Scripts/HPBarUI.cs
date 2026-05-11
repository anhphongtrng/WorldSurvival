using UnityEngine;

public class HPBarUI : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}