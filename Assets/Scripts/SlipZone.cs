using UnityEngine;
using System.Collections;

public class SlipZone : MonoBehaviour
{
    public float recoverTime = 4f;

    public AudioSource slipAudio;

    private bool hasSlipped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (hasSlipped)
            return;

        StartCoroutine(SlipPlayer(other));
    }

    IEnumerator SlipPlayer(Collider playerCol)
    {
        hasSlipped = true;

        PlayerMovement movement =
            playerCol.GetComponent<PlayerMovement>();

        Animator animator =
            playerCol.GetComponent<Animator>();

        Rigidbody rb =
            playerCol.GetComponent<Rigidbody>();

        // STOP MOVEMENT SCRIPT
        if (movement != null)
        {
            movement.enabled = false;
            movement.footstepAudio.Stop();
        }

        // STOP PHYSICS MOVEMENT
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // PLAY SLIP
        if (animator != null)
        {
            animator.SetBool("isWalking", false);

            animator.ResetTrigger("Slip");
            animator.SetTrigger("Slip");
        }

        // PLAY SOUND
        if (slipAudio != null)
        {
            slipAudio.Play();
        }

        // FORCE STOP INPUT EVERY FRAME
        float timer = recoverTime;

        while (timer > 0)
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            timer -= Time.deltaTime;

            yield return null;
        }

        // ENABLE MOVEMENT AGAIN
        if (movement != null)
        {
            movement.enabled = true;
        }

        hasSlipped = false;
    }
}