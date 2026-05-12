using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    protected bool isDead = false;
    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
