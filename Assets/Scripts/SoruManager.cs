using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;


public class SoruManager : MonoBehaviour
{
    [SerializeField]
    List<SoruItem> sorularList;

    [SerializeField]
    TMP_Text soruTxt;

    [SerializeField]
    GameObject cevapPrefab;

    [SerializeField]
    Transform cevapContainer;

    
    int cevapAdet;

    int kacinciSoru;

    string[] secenekler = {"A)","B)","C)" };

    GameManager gameManager;
    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        sorularList = sorularList.OrderBy(i => Random.value).ToList();
        //SorulariYazdir();
    }


    public void SorulariYazdir()
    {
        cevapAdet = 0;


        soruTxt.text = sorularList[kacinciSoru].soru;

        soruTxt.GetComponent<CanvasGroup>().alpha = 0f;
        soruTxt.GetComponent<RectTransform>().localScale = Vector3.zero;
        CevaplariOlustur();
    }


    public void CevaplariOlustur()
    {
        GameObject[] silinecekCevaplar = GameObject.FindGameObjectsWithTag("cevapTag");

        if(silinecekCevaplar.Length>=0)
        {
            for (int i = 0; i < silinecekCevaplar.Length; i++)
            {
                DestroyImmediate(silinecekCevaplar[i]);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            GameObject cevapObje = Instantiate(cevapPrefab);
            cevapObje.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = secenekler[i];
            cevapObje.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = sorularList[kacinciSoru].cevaplar[i].ToString();
            cevapObje.transform.SetParent(cevapContainer);

            cevapObje.GetComponent<Transform>().localScale = Vector3.zero;
        }


        gameManager.dogruCevap = sorularList[kacinciSoru].dogruCevap;

        StartCoroutine(CevaplariAcRoutine());

    }

    IEnumerator CevaplariAcRoutine()
    {
        yield return new WaitForSeconds(.5f);

        soruTxt.GetComponent<CanvasGroup>().DOFade(1, .3f);
        soruTxt.GetComponent<RectTransform>().DOScale(1, .3f);

        yield return new WaitForSeconds(.4f);


        while(cevapAdet<3)
        {
            cevapContainer.GetChild(cevapAdet).DOScale(1, .2f);
            yield return new WaitForSeconds(.2f);

            cevapAdet++;
        }

        kacinciSoru++;
        gameManager.soruCevaplansinmi = true;
    }

}
