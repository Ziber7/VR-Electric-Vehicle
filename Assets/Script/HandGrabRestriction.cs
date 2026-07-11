using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class HandGrabRestriction : MonoBehaviour
{
    public enum HandOption { Left, Right }
    [SerializeField] private GameObject HandObject;
    public Material NormalHandMaterial;

    public GameObject Glove;
    public Material GloveMaterial;

    [Header("Hand Restriction")]
    [Tooltip("Which hand is allowed to grab this object.")]
    public HandOption allowedHand = HandOption.Left;

    private XRGrabInteractable grabInteractable;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError($"HandGrabRestriction: Missing XRGrabInteractable on {gameObject.name}");
            return;
        }

        originalPosition = transform.position;
        originalRotation = transform.rotation;

        grabInteractable.selectEntered.AddListener(OnSelectEntered);
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!IsCorrectHand(args.interactorObject))
        {
            StartCoroutine(ReleaseNextFrame(args.interactorObject));
            ResetPosition();
        } else
        {
            StartCoroutine(ReleaseNextFrame(args.interactorObject));
            ResetPosition();
            ChangeMaterialHand();
        }
    }

    private bool IsCorrectHand(IXRSelectInteractor interactor)
    {
        var mb = interactor as MonoBehaviour;
        if (mb == null) return true;

        // Walk the transform hierarchy looking for "Left"/"Right" in names.
        // Standard XR Rigs name controller GameObjects like "LeftHand Controller".
        Transform t = mb.transform;
        while (t != null)
        {
            string n = t.name.ToLower();
            if (n.Contains("left"))  return allowedHand == HandOption.Left;
            if (n.Contains("right")) return allowedHand == HandOption.Right;
            t = t.parent;
        }

        // Can't determine the hand → allow the grab
        return true;
    }

    private IEnumerator ReleaseNextFrame(IXRSelectInteractor interactor)
    {
        // Wait one frame so the selection is fully established,
        // then force-release from the wrong hand.
        yield return null;

        if (grabInteractable != null && grabInteractable.isSelected)
        {
            var manager = grabInteractable.interactionManager;
            if (manager != null)
                manager.SelectExit(interactor, grabInteractable);
        }
    }

    public void ChangeMaterialHand()
    {
        // Implementation for changing hand material

        // Disable XR Grab Interactable on the glove
        XRGrabInteractable XRGGlove = Glove.GetComponent<XRGrabInteractable>();
        XRGGlove.enabled = false;

        SkinnedMeshRenderer handRenderer = HandObject.GetComponent<SkinnedMeshRenderer>();
        handRenderer.material = NormalHandMaterial;

        // change the material of the glove
        MeshRenderer gloveRenderer = Glove.GetComponent<MeshRenderer>();
        gloveRenderer.material = GloveMaterial;
    }


}
