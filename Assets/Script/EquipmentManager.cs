using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class EquipmentConfig
{
    public string label;
    public GameObject equipmentObj;
    public GameObject targetSocket;  // both the snap target and the hover object to disable

    // Runtime state (not exposed in Inspector)
    [System.NonSerialized] public bool isSnapped;
}

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    public EquipmentConfig[] configs;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        SetupAllEquipment();
    }

    void SetupAllEquipment()
    {
        foreach (var eq in configs)
        {
            if (eq.equipmentObj == null) continue;

            // Auto-attach trigger handler to forward OnTriggerEnter to the manager
            var handler = eq.equipmentObj.AddComponent<EquipmentTriggerHandler>();
            handler.config = eq;
        }
    }

    // ── Called by EquipmentTriggerHandler ──────────────────────────

    public void OnTriggerEntered(EquipmentConfig config, Collider other)
    {
        if (config.targetSocket == null) return;
        if (other.gameObject != config.targetSocket) return;

        config.isSnapped = true;

        // Reparent to the same parent as the socket (same hierarchy level)
        config.equipmentObj.transform.SetParent(config.targetSocket.transform.parent);

        // Snap to the socket's position & rotation
        config.equipmentObj.transform.position = config.targetSocket.transform.position;
        config.equipmentObj.transform.rotation = config.targetSocket.transform.rotation;

        // Hide the socket / hover indicator
        if (config.targetSocket != null)
            config.targetSocket.SetActive(false);

        // Disable grabbing after snap
        var grab = config.equipmentObj.GetComponent<XRGrabInteractable>();
        if (grab != null)
            grab.enabled = false;
    }
}

/// <summary>
/// Tiny runtime component auto-attached by EquipmentManager.
/// Forwards OnTriggerEnter to the manager so individual objects
/// don't need any script.
/// </summary>
public class EquipmentTriggerHandler : MonoBehaviour
{
    public EquipmentConfig config;

    void OnTriggerEnter(Collider other)
    {
        if (EquipmentManager.instance != null)
            EquipmentManager.instance.OnTriggerEntered(config, other);
    }
}
