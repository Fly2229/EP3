using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]protected float speed = 3f;
    public EnemyType enemyType;

    public virtual void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void Update()
    {
        Move();
    }

}