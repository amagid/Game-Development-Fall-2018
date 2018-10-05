using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBarUI : MonoBehaviour {

    [SerializeField] private Image powerBar;
    [SerializeField]private Image sanityBar;
    private float maxSanity = 100f;
    private float maxPower = 0f;
    private float sanity;
    [SerializeField] private PlayerCharacter playerChar;
    private float power;

    // Use this for initialization
    void Start()
    {
        playerChar = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (maxPower == 0f)
        {
            maxPower = playerChar.getPowerSource().getMaxPower();
        }
        else
        {
            power = playerChar.getPower();
            sanity = playerChar.getSanity();
            sanityBar.fillAmount = sanity / maxSanity;
            powerBar.fillAmount = power / maxPower;
        }
    }

}
