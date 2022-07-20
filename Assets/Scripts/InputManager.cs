using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameObject Oyuncu;

    GameManager gameManager;

    SesManager sesManager;

    public string ad;
    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
        sesManager = Object.FindObjectOfType<SesManager>();


        Oyuncu = GameObject.Find("Oyuncu");
    }

    void OnMouseDown()
    {
        if (!gameManager.soruCevaplansinmi)
            return;

        if(this.transform.position.z>Oyuncu.transform.position.z && this.transform.position.z<Oyuncu.transform.position.z+2)
        {
            Vector3 mousePos = this.transform.position;
            Oyuncu.GetComponent<OyuncuHareketManager>().HareketEt(mousePos, 0.5f);
            gameManager.SonucuKontrolEt(ad);
            gameManager.soruCevaplansinmi = false;
            sesManager.ButonSesiCikar();
        }




    }
}
