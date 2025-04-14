using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class ToggleRayCaster : MonoBehaviour
{
    private PhysicsRaycaster physicsRaycaster;

    void Awake()
    {
        // Get or add the PhysicsRaycaster
        physicsRaycaster = GetComponent<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            physicsRaycaster = gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    // Call this to enable the PhysicsRaycaster
    public void EnableRaycaster()
    {
        if (physicsRaycaster != null)
        {
            physicsRaycaster.enabled = true;
            Debug.Log("PhysicsRaycaster enabled.");
        }
    }

    // Call this to disable the PhysicsRaycaster
    public void DisableRaycaster()
    {
        if (physicsRaycaster != null)
        {
            physicsRaycaster.enabled = false;
            Debug.Log("PhysicsRaycaster disabled.");
        }
    }
}