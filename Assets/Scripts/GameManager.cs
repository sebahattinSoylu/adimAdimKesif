using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject soruPaneli;

    [SerializeField]
    GameObject dogruIcon, yanlisIcon;

    [SerializeField]
    GameObject oyuncuPrefab;

    [SerializeField]
    GameObject robot_1, robot_2, robot_3;

    [SerializeField]
    GameObject dogruSonuc, yanlisSonuc;
    public bool soruCevaplansinmi;

    public string dogruCevap;


    int kalanHak;

 

    SoruManager soruManager;
     

    OyuncuHareketManager oyuncuHareketManager;

    SesManager sesManager;

    int dogruAdet;

    private void Awake()
    {
        sesManager = Object.FindObjectOfType<SesManager>();
        oyuncuHareketManager = Object.FindObjectOfType<OyuncuHareketManager>();
        soruManager = Object.FindObjectOfType<SoruManager>();
    }

    private void Start()
    {
        kalanHak = 3;
        dogruAdet = 0;

        StartCoroutine(OyunuAcRouitine());
    }



    IEnumerator OyunuAcRouitine()
    {
        yield return new WaitForSeconds(.1f);
        sesManager.BaslaSesiCikar();

        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(30, 1f);

        yield return new WaitForSeconds(1.1f);
        soruManager.SorulariYazdir();

    }
   


    public void SonucuKontrolEt(string gelenCevap)
    {
        if(gelenCevap==dogruCevap)
        {
            //sonuc dogru ise yapýlacak iþlemler

            dogruAdet++;
            sesManager.DogruSesiCikar();

            if(dogruAdet>=15)
            {
                DogruSonucGoster();
            } else
            {
                soruManager.SorulariYazdir();
            }
            DogruIconuAktiflestir();
        } else
        {
            //sonuc yanlýþ ise yapýlacak iþlemler
            sesManager.YanlisSesiCikar();
            YanlisIconuAktiflestir();
            StartCoroutine(OyuncuHataYaptiGeriGeldi());
        }
    }


    void DogruIconuAktiflestir()
    {
        dogruIcon.GetComponent<CanvasGroup>().DOFade(1, .3f);

        Invoke("DogruIconuPasiflestir", .8f);
    }


    void YanlisIconuAktiflestir()
    {
        yanlisIcon.GetComponent<CanvasGroup>().DOFade(1, .3f);
        Invoke("YanlisIconuPasiflestir", .8f);
    }

    void DogruIconuPasiflestir()
    {
        
        dogruIcon.GetComponent<CanvasGroup>().DOFade(0, .3f);
    }

    void YanlisIconuPasiflestir()
    {
       
        yanlisIcon.GetComponent<CanvasGroup>().DOFade(0, .3f);
        
    }

    IEnumerator OyuncuHataYaptiGeriGeldi()
    {
        yield return new WaitForSeconds(1f);

        oyuncuHareketManager.OyuncuHataYapti();

        yield return new WaitForSeconds(1.4f);


        kalanHak--;


        HakKaybet();
        if(kalanHak>0)
        {
            oyuncuHareketManager.OyuncuGeriGelsin();

            yield return new WaitForSeconds(1f);

            soruManager.SorulariYazdir();
        } else
        {
            //oyun bitti
            YanlisSonucGoster();
        }


        

       
    }

   

    void HakKaybet()
    {
        if(kalanHak==2)
        {
            robot_3.SetActive(false);
            robot_2.SetActive(true);
            robot_1.SetActive(true);
        } else if(kalanHak==1)
        {
            robot_3.SetActive(false);
            robot_2.SetActive(false);
            robot_1.SetActive(true);
        } else if(kalanHak==0)
        {
            robot_3.SetActive(false);
            robot_2.SetActive(false);
            robot_1.SetActive(false);
        }
    }

   
   void DogruSonucGoster()
    {
        sesManager.BitisSesiCikar();
        
        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(-1100, 1f);
        dogruSonuc.GetComponent<CanvasGroup>().DOFade(1, .5f);
        dogruSonuc.GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);
    }


    void YanlisSonucGoster()
    {
        sesManager.BitisSesiCikar();
        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(-1100, 1f);
        yanlisSonuc.GetComponent<CanvasGroup>().DOFade(1, .5f);
        yanlisSonuc.GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);
    }




}
