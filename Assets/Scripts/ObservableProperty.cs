using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObservableProperty<T>
{
    private T _value;
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            Notify();
        }
    }
    
    private UnityEvent<T> _onValueChanged = new();
    
    public ObservableProperty(T value) => Init(value);
    
    private void Init(T value)
    {
        _value = value;
    }

    public void Subscribe(UnityAction<T> action)
    {
        _onValueChanged.AddListener(action);
    }

    public void Unsubscribe(UnityAction<T> action)
    {
        _onValueChanged.RemoveListener(action);
    }

    public void Notify()
    {
        _onValueChanged?.Invoke(Value);
    }
}
