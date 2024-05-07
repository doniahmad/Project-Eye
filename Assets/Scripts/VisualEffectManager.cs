using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VisualEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject playerCam;
    [SerializeField] private PostProcessVolume postProcessVolume;
    [SerializeField] private PostProcessProfile hypermetropiaProfile;
    [SerializeField] private PostProcessProfile monochromacyProfile;
    [SerializeField] private GameObject cataractUI;
    [SerializeField] private PostProcessProfile defaultProfile;

    BlurPostProcessing blurPostProcessing;
    ColorBlindness colorBlindness;

    private int effect = 1;

    private void Awake()
    {
        blurPostProcessing = playerCam.GetComponent<BlurPostProcessing>();
        colorBlindness = playerCam.GetComponent<ColorBlindness>();

        ResetEffect();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            switch (effect)
            {
                case 1:
                    ApplyHypermetropia();
                    effect = 2;
                    break;
                case 2:
                    ApplyMonochromacy();
                    effect = 3;
                    break;
                case 3:
                    ApplyCataract();
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
        blurPostProcessing.enabled = false;
        colorBlindness.enabled = false;
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
        blurPostProcessing.enabled = true;
        cataractUI.SetActive(true);
    }

}
