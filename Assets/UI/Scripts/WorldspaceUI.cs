using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldspaceUI : MonoBehaviour
{

    private Transform _thisCam;
    private Transform _thisMenuTransform;
    private CanvasGroup _thisCG;
    [SerializeField]
    private float _worldSpaceCanvasOffset = -1f;

    private void Awake()
    {
        _thisCam = Camera.main.transform;
        _thisMenuTransform = transform;
        _thisCG = gameObject.GetComponent<CanvasGroup>();
    }

    void LateUpdate()
    {
        //_thisMenuTransform.position = new Vector3(_thisCam.localPosition.x, WorldSpaceCanvasHeight, _thisCam.localPosition.z);
        //_thisMenuTransform.localEulerAngles = new Vector3 (75, _thisCam.localRotation.eulerAngles.y, 0);
        _thisMenuTransform.SetPositionAndRotation(new Vector3(_thisCam.position.x, _thisCam.position.y + _worldSpaceCanvasOffset, _thisCam.position.z),
            Quaternion.Euler(new Vector3(75, _thisCam.localRotation.eulerAngles.y, 0)));

        //check camera forward is facing downward
        if (Vector3.Dot(Camera.main.transform.forward, Vector3.down) > 0.75f)
        {
            ShowCanvas();
        }
        else
        {
            HideCanvas();
        }
    }

    public void ShowCanvas()
    {
        _thisCG.alpha = 1;
        _thisCG.interactable = true;
        _thisCG.blocksRaycasts = true;
    }

    public void HideCanvas()
    {
        _thisCG.alpha = 0;
        _thisCG.interactable = false;
        _thisCG.blocksRaycasts = false;
    }


}
