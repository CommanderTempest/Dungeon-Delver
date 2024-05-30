using System;
using System.Reflection.Metadata.Ecma335;
using Godot;

[GlobalClass] // allows selection of this class in Godot when creating objects
public partial class StatResource : Resource
{
    public Action OnZero; // signal

    [Export] public Stat StatType {get; private set;}

    private float _statValue; // this is the variable the property below accesses
    [Export] public float StatValue 
    {
        get => _statValue;
        set 
        {
            _statValue = Mathf.Clamp(value, 0, Mathf.Inf);

            if (_statValue == 0)
            {
                OnZero?.Invoke();
            }
        }
    }

    

}