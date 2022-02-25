using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public Image hpimage;
    public Image hpeffect;

    [HideInInspector] public float hp;
    [SerializeField] private float maxhp;
    [SerializeField] private float hurtspeed = 0.005f;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        hpimage.fillAmount = hp / maxhp;
        if (hpeffect.fillAmount > hpimage.fillAmount)
        {
            hpeffect.fillAmount -= hurtspeed;
        }
        else
        {
            hpeffect.fillAmount = hpimage.fillAmount;
        }
    }
}
