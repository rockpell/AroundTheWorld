using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    [SerializeField] private Vector2 initPosition;
    [SerializeField] private Vector2 activePositon;
    [SerializeField] private Text messageText;

    private RectTransform rectTransform;

    private Queue<string> messageQueue;
    private Coroutine nowCoroutine;

    private float deltaMove = 3;

    // Start is called before the first frame update
    void Start()
    {
        messageQueue = new Queue<string>();
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = initPosition;
    }

    public void enqueueMessage(string text)
    {
        messageQueue.Enqueue(text);
        if(nowCoroutine != null)
        {
            StopAllCoroutines();
            nowCoroutine = null;
            nowCoroutine = StartCoroutine(sequence());
        }
        else
        {
            nowCoroutine = StartCoroutine(sequence());
        }
    }

    private IEnumerator sequence()
    {
        yield return StartCoroutine(downMessage());
        while(messageQueue.Count > 0)
        {
            setMessageText(messageQueue.Dequeue());
            yield return new WaitForSeconds(1f);
        }
        
        yield return StartCoroutine(upMessage());
        nowCoroutine = null;
    }

    private IEnumerator downMessage()
    {
        while (rectTransform.anchoredPosition.y > activePositon.y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - deltaMove);
            yield return null;
        }
        rectTransform.anchoredPosition = activePositon;
    }

    private IEnumerator upMessage()
    {
        while (rectTransform.anchoredPosition.y < initPosition.y)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + deltaMove);
            yield return null;
        }
        rectTransform.anchoredPosition = initPosition;
    }

    private void setMessageText(string text)
    {
        messageText.text = text;
    }
}
