using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    public float speed = 10f;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
              
                EventManager.Instance.EnemyEliminated(enemy.enemyType);
            }

            Destroy(other.gameObject); // Destruir el enemigo
            Destroy(gameObject); // Destruir el proyectil
        }
    }
}