using UnityEngine;

public class ActionController : MonoBehaviour
{
    private IUnit _tagetWarrior;
    private IUnit _currentAim;
    private bool _attack;
    public static ActionController Instance;

    private void Awake()
    {
        Instance = this;
        _attack = false;
    }

    public void SetAim(GameObject targetWarrior)
    {
        targetWarrior.TryGetComponent(out _tagetWarrior);
    }

    public void SetTarget(GameObject aimWarrior)
    {
        aimWarrior.TryGetComponent(out _currentAim);
    }

    public void AttackAction()
    {
        CheckSelected();
        _tagetWarrior.Attack(_currentAim);
    }

    public void Next()
    {
        CheckSelected();
        _currentAim.Attack(_tagetWarrior);
    }

    private void CheckSelected()
    {
        if (_tagetWarrior == null)
        {
            GameObject[] findedObjects = GameObject.FindGameObjectsWithTag("Player");
            int index = Random.Range(0, findedObjects.Length);
            findedObjects[index].TryGetComponent(out _tagetWarrior);
        }

        if (_currentAim == null)
        {
            GameObject[] findedObjects = GameObject.FindGameObjectsWithTag("Enemy");
            int index = Random.Range(0, findedObjects.Length);
            findedObjects[index].TryGetComponent(out _currentAim);
        }
    }
}
