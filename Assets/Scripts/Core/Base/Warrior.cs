using UnityEngine;

public class Warrior : MonoBehaviour, IUnit
{
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _HP;
    private string _currentAnimation;
    
    private void OnMouseDown()
    {
        if (transform.CompareTag("Player"))
        {
            ActionController.Instance.SetTarget(gameObject);
        }
        else
        {
            ActionController.Instance.SetAim(gameObject);
        }
    }
    
    public void Attack(IUnit aim)
    {
        _currentAnimation = "Attack";
        aim.Damage(Random.Range(0, _maxDamage));
    }

    public void Damage(int damage)
    {
        _currentAnimation = "Damage";
        _HP -= damage;
        if (_HP <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        _currentAnimation = "Death";
        transform.tag = "DeadBody";
    }
}
