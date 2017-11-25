using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapPOIController : MonoBehaviour {

    public string Name;
    public string InfoText;
    public int Rating = -1;
    public bool Interested;
    public bool Visited = false;

    public GameObject WorldspacePOIMarker;
    private Image _boothIcon;
    private TextMeshProUGUI _textName;

    public void Awake()
    {
        _boothIcon = GetComponent<Image>();
        _textName = GetComponentInChildren<TextMeshProUGUI>();
        _textName.text = Name;
        _textName.color = new Color(1, 1, 1, 1);
        WorldspacePOIMarker.GetComponentInChildren<TextMeshProUGUI>().text = Name;
    }

    public void MarkAsNeutral()
    {
        _boothIcon.color = new Color32(0, 211, 216, 255);
    }

    public void MarkAsInterested()
    {
        _boothIcon.color = new Color32(252, 82, 85, 255);
    }

    public void MarkAsVisited()
    {
        _boothIcon.color = new Color32(255, 255, 2, 255);
    }

    public void StandAccepted()
    {
        _boothIcon.color = new Color32(31, 218, 121, 255);
    }

    // Update is called once per frame
    public void UpdateMarker () {
        if(!Interested)
        {
            MarkAsNeutral();
        }
        else
        {
            if (Visited && Rating == -1)
            {
                MarkAsVisited();
            }
            else if(Visited && Rating != -1)
            {
                StandAccepted();
            }
            else
            {
                MarkAsInterested();
            }
        }
	}
}
