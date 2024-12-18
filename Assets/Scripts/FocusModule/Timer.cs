using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action<Task> OnTimerEnd; // Event to propagate when timer ends#
    public event Action<string> OnTimerTick; // event to update the timer


    uint _totalSeconds;
    uint _secondsRemaining;
    uint _hours;
    uint _minutes;
    uint _seconds;

    public uint maxHours = 24;
    public bool isTimerTicking;

    public Timer(uint seconds)
    {
        _totalSeconds = seconds;

        _hours = seconds / 3600;
        _minutes = seconds % 3600 / 60;
        _seconds = seconds % 60;
    }

    public uint Hours 
    { 
        get { return _hours; }
        set
        {
            if (value < maxHours)
            {
                _hours = value;
            }
            else
            {
                throw new Exception($"Hours cannot be set to {value}, because "
                + $"it is greater than {maxHours}.");
            }
        }
    }

    public uint Minutes 
    { 
        get { return _minutes; }
        set
        {
            if (value < 60)
            {
                _minutes = value;
            }
            else
            {
                throw new Exception($"Minutes cannot be set to {value}, because "
                + "it is greater than 59.");
            }
        }
    }

    public uint Seconds
    { 
        get { return _seconds; }
        set
        {
            if (value < 60)
            {
                _seconds = value;
            }
            else
            {
                throw new Exception($"Seconds cannot be set to {value}, because "
                + "it is greater than 59.");
            }
        }
    }


    public override string ToString()
    {
        return Hours.ToString() + " : " + Minutes.ToString() + " : " + Seconds.ToString();
    }

    
    // This is what we call a "Coroutine", as indicated by the "yield return"
    public IEnumerator StartClock(Task activatedTask)
    {
        _secondsRemaining = _totalSeconds;
        isTimerTicking = true;

        while (_secondsRemaining > 0)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            _secondsRemaining -= 1;

            // Update hours, minutes, and seconds based on remaining time
            _hours = _secondsRemaining / 3600;
            _minutes = _secondsRemaining % 3600 / 60;
            _seconds = _secondsRemaining % 60;

            Debug.Log(ToString()); // Optional: Log the current time

            // Triggers the event with the updated format
            OnTimerTick?.Invoke(ToString());
        }

        isTimerTicking = false;
        Debug.Log("Timer finished!");
        
        // a Task object to be passed to the event 
        OnTimerEnd?.Invoke(activatedTask);
    }
}