using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Hint : MonoBehaviour
{
    private Image img_Bg;
    private Text txt_Hint;

    private void Awake()
    {
        img_Bg = GetComponent<Image>();
        txt_Hint = GetComponentInChildren<Text>();
    }
    public void Show()
    {
        StopCoroutine("Dealy");

        img_Bg.color = new Color(img_Bg.color.r, img_Bg.color.g, img_Bg.color.b, 0);
        txt_Hint.color = new Color(txt_Hint.color.r, txt_Hint.color.g, txt_Hint.color.b, 0);
        transform.position = new Vector3(300, -30, 0);
        transform.DOLocalMoveY(0, 0.3f).OnComplete(() =>
        {
            StartCoroutine("Dealy");
        });
        img_Bg.DOColor(new Color(img_Bg.color.r, img_Bg.color.g, img_Bg.color.b, 0.4f), 0.3f);
        txt_Hint.DOColor(new Color(txt_Hint.color.r, txt_Hint.color.g, txt_Hint.color.b, 1), 0.3f);
    }
    private IEnumerator Dealy()
    {
        yield return new WaitForSeconds(0.2f);
        transform.DOLocalMoveY(70, 0.2f);
        img_Bg.DOColor(new Color(img_Bg.color.r, img_Bg.color.g, img_Bg.color.b, 0), 0.2f);
        txt_Hint.DOColor(new Color(txt_Hint.color.r, txt_Hint.color.g, txt_Hint.color.b, 0), 0.2f);
    }
}
