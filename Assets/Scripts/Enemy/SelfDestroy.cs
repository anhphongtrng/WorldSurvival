using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    EnemyController enemyController;

    private void Awake()
    {
        enemyController = EnemyController.instance;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyController.damageReceiver.IsDead())
        {
            Destroy(gameObject);
        }
    }
}
