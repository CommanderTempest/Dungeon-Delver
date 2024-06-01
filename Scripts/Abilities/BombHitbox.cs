using Godot;
using System;

public partial class BombHitbox : Area3D, IHitbox
{
  public float GetDamage()
  {
    return GetOwner<Ability>().Damage;
  }

  public bool CanStun()
  {
    return true;
  }
}
