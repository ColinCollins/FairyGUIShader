using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{

    private static Dictionary<string, Delegate> _eventListeners = new Dictionary<string, Delegate>();

    public static void AddEvent<T>(string key, Action<T> listener)
    {
        if (string.IsNullOrEmpty(key))
        {
            return;
        }

        if (_eventListeners.TryGetValue(key, out Delegate old))
        {
            _eventListeners[key] = Delegate.Combine(old, listener);
        }
        else
        {
            _eventListeners[key] = listener;
        }
    }

    public static void AddEvent(string key, Action listener)
    {
        if (string.IsNullOrEmpty(key))
        {
            return;
        }

        if (_eventListeners.TryGetValue(key, out Delegate old))
        {
            _eventListeners[key] = Delegate.Combine(old, listener);
        }
        else
        {
            _eventListeners[key] = listener;
        }
    }

    public static void InvokeEvent<T>(string key, T value)
    {
        if (!_eventListeners.TryGetValue(key, out Delegate @delegate))
        {
            return;
        }

        if (@delegate is Action<T> action)
        {
            action.Invoke(value);
        }
    }

    public static void InvokeEvent(string key)
    {
        if (!_eventListeners.TryGetValue(key, out Delegate @delegate))
        {
            return;
        }

        if (@delegate is Action action)
        {
            action.Invoke();
        }
    }

    public static void RemoveEvent<T>(string key, Action<T> listener)
    {
        if (string.IsNullOrEmpty(key)) return;

        if (!_eventListeners.TryGetValue(key, out Delegate source)) return;

        _eventListeners[key] = Delegate.Remove(source, listener);
    }

    public static void RemoveEvent(string key, Action listener)
    {
        if (string.IsNullOrEmpty(key)) return;

        if (!_eventListeners.TryGetValue(key, out Delegate source)) return;

        _eventListeners[key] = Delegate.Remove(source, listener);
    }

    public static void RemoveEvent(string key)
    {
        if (string.IsNullOrEmpty(key)) return;

        if (!_eventListeners.TryGetValue(key, out Delegate source)) return;

        _eventListeners.Remove(key);
    }

    public static void RemoveAllEvent()
    {
        _eventListeners.Clear();
    }
}
