using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchatzkistenController : MonoBehaviour
{
    public Animator _thisAnimator;
    private ParticleSystem _emitter;
    private Transform _thisCanvasTransform;
    private CanvasGroup _thisCG;
    private Camera _thisCamera;

    public void Awake () {
        _thisAnimator = GetComponentInChildren<Animator>();
        _emitter = GetComponentInChildren<ParticleSystem>();
        _thisCG = GetComponentInChildren<CanvasGroup>();
        _thisCG.alpha = 0;
        _thisCG.interactable = false;
        _thisCG.blocksRaycasts = false;
        _thisCanvasTransform = _thisCG.transform;
        _thisCamera = Camera.main;
    }

    void OnTriggerEnter()
    {
        StartCoroutine(RevealChest());
    }

    public IEnumerator RevealChest()
    {
        _thisAnimator.Play("Reveal");
        yield return new WaitForSeconds(0.5f);
        ShowButton();
    }

    void OnTriggerExit()
    {
        _thisAnimator.Play("CloseAndShrink");
        HideButton();
    }
    
    public void OpenChest()
    {
        StartCoroutine(OpenChestCoroutine());
    }

    public IEnumerator OpenChestCoroutine()
    {
        _thisAnimator.Play("Open");
        yield return new WaitForSeconds(0.5f);
        _emitter.Emit(30);
        //yield return null;
    }

    public void ShowButton()
    {
        _thisCG.alpha = 1;
        _thisCG.interactable = true;
        _thisCG.blocksRaycasts = true;
    }

    public void HideButton()
    {
        _thisCG.alpha = 0;
        _thisCG.interactable = false;
        _thisCG.blocksRaycasts = false;
    }

    void LateUpdate()
    {
        _thisCanvasTransform.LookAt(_thisCanvasTransform.position + _thisCamera.transform.rotation * Vector3.forward,
                         _thisCamera.transform.rotation * Vector3.up);

        //float tempScale = scaleMin + ((1 - (Mathf.Clamp(Vector3.Distance(_thisCamera.transform.position, transform.position), 0, 1250) / 1250)) * (scaleMax - scaleMin));
        //transform.localScale = new Vector3(tempScale, tempScale, 1);
    }
}
