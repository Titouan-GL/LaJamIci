using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpen : MonoBehaviour
{
    RectTransform rectTransform;
    public MainUIScript mUIs;

    public int menuIndex;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if(mUIs == null) mUIs = GetComponentInParent<MainUIScript>();
    }

    public void OpenSelf()
    {
        rectTransform.localScale = new Vector3(0.01f, 0.01f, 1);
        StartCoroutine(Open());
    }

    public void Update()
    {
        mUIs.SetCurrentMenu(menuIndex);
    }

    public void CloseSelf()
    {
        StartCoroutine(Close());
    }

    private IEnumerator Open()
    {
        StopCoroutine(Close());
        while (rectTransform.localScale.x < 1 || rectTransform.localScale.y < 1)
        {
            if (rectTransform.localScale.x < 1)
            {
                rectTransform.localScale += new Vector3(0.15f, 0f, 0f);
            }
            else if (rectTransform.localScale.y < 1)
            {
                rectTransform.localScale += new Vector3(0f, 0.15f, 0f);
            }
            yield return new WaitForSeconds(0.02f);
        }
        rectTransform.localScale = new Vector3(1, 1, 1);
    }

    private IEnumerator Close()
    {
        StopCoroutine(Open());
        while (rectTransform.localScale.x > 0.01 || rectTransform.localScale.y > 0.01)
        {
            if (rectTransform.localScale.x > 0.01)
            {
                rectTransform.localScale -= new Vector3(0.15f, 0f, 0f);
            }
            else if (rectTransform.localScale.x < 0)
            {
                rectTransform.localScale = new Vector3(0.01f, 1f, 1f);
            }
            else if (rectTransform.localScale.y > 0.01)
            {
                rectTransform.localScale -= new Vector3(0f, 0.15f, 0f);
            }
            yield return new WaitForSeconds(0.02f);
        }
        rectTransform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(false);
    }
}
