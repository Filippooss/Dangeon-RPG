using Godot;
using System;

public partial class AttackHitBox : Area3D, IHitBox
{
    public bool CanStun()
    {
        return false;
    }


    public float GetDamage() => GetOwner<Characters>().GetStatResource(E_Stat.Strength).StatValue;
}
