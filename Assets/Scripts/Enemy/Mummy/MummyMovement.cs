using Unity.VisualScripting;
using UnityEngine;

public class MummyMovement : EnemyMovement
{
    protected override void Awake()
    {
        moveSpeed = 3f;
    }

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    public override void Move()
    {
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < 10f)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeed * Time.deltaTime);
            FlipEnemy();
        }
    }
}
