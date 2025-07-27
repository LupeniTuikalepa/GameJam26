using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private Image HealthImage;
    private PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindFirstObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement == null) return;
        float healthNormalized = playerMovement.Health / playerMovement.MaxHealth;
        HealthImage.fillAmount = healthNormalized;
    }
}
