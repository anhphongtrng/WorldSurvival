using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerState();
    }

    public void PlayerState()
    {
        if (PlayerController.instance.playerMove.GetMoveInput() != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    public void TriggerHitting()
    {
        anim.SetTrigger("isHitting");
    }
}
