using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] private float timeBetweenSteps;
    private IUnit _tagetWarrior;
    private IUnit _currentAim;
    private bool _attack;
    public static ActionController Instance;

    private Transform _warrior;
    private Transform _victim;
    private bool botStep;

    private void Awake()
    {
        Instance = this;
        _attack = false;
        StepController.GenerateFirstStep();
        botStep = !StepController.Step;
        FindObjectOfType<UIController>().CheckUIButtonStatus(StepController.Step);
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
        if (StepController.Step)
        {
            CheckSelected();
            botStep = true;
            MovingSelectedWarriors.Moving(_warrior, _victim);
            _warrior = null;
            _victim = null;
        }
    }

    public void Next()
    {
        if (StepController.Step)
        {
            StepController.Step = false;
            StartCoroutine(EnemyStep(0));
        }
    }
    

    private void CheckSelected()
    {
        if (_warrior == null)
        {
            GameObject[] findedObjects = GameObject.FindGameObjectsWithTag("Player");
            int index = Random.Range(0, findedObjects.Length);
            findedObjects[index].TryGetComponent(out _tagetWarrior);
            _warrior = findedObjects[index].transform;
        }

        if (_victim == null)
        {
            GameObject[] findedObjects = GameObject.FindGameObjectsWithTag("Enemy");
            int index = Random.Range(0, findedObjects.Length);
            findedObjects[index].TryGetComponent(out _currentAim);
            _victim = findedObjects[index].transform;
        }
    }

    private void Update()
    {
        if (!StepController.Step && botStep)
        {
            botStep = false;
            StartCoroutine(EnemyStep(timeBetweenSteps));
        }
        
        Debug.Log(StepController.Step);
    }

    private IEnumerator EnemyStep(float time)
    {
        _tagetWarrior = null;
        _currentAim = null;
        yield return new WaitForSeconds(time);
        CheckSelected();
        MovingSelectedWarriors.Moving(_victim, _warrior);
        _warrior = null;
        _victim = null;
    }
}
