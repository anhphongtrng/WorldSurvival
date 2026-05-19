using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public PlayerMove playerMove;
    public PlayerDamageReceiver playerDamageReceiver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerMove = GetComponent<PlayerMove>();
        playerDamageReceiver = GetComponent<PlayerDamageReceiver>();
    }

}
