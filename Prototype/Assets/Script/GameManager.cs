using UnityEngine;
using UnityEngine.SceneManagement;

public enum CombatState
{
    ArenaOut,
    ArenaIn
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("PlayerRig Prefab")]
    public GameObject playerRigPrefab;

    private GameObject currentPlayerRig;

    public CombatState CurrentCombatState { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
            SetupScene();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupScene();
    }

    private void SetupScene()
    {
        // Instancie le PlayerRig s’il n’existe pas
        if (currentPlayerRig == null)
        {
            Transform spawnPoint = GameObject.Find("Spawn")?.transform;
            Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            currentPlayerRig = Instantiate(playerRigPrefab, spawnPosition, Quaternion.identity);
        }

        // Par défaut, on commence hors de l'arène
        SetCombatState(CombatState.ArenaOut);
    }

    public void SetCombatState(CombatState newState)
    {
        CurrentCombatState = newState;
        Debug.Log($"[GameManager] CombatState = {newState}");

        ShowWeapon(newState == CombatState.ArenaIn);
    }

    private void ShowWeapon(bool show)
    {
        if (currentPlayerRig == null) return;

        Transform weapon = currentPlayerRig.transform.Find("Weapon");
        if (weapon != null)
        {
            weapon.gameObject.SetActive(show);
        }
    }
}
