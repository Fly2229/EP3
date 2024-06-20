using UnityEngine;

public class FastEnemy : Enemy
{
    void Start()
    {
        enemyType = EnemyType.Fast;
        speed = 6f;
    }

    public override void Move()
    {
        transform.Translate(Vector2.left * (speed * 1.5f) * Time.deltaTime);
    }
}