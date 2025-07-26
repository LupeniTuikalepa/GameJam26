using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightDayCycle : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    private float _cycleTimer; //en seconde
    [SerializeField] private float cycleMax = 24; //en seconde

    [SerializeField] private Light2D light2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float t = _cycleTimer / cycleMax;
        light2D.color = gradient.Evaluate(t);
        _cycleTimer = (_cycleTimer + Time.deltaTime) % cycleMax;
    }
}
