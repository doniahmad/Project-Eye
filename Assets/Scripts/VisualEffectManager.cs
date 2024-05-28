using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VisualEffectManager : MonoBehaviour
{
    public static VisualEffectManager Instance { get; set; }

    [SerializeField] private GameObject playerCam;
    [SerializeField] private PostProcessVolume postProcessVolume;
    [SerializeField] private PostProcessProfile hypermetropiaProfile;
    [SerializeField] private PostProcessProfile monochromacyProfile;
    [SerializeField] private GameObject cataractUI;
    [SerializeField] private PostProcessProfile defaultProfile;

    [SerializeField] private BlurPostProcessing blurPostProcessing;
    [SerializeField] private ColorBlindness colorBlindness;


    public bool isColorBlind;

    private int effect = 1;

    private void Awake()
    {
        Instance = this;
        // blurPostProcessing = playerCam.GetComponent<BlurPostProcessing>();
        // colorBlindness = playerCam.GetComponent<ColorBlindness>();

        ResetEffect();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            switch (effect)
            {
                case 1:
                    if (isColorBlind)
                    {
                        ApplyProtanomaly();
                    }
                    else
                    {
                        ApplyHypermetropia();
                    }
                    effect = 2;
                    break;
                case 2:

                    ApplyMonochromacy();
                    effect = 3;
                    break;
                case 3:
                    if (isColorBlind)
                    {
                        ApplyTritanomaly();
                        effect = 4;
                    }
                    else
                    {
                        ApplyCataract();
                        effect = 0;
                    }
                    break;
                case 4:
                    ApplyDeuteranomaly();
                    effect = 0;
                    break;
                default:
                    ResetEffect();
                    effect = 1;
                    break;
            }
        }
    }

    public void ResetEffect()
    {
        blurPostProcessing.blurSize = 0;
        colorBlindness.severity = 0;
        postProcessVolume.sharedProfile = defaultProfile;
        cataractUI.SetActive(false);
    }

    public void ApplyHypermetropia()
    {
        ResetEffect();
        postProcessVolume.sharedProfile = hypermetropiaProfile;
    }

    public void ApplyMonochromacy()
    {
        ResetEffect();
        postProcessVolume.sharedProfile = monochromacyProfile;
    }

    public void ApplyCataract()
    {
        ResetEffect();
        blurPostProcessing.blurSize = 0.015f;
        cataractUI.SetActive(true);
    }

    public void ApplyProtanomaly()
    {
        ResetEffect();
        colorBlindness.blindType = ColorBlindness.BlindTypes.Protanomaly;
        colorBlindness.severity = 1;
    }

    public void ApplyTritanomaly()
    {
        ResetEffect();
        colorBlindness.blindType = ColorBlindness.BlindTypes.Tritanomaly;
        colorBlindness.severity = 1;
    }

    public void ApplyDeuteranomaly()
    {
        ResetEffect();
        colorBlindness.blindType = ColorBlindness.BlindTypes.Deuteranomaly;
        colorBlindness.severity = 1;
    }

}
