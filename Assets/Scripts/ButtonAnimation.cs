using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Animation Settings")]
    [SerializeField] private float hoverScale = 1.08f;
    [SerializeField] private float animationDuration = 0.15f;
    private Vector3 startScale;

    void Awake()
    {
        startScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(startScale * hoverScale, animationDuration)
            .SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(startScale, animationDuration)
             .SetEase(Ease.OutBack).SetUpdate(true);
    }

    
}
