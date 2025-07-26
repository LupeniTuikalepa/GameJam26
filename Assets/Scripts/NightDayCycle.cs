using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NightDayCycle : MonoBehaviour
{
    [System.Serializable]
    public struct DayAndNightMark
    {
        [SerializeField] public float timeRatio;
        [SerializeField] public Color color; 
        [SerializeField] public float intensity;
    };

    private const float _TIME_CHECK_EPSILON = 0.01f;
    private float _currentCycleTime;
    [SerializeField] private DayAndNightMark[] marks;
    [SerializeField] private float cycleDuration = 24; //en seconde
    [SerializeField] private Light2D light2D;
    private int _currentMarkIndex, _nextMarkIndex;
    private float _currentMarkTime, _nextMarkTime;
    private float _marksTimeDifference;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentMarkIndex = -1;
        CycleMarks();
    }

    // Update is called once per frame
    void Update()
    {
        float t = (_currentCycleTime - _currentMarkTime) / _marksTimeDifference;
        _currentCycleTime = (_currentCycleTime + Time.deltaTime) % cycleDuration;
        //blend color
        DayAndNightMark curr = marks[_currentMarkIndex], next = marks[_nextMarkIndex];
        light2D.color = Color.Lerp(curr.color, next.color, t);
        light2D.intensity = Mathf.Lerp(curr.intensity, next.intensity, t);
        //passed mark
        if (Mathf.Abs(_currentCycleTime - _nextMarkTime) < _TIME_CHECK_EPSILON)
        {
            light2D.color = next.color;
            light2D.intensity = next.intensity;
            CycleMarks();

        }
    }

    private void CycleMarks()
    {
        _currentMarkIndex = (_currentMarkIndex + 1)  % marks.Length;
        _nextMarkIndex = (_currentMarkIndex + 1)  % marks.Length;
        _currentMarkTime = marks[_currentMarkIndex].timeRatio * cycleDuration;
        _nextMarkTime = marks[_nextMarkIndex].timeRatio * cycleDuration;
        _marksTimeDifference = _nextMarkTime - _currentMarkTime;

        if (_marksTimeDifference < 0)
        {
            _marksTimeDifference += cycleDuration; 
        }
        Debug.Log($"Switched to mark {_currentMarkIndex} → {marks[_currentMarkIndex].color}, intensity {marks[_currentMarkIndex].intensity}");

    }
}
