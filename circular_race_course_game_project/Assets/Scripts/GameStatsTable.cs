using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections.Generic;

public class GameStatsTable : MonoBehaviour
{
    [SerializeField] private Transform entryContainer; // Assign in Inspector
    [SerializeField] private Transform entryTemplate; // Assign in Inspector

    private void Awake()
    {
        if (entryContainer == null)
        {
            Debug.LogError("EntryContainer is not assigned in the Inspector.");
            return;
        }

        if (entryTemplate == null)
        {
            Debug.LogError("EntryTemplate is not assigned in the Inspector.");
            return;
        }

        entryTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        // Clear existing entries without destroying template
        foreach (Transform child in entryContainer)
        {
            if (child != entryTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        List<EntryData> entryDataList = new List<EntryData>
        {
            new EntryData { Rank = 1, Score = Random.Range(0, 1000) },
            new EntryData { Rank = 2, Score = Random.Range(0, 1000) },
            new EntryData { Rank = 3, Score = Random.Range(0, 1000) },
            new EntryData { Rank = 4, Score = Random.Range(0, 1000) },
            new EntryData { Rank = 5, Score = Random.Range(0, 1000) },
            new EntryData { Rank = 6, Score = Random.Range(0, 1000) }
        };

        foreach (EntryData entryData in entryDataList)
        {
            AddEntry(entryData, entryContainer, new List<Transform>());
        }
    }

    public void AddEntry(EntryData entryData, Transform container, List<Transform> transformList)
    {
        float templateHeight = 50f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRecTransform = entryTransform.GetComponent<RectTransform>();
        entryRecTransform.anchoredPosition = new Vector2(0, -templateHeight * (entryData.Rank - 1));
        entryTransform.gameObject.SetActive(true);

        string rankString;
        switch (entryData.Rank)
        {
            default:
                rankString = entryData.Rank + "TH";
                break;
            case 1: rankString = "1ST";
                break;
            case 2: rankString = "2ND";
                break;
            case 3: rankString = "3RD";
                break;
        }

        // Retrieve all children including nested ones
        List<Transform> entryChildren = GetChildren(entryTransform, true);
        transformList.AddRange(entryChildren);

        // Debug the children list
        Debug.Log("Instantiated entry has the following children:");
        foreach (Transform child in entryChildren)
        {
            Debug.Log(child.name);
        }

        Transform nameTextTransform = FindComponent(entryChildren, "NameText");
        if (nameTextTransform != null)
        {
            TextMeshProUGUI nameText = nameTextTransform.GetComponent<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = rankString;
                Debug.Log("NameText assigned: " + rankString);
            }
            else
            {
                Debug.LogError("NameText component is missing TextMeshProUGUI component.");
            }
        }
        else
        {
            Debug.LogError("NameText component not found in the template instance.");
        }

        Transform uProducedTextTransform = FindComponent(entryChildren, "UProducedText");
        if (uProducedTextTransform != null)
        {
            TextMeshProUGUI uProducedText = uProducedTextTransform.GetComponent<TextMeshProUGUI>();
            if (uProducedText != null)
            {
                uProducedText.text = entryData.Score.ToString();
                Debug.Log("UProducedText assigned: " + entryData.Score.ToString());
            }
            else
            {
                Debug.LogError("UProducedText component is missing TextMeshProUGUI component.");
            }
        }
        else
        {
            Debug.LogError("UProducedText component not found in the template instance.");
        }

        Transform tConqueredTextTransform = FindComponent(entryChildren, "TConqueredText");
        if (tConqueredTextTransform != null)
        {
            TextMeshProUGUI tConqueredText = tConqueredTextTransform.GetComponent<TextMeshProUGUI>();
            if (tConqueredText != null)
            {
                tConqueredText.text = entryData.Score.ToString();
                Debug.Log("TConqueredText assigned: " + entryData.Score.ToString());
            }
            else
            {
                Debug.LogError("TConqueredText component is missing TextMeshProUGUI component.");
            }
        }
        else
        {
            Debug.LogError("TConqueredText component not found in the template instance.");
        }

        Transform gEarnedTextTransform = FindComponent(entryChildren, "GEarnedText");
        if (gEarnedTextTransform != null)
        {
            TextMeshProUGUI gEarnedText = gEarnedTextTransform.GetComponent<TextMeshProUGUI>();
            if (gEarnedText != null)
            {
                gEarnedText.text = entryData.Score.ToString();
                Debug.Log("GEarnedText assigned: " + entryData.Score.ToString());
            }
            else
            {
                Debug.LogError("GEarnedText component is missing TextMeshProUGUI component.");
            }
        }
        else
        {
            Debug.LogError("GEarnedText component not found in the template instance.");
        }
    }

    private List<Transform> GetChildren(Transform parent, bool recursive)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parent)
        {
            children.Add(child);
            if (recursive)
            {
                children.AddRange(GetChildren(child, true));
            }
        }

        return children;
    }

    private Transform FindComponent(List<Transform> children, string name)
    {
        return children.Find(child => child.name == name);
    }
}

[System.Serializable]
public class EntryData
{
    public int Rank;
    public int Score;
}
