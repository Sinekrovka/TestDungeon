using UnityEngine;

public interface IUnit
{
    void Damage(int damage);
    void Attack(Transform aim);
    void ChangeAnimation(string animationName);
    float StartPosition();
    void DeselectUnit();

}
