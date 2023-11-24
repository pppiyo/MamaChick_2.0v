using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxpage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    [SerializeField] Button previousBtn, nextBtn;

    float dragthreshold;

    public int Maxpage { get => maxpage; set => maxpage = value; }
    public int CurrentPage { get => currentPage; set => currentPage = value; }
    public Vector3 TargetPos { get => targetPos; set => targetPos = value; }
    public Vector3 PageStep { get => pageStep; set => pageStep = value; }
    public RectTransform LevelPagesRect { get => levelPagesRect; set => levelPagesRect = value; }
    public float TweenTime { get => tweenTime; set => tweenTime = value; }
    public LeanTweenType TweenType { get => tweenType; set => tweenType = value; }

    public void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragthreshold = Screen.width / 15;
        UpdateArrowButton();
    }
    public void Next()
    {
        if(currentPage < maxpage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();

                
        }
    }
    public void Previous()
    {
        if(currentPage>1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }
    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateArrowButton();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragthreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x) Previous();
            else Next();
        }
        else
        {
            MovePage();
        }
    }
    void UpdateArrowButton()
    {
        nextBtn.interactable = true;
        previousBtn.interactable = true;
        if(currentPage == 1) previousBtn.interactable = false;
        else if (currentPage == Maxpage) nextBtn.interactable = false;
    }

}