using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Delegado y evento para la destrucción de enemigos
    public delegate void EnemyDestroyed(EnemyType enemyType);
    public event EnemyDestroyed OnEnemyDestroyed;

    // Método para manejar la eliminación de enemigos
    public void EnemyEliminated(EnemyType enemyType)
    {
        OnEnemyDestroyed?.Invoke(enemyType);

        Debug.Log("Enemy destroyed. Type: " + enemyType); // Agrega este mensaje de depuración
    }
}