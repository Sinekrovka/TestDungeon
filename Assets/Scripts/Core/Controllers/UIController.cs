using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform playerPanel;
    [SerializeField] private RectTransform textMessagePlayerStep;
    [SerializeField] private RectTransform textMessageEnemyStep;
    
    private Vector3 _startPositionPanel;
    private float _startPositionPlayerMessage;
    private float _startPositionEnemyStep;
    private void Awake()
    {
        MovingSelectedWarriors.endBattle += CheckUIButtonStatus;
        _startPositionPanel = playerPanel.anchoredPosition;
    }

    public void CheckUIButtonStatus(bool currentStep)
    {
        if (currentStep)
        {
            ShowUI();
        }
        else
        {
            HideUI();
        }
    }

    private void ShowUI()
    {
        playerPanel.DOAnchorPos(_startPositionPanel, 0.5f).SetEase(Ease.Linear);
        textMessageEnemyStep.DOAnchorPosY(_startPositionEnemyStep + 200, 0.5f);
        textMessagePlayerStep.DOAnchorPosY(_startPositionPlayerMessage, 0.5f);

    }

    private void HideUI()
    {
        playerPanel.DOAnchorPos(_startPositionPanel - new Vector3(0,200), 0.5f).SetEase(Ease.Linear);
        textMessageEnemyStep.DOAnchorPosY(_startPositionEnemyStep, 0.5f);
        textMessagePlayerStep.DOAnchorPosY(_startPositionPlayerMessage + 200, 0.5f);
    }
    
}
