using DG.Tweening;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    private IUnit _tagetWarrior;
    private IUnit _currentAim;
    private bool _attack;
    public static ActionController Instance;

    private Transform _warrior;
    private Transform _victim;

    private void Awake()
    {
        Instance = this;
        _attack = false;
    }

    public void SetAim(GameObject targetWarrior)
    {
        targetWarrior.TryGetComponent(out _tagetWarrior);
        _warrior = targetWarrior.transform;
    }

    public void SetTarget(GameObject aimWarrior)
    {
        aimWarrior.TryGetComponent(out _currentAim);
        _victim = aimWarrior.transform;
    }

    public void AttackAction()
    {
        CheckSelected();
        Moving(_tagetWarrior, _currentAim);
    }

    public void Next()
    {
        CheckSelected();
        Moving(_currentAim, _tagetWarrior);
    }

    private void CheckSelected()
    {
        if (_tagetWarrior == null)
        {
            GameObject[] findedObjects = GameObject.FindGameObjectsWithTag("Player");
            int index = Random.Range(0, findedObjects.Length);
            findedObjects[index].TryGetComponent(out _tagetWarrior);
            _warrior = findedObjects[index].transform;
        }

        if (_currentAim == null)
        {
            GameObject[] findedObjects = GameObject.FindGameObjectsWithTag("Enemy");
            int index = Random.Range(0, findedObjects.Length);
            findedObjects[index].TryGetComponent(out _currentAim);
            _victim = findedObjects[index].transform;
        }
    }

    private void Moving(IUnit warrior, IUnit victim)
    {
        _victim.DOMoveX(0.1f, 0.5f);
        _warrior.DOMoveX(-0.1f, 0.5f).OnComplete(delegate { warrior.Attack(victim); });
    }
}
