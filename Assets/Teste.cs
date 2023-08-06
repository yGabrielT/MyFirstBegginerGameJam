using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Teste : MonoBehaviour
{
    private Vector3 _originalScale;
    private Vector3 _toScale;
    // Start is called before the first frame update
    void Start()
    {
        _originalScale = transform.localScale;
        _toScale = _originalScale/2f;
        DOTween.Sequence()
            .Append(transform.DOShakeScale(.2f, .5f, 1, 20, true, ShakeRandomnessMode.Full));
            //.Append(transform.DOScale(_originalScale, 1).SetEase(Ease.InOutSine));
        
    }
}
