using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
