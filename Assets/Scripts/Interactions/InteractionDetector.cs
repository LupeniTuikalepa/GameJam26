using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;

    [SerializeField] private PlayerMovement playerMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionIcon.SetActive(false);

    }

    public void OnInteract(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            playerMovement.TriggerInteractionAnimation();
            StartCoroutine(HandleInteraction());
        }
    }

    private System.Collections.IEnumerator HandleInteraction()
    {
        yield return new WaitForSeconds(0.5f);
        
        interactableInRange?.Interact();
        
        playerMovement.StopInteractionAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
