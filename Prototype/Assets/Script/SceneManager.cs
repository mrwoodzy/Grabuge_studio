using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class SceneManagerLocal : MonoBehaviour
{
    [Header("Spawn Settings")]
    private Vector3 spawnPosition = Vector3.zero;
    private Color gizmoColor = new Color(0.2f, 0.8f, 1f, 0.8f);
    private float gizmoRadius = 0.5f;

    private Transform spawnTransform;

    void Start()
    {
        // Assure la présence d’un dossier SceneSetup
        Transform setupRoot = GameObject.Find("SceneSetup")?.transform;
        if (setupRoot == null)
        {
            GameObject root = new GameObject("SceneSetup");
            setupRoot = root.transform;
        }

        // Crée un Spawn s’il n’existe pas
        GameObject existingSpawn = GameObject.Find("Spawn");

        if (existingSpawn == null)
        {
            GameObject spawn = new GameObject("Spawn");
            spawn.transform.position = spawnPosition; 
            spawn.transform.SetParent(setupRoot);
            spawnTransform = spawn.transform;
        }
        else
        {
            spawnTransform = existingSpawn.transform;
            existingSpawn.transform.SetParent(setupRoot);
        }

        // Se placer sous SceneSetup
        if (transform.parent != setupRoot)
        {
            transform.SetParent(setupRoot);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Vector3 previewPosition = spawnTransform != null
            ? spawnTransform.position
            : transform.position + spawnPosition;

        Gizmos.DrawSphere(previewPosition, gizmoRadius);
    }
}
