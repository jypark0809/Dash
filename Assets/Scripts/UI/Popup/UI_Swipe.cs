using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Swipe : MonoBehaviour
{
    [SerializeField]
    private Scrollbar       scrollBar;                      // Scrollbar의 위치를 바탕으로 현재 페이지 검사
    [SerializeField]
    private Transform[]     circleContents;
    [SerializeField]
    private float           swipeTime;                      // 페이지가 Swipe 되는 시간
    [SerializeField]
    private float           swipeDistance = 50.0f;          // 페이지가 Swipe되기 위해 움직여야 하는 최소 거리

    private float[]         scrollPageValues;               // 각 페이지의 위치 값 [0.0 ~ 1.0]
    private float           valueDistance = 0;              // 각 페이지 사이의 거리
    private int             currentPage = 0;
    private int             maxPage;
    private float           startTouchX;
    private float           endTouchX;
    private bool            isSwipeMode = false;
    private float           circleContentScale = 1.4f;

    void Awake()
    {
        // 스크롤 되는 페이지의 각 value 값을 저장하는 배열 메모리 할당
        scrollPageValues = new float[transform.childCount];

        // 스크롤 되는 페이지 사이의 거리
        valueDistance = 1f / (scrollPageValues.Length -1f);

        // 스크롤되는 페이지의 각 value 위치 설정
        for (int i = 0; i < scrollPageValues.Length; i++)
        {
            scrollPageValues[i] = valueDistance * i;
        }
        
        // 최대 페이지 수
        maxPage = transform.childCount;
    }

    void Start()
    {
        SetScrollBarValue(0);
    }

    void SetScrollBarValue(int index)
    {
        currentPage = index;
        scrollBar.value = scrollPageValues[index];
    }

    void Update()
    {
        UpdateInput();
        UpdateCircleContent();
    }

    void UpdateInput()
    {
        // 현재 Swipe를 진행중이면 터치 불가
        if (isSwipeMode == true)
            return;

        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTouchX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchX = Input.mousePosition.x;

            UpdateSwipe();
        }
        #endif

        #if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchX = touch.position.x;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchX = touch.position.x;

                UpdateSwipe();
            }
        }
        #endif
    }

    void UpdateSwipe()
    {
        // 너무 작은 거리를 움직였을 때는 Swipe X
        if (Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
        {
            StartCoroutine(OnSwipeOneStep(currentPage));
            return;
        }

        // Swipe 방향
        bool isLeft = startTouchX < endTouchX ? true : false;

        // 이동 방향이 왼쪽일 때
        if (isLeft)
        {
            if (currentPage == 0)
                return;
            currentPage--;
        }
        else
        {
            if (currentPage == maxPage - 1)
                return;
            currentPage++;
        }

        StartCoroutine(OnSwipeOneStep(currentPage));
    }

    // 페이지를 옆으로 넘기는 Swipe 효과 재생
    private IEnumerator OnSwipeOneStep(int index)
    {
        float start = scrollBar.value;
        float current = 0;
        float percent = 0;

        isSwipeMode = true;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / swipeTime;

            scrollBar.value = Mathf.Lerp(start, scrollPageValues[index], percent);
            yield return null;
        }

        isSwipeMode = false;
    }

    private void UpdateCircleContent()
    {
        for (int i = 0; i <scrollPageValues.Length; i++)
        {
            circleContents[i].localScale = Vector2.one;
            circleContents[i].GetComponent<Image>().color = Color.white;

            if (scrollBar.value < scrollPageValues[i] + (valueDistance / 2) &&
                scrollBar.value > scrollPageValues[i] - (valueDistance / 2))
            {
                circleContents[i].localScale = Vector2.one * circleContentScale;
                circleContents[i].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
            }
        }
    }
}
