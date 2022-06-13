using Spine.Unity;
using UnityEngine;

public class Miner : Warrior, IUnit
{
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _HP;
   
    private SkeletonAnimation _animationScript;
    private float startPosition;

    private void Awake()
    {
        _animationScript = GetComponentInChildren<SkeletonAnimation>();
        startPosition = transform.position.x;
        HealthIndicatorInit(_HP);
    }
    
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
        ShowSelected();
    }
    
    public void Attack(Transform aim)
    {
        HideSelected();
        IUnit aimInterface = aim.GetComponent<IUnit>();
        aimInterface.Damage(Random.Range(0, _maxDamage));
        ChangeAnimation("PickaxeCharge");
        StartCoroutine(MovingSelectedWarriors.WaitAnimation(1.6f, transform, 
            aim, startPosition, aimInterface.StartPosition()));
        
    }

    public void Damage(int damage)
    {
        HideSelected();
        if (damage.Equals(0))
        {
            ShowDamage("УКЛОНЕНИЕ");
        }
        else
        {
            ShowDamage(damage.ToString());
            _HP -= damage;
            if (_HP <= 0)
            {
                Death();
            }
            else
            {
                ChangeAnimation("Damage");
            }
            GetDamage(_HP);
        }
    }

    private void Death()
    {
        _HP = 0;
        transform.tag = "DeadBody";
        ChangeAnimation("Pull");
        DeadBody();
    }

    public void ChangeAnimation(string animationName)
    {
        _animationScript.AnimationName = animationName;
    }

    public float StartPosition()
    {
        return startPosition;
    }

    public void DeselectUnit()
    {
        HideSelected();
    }
}
