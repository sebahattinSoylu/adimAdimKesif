using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesManager : MonoBehaviour
{
    [SerializeField]
    AudioSource buton_FX, dogru_FX, finish_FX, basla_FX, yanlis_FX;


    public void ButonSesiCikar()
    {
        buton_FX.Play();
    }

    public void DogruSesiCikar()
    {
        dogru_FX.Play();
    }

    public void YanlisSesiCikar()
    {
        yanlis_FX.Play();
    }


    public void BaslaSesiCikar()
    {
        basla_FX.Play();
    }
    public void BitisSesiCikar()
    {
        finish_FX.Play();
    }

}
