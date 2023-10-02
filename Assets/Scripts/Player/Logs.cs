using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Logs : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textLog;
    [SerializeField] private TextMeshProUGUI logTitle;
    [SerializeField] private GameObject logPrefab;
    [SerializeField] private Transform logParent;
    [SerializeField] private GameObject logFullDisplay;
    [HideInInspector] public int logFound = 0;

    [HideInInspector] private List<string> logs = new List<string>()
    {
        "log 1",
        "blabliblou je suis le log 2",
    };

    [HideInInspector] private List<string> logsTitles = new List<string>()
    {
        "wouwou titre 1",
        "titre 2",
    };

    public void AddLog(int index)
    {
        GameObject go = Instantiate(logPrefab);
        RectTransform rectTransform = go.GetComponent<RectTransform>();
        go.transform.SetParent(logParent);
        rectTransform.anchoredPosition = new Vector2(0, -35 - (40 * logFound));
        go.GetComponentInChildren<TextMeshProUGUI>().text = logsTitles[index];
        Button button = go.GetComponentInChildren<Button>();
        button.onClick.AddListener(() => { SetText(index); });
        logFound++;
        SetText(index);
    }

    public void SetText(int logIndex)
    {
        textLog.text = logs[logIndex];
        logTitle.text = logsTitles[logIndex];
        displayLog(true);
    }

    public void displayLog(bool b)
    {
        logFullDisplay.SetActive(b);
    }

}
