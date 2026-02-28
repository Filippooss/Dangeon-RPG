using Godot;
using System;

public partial class AbilityHitBox : Area3D, IHitBox {
    public bool CanStun() => true;
    
    public float GetDamage() => GetOwner<Ability>().Damage;
}
