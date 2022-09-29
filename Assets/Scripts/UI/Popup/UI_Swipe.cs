using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Swipe : MonoBehaviour
{
    [SerializeField]
    private Scrollbar       scrollBar;                      // Scrollbar�� ��ġ�� �������� ���� ������ �˻�
    [SerializeField]
    private Transform[]     circleContents;
    [SerializeField]
    private float           swipeTime;                      // �������� Swipe �Ǵ� �ð�
    [SerializeField]
    private float           swipeDistance = 50.0f;          // �������� Swipe�Ǳ� ���� �������� �ϴ� �ּ� �Ÿ�

    private float[]         scrollPageValues;               // �� �������� ��ġ �� [0.0 ~ 1.0]
    private float           valueDistance = 0;              // �� ������ ������ �Ÿ�
    private int             currentPage = 0;
    private int             maxPage;
    private float           startTouchX;
    private float           endTouchX;
    private bool            isSwipeMode = false;
    private float           circleContentScale = 1.4f;

    void Awake()
    {
        // ��ũ�� �Ǵ� �������� �� value ���� �����ϴ� �迭 �޸� �Ҵ�
        scrollPageValues = new float[transform.childCount];

        // ��ũ�� �Ǵ� ������ ������ �Ÿ�
        valueDistance = 1f / (scrollPageValues.Length -1f);

        // ��ũ�ѵǴ� �������� �� value ��ġ ����
        for (int i = 0; i < scrollPageValues.Length; i++)
        {
            scrollPageValues[i] = valueDistance * i;
        }
        
        // �ִ� ������ ��
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
        // ���� Swipe�� �������̸� ��ġ �Ұ�
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
        // �ʹ� ���� �Ÿ��� �������� ���� Swipe X
        if (Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
        {
            StartCoroutine(OnSwipeOneStep(currentPage));
            return;
        }

        // Swipe ����
        bool isLeft = startTouchX < endTouchX ? true : false;

        // �̵� ������ ������ ��
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

    // �������� ������ �ѱ�� Swipe ȿ�� ���
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
