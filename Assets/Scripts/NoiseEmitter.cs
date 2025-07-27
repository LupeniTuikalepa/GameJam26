using UnityEngine;

public class NoiseEmitter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float noiseRadius = 5f;
    //[SerializeField] private NoiseEmitter noiseEmitter;
    private bool playerDetected = false;
    public GameObject noiseVisual;
    [SerializeField] private float maxScale = 5f;   // Taille max du halo
    [SerializeField] private float scaleSpeed = 1f; //
    private Vector3 initialScale;
    private float pulseSpeed; // Vitesse qui augmente
    private float timeInside = 0f;
  // Vitesse de pulsation de départ
    [SerializeField] private float pulseAcceleration = 0.5f; // Augmentation de la vitesse
    [SerializeField] private float minScale = 0.8f;
    [SerializeField] private float basePulseSpeed = 1f;   
    private void Start()
    {
        // Désactive le visuel au lancement
        if (noiseVisual != null)
            noiseVisual.SetActive(false);
        if (noiseVisual != null)
        {
            initialScale = noiseVisual.transform.localScale;
            noiseVisual.SetActive(false);
        }
        
        
    }
    private void Update()
    {
        if (playerDetected && noiseVisual != null)
        {
            timeInside += Time.deltaTime;
            pulseSpeed = basePulseSpeed + timeInside * pulseAcceleration;

            // Calcul de la pulsation avec un sin (oscillation)
            float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            noiseVisual.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
            timeInside = 0f;
            noiseVisual.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            noiseVisual.SetActive(false);

        }
    }
    
}
    