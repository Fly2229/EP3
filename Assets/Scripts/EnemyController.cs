using UnityEngine;

public enum EnemyType
{
    Normal,
    Fast,
    Strong
}

public class EnemyController : MonoBehaviour
{
    public EnemyType enemyType;
    public float speed;

    void Start()
    {

        Debug.Log("Suscribing to OnEnemyDestroyed event.");
        EventManager.Instance.OnEnemyDestroyed += HandleEnemyDestroyed;
        // Inicializa propiedades basadas en el tipo de enemigo
        switch (enemyType)
        {
            case EnemyType.Normal:
                speed = 2f;
                break;
            case EnemyType.Fast:
                speed = 4f;
                break;
            case EnemyType.Strong:
                speed = 1f;
                break;
        }
    }

    void Update()
    {
        // Movimiento del enemigo
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Lógica para dañar al jugador
            Destroy(gameObject);
        }
    }

    private void HandleEnemyDestroyed(EnemyType enemyType)
    {
        Debug.Log("Enemy destroyed. Type: " + enemyType);
        // Más lógica para manejar la destrucción de enemigos, si es necesario
    }
}