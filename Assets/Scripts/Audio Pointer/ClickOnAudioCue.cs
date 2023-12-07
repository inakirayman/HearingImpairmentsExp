using ProjectArtic.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnAudioCue : MonoBehaviour
{
    [SerializeField] private float _raycastDistance = 10f;
    [SerializeField] private LayerMask _layerMask; // Specify the layer mask in the Unity editor

    private InputManager _inputManager;

    private void Start()
    {
        _inputManager = InputManager.Instance;
    }

    void Update()
    {
        if (_inputManager.GetMouseButton1WasPressedThisFrame())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform raycast with layer mask
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _raycastDistance, _layerMask))
            {
                // Check if the hit object has an AudioCue component
                AudioCue audioCue = hit.collider.GetComponent<AudioCue>();
                if (audioCue != null)
                {
                    // Call the Clicked method on the AudioCue
                    audioCue.Clicked();
                }
            }
        }
    }
}
