using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollGameModes : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector3 panelLocation;

    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0f, 0f);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / value;
       
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && transform.position.x > -6000)
            {
                newLocation += new Vector3(-value, 0f, 0f);
            }
            else if (percentage < 0 && transform.position.x < 8600) 
            {
                    newLocation += new Vector3(value, 0f, 0f);
            }
            StartCoroutine(SmoothMove(transform.position,newLocation,easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }

    IEnumerator SmoothMove(Vector3 startPos, Vector3 endPos, float seconds)
    {
        float t = 0f;
        while(t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

}
