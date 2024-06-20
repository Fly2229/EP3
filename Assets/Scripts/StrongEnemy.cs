using UnityEngine;

public class StrongEnemy : Enemy
{
    void Start()
    {
        enemyType = EnemyType.Strong;
        speed = 1f;
    }

    public override void Move()
    {
        transform.Translate(Vector2.left * (speed * 0.5f) * Time.deltaTime);
    }
}