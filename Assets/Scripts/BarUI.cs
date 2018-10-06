using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour {

    [SerializeField] private Image powerBar;
    [SerializeField] private Image sanityBar;
    [SerializeField] private Color lowColor;
    [SerializeField] private Color fullColor;
    private Color lerpedColor;
    private float lerpSpeed;
    private float maxSanity = 100f;
    private float maxPower = 0f;
    private float sanity;
    [SerializeField] private PlayerCharacter playerChar;
    private float power;

    // Use this for initialization
    void Start()
    {
        playerChar = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
        lowColor = Color.red;
        fullColor = Color.green;
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
            sanityBar.color = Color.Lerp(Color.red, Color.green, sanityBar.fillAmount);
            powerBar.color = Color.Lerp(Color.red, Color.green, powerBar.fillAmount);
        }
    }

}
