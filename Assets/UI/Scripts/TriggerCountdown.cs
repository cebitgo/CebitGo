using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TriggerCountdown : MonoBehaviour {

    [SerializeField]
    private float CountdownTime = 10f;
    bool TimerStarted = false;
    public UnityEvent OnTimerElapsed;
    private Slider _progressSlider;
    private bool _visited = false;

    private void Awake()
    {
        _progressSlider = GetComponentInChildren<Slider>();
    }

    void OnTriggerEnter()
    {
        if (!TimerStarted && !_visited) TimerStarted = true;
    }

    void OnTriggerExit()
    {
        if (TimerStarted) TimerStarted = false;
        _progressSlider.value = 0;
        _timer = 0;
    }

    private float _timer = 0f;

    void Update()
    {
        if (TimerStarted && !_visited)
        {
            _timer += Time.deltaTime;
            _progressSlider.value = _timer / CountdownTime;
            
            if (_timer >= CountdownTime)
            {
                _visited = true;
                OnTimerElapsed.Invoke();
                _timer = 0;
            }
        }
    }
}
