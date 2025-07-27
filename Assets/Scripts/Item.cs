using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public bool IsTaken { get; private set; }
    public string ItemId { get; private set; }
    public GameObject itemPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemId ??= GlobalHelper.GenerateUniqueId(itemPrefab);
    }


    public bool CanInteract()
    {
        return !IsTaken;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        TakeItem();
    }

    private void TakeItem()
    {
        // Set IsTaken
        setTaken(true);
        // Destroy Item
        if (itemPrefab)
        {
            Destroy(itemPrefab);
        }
    }

    public void setTaken(bool taken)
    {
        IsTaken = taken;
    }
}
