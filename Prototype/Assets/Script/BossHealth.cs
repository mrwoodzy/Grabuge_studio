using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Boss prend {amount} dégâts. Vie restante : {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss vaincu !");
        // Tu peux désactiver le boss, lancer une anim, etc.
        gameObject.SetActive(false);
    }
}
