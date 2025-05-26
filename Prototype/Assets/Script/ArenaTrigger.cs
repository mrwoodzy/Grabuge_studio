using UnityEngine;

public class ArenaTrigger : MonoBehaviour
{
    public CombatState triggerState = CombatState.ArenaIn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetCombatState(triggerState);
        }
    }
}
