using System.Collections;
using UnityEngine;

namespace NavKeypad
{
    public class KeypadButtonUnlocked : MonoBehaviour
    {
        [Header("Value")]
        [SerializeField] private string value;

        [Header("Button Animation Settings")]
        [SerializeField] private float bttnspeed = 0.1f;
        [SerializeField] private float moveDist = 0.0025f;
        [SerializeField] private float buttonPressedTime = 0.1f;

        [Header("Component References")]
        [SerializeField] private KeypadUnlocked keypad;
        [SerializeField] private CameraController cameraController;

        private bool moving;

        void Start()
        {
            if (cameraController == null && Camera.main != null)
                cameraController = Camera.main.GetComponent<CameraController>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                TryClick();
        }

        void TryClick()
        {
            if (Camera.main == null) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                KeypadButtonUnlocked clickedButton = hit.collider.GetComponentInParent<KeypadButtonUnlocked>();

                if (clickedButton == this)
                    PressButton();
            }
        }

        public void PressButton()
        {
            if (moving) return;

            if (cameraController != null && !cameraController.IsInteracting())
            {
                Debug.LogWarning("Keypad button blocked because camera is not interacting: " + gameObject.name);
                return;
            }

            if (keypad == null)
            {
                Debug.LogWarning("Keypad reference is missing on button: " + gameObject.name);
                return;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                Debug.LogWarning("Button value is empty on: " + gameObject.name);
                return;
            }

            keypad.AddInput(value.Trim().ToLower());

            StartCoroutine(MoveSmooth());
        }

        private IEnumerator MoveSmooth()
        {
            moving = true;

            Vector3 startPos = transform.localPosition;
            Vector3 endPos = transform.localPosition + new Vector3(0, 0, moveDist);

            float elapsedTime = 0f;
            while (elapsedTime < bttnspeed)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / bttnspeed);
                transform.localPosition = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }

            transform.localPosition = endPos;

            yield return new WaitForSeconds(buttonPressedTime);

            startPos = transform.localPosition;
            endPos = transform.localPosition - new Vector3(0, 0, moveDist);

            elapsedTime = 0f;
            while (elapsedTime < bttnspeed)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / bttnspeed);
                transform.localPosition = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }

            transform.localPosition = endPos;

            moving = false;
        }
    }
}