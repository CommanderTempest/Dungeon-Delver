using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
  [Export] private CharacterState currentState;
  [Export] private CharacterState[] states;

    public override void _Ready()
    {
        foreach (var state in states)
        {
          state.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        }

        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>()
    {
      CharacterState newState = states.Where((element) => element is T).FirstOrDefault();

      if (newState == null) {return;}
      if (currentState is T) {return;}
      if (!newState.CanTransition()) {return;}

      currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
      currentState = newState;
      currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
      
    }
}
