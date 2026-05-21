using UnityEngine;

public class ElectricalPanel : MonoBehaviour
{
    [Header("Door")]
    public Transform door;

    [Header("Switch")]
    public Collider switchCollider;

    [Header("Open Settings")]
    public float openAngle = -90f;
    public float openSpeed = 2f;

    [Header("Interactable")]
    public Interactable interactable;

    [Header("Sound")]
    public AudioSource openSound;

    // ALL BOX COLLIDERS
    private Collider[] boxColliders;

    private bool isOpen = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = door.localRotation;

        openRotation =
            Quaternion.Euler(
                door.localEulerAngles +
                new Vector3(0, openAngle, 0)
            );

        // AUTO FIND INTERACTABLE
        if (interactable == null)
        {
            interactable =
                GetComponent<Interactable>();
        }

        // GET ALL COLLIDERS
        boxColliders =
            GetComponentsInChildren<Collider>();
    }

    void Update()
    {
        // ONLY WORK WHEN ZOOMED
        if (interactable == null ||
            !interactable.isZoomed)
        {
            return;
        }

        // PRESS E
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;

            // PLAY SOUND
            if (openSound != null)
            {
                openSound.Play();
            }

            // DISABLE BOX COLLIDERS
            foreach (Collider col in boxColliders)
            {
                // KEEP SWITCH COLLIDER ACTIVE
                if (col != switchCollider)
                {
                    col.enabled = !isOpen;
                }
            }
        }

        // OPEN/CLOSE DOOR
        if (door != null)
        {
            door.localRotation =
                Quaternion.Lerp(
                    door.localRotation,
                    isOpen ? openRotation : closedRotation,
                    Time.deltaTime * openSpeed
                );
        }
    }
}