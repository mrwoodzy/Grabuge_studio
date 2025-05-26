using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damageAmount = 10;
    public float attackRange = 2f;
    public LayerMask bossLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // touche F pour attaquer
        {
            Debug.Log("Attaque du joueur !");

            // Raycast vers l'avant du joueur pour détecter un boss
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackRange, bossLayer))
            {
                if (hit.collider.TryGetComponent(out BossHealth boss))
                {
                    boss.TakeDamage(damageAmount);
                }
            }
        }
    }
}
