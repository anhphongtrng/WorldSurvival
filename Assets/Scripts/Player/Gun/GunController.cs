using UnityEngine;

public class GunController : MonoBehaviour
{
    public GunShooting gunShooting;

    private void Awake()
    {
        gunShooting = GetComponent<GunShooting>();
    }
}
