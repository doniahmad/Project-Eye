using UnityEngine;

public class BlurPostProcessing : MonoBehaviour
{
    public Material blurMaterial;

    public enum BlurType
    {
        BoxLow = 0, BoxMedium, BoxHigh, GaussLow, GaussHigh
    }
    public BlurType blurType;
    [Range(0, 0.1f)] public float blurSize = 0.0f;
    [Range(0, 0.1f)] public float standartDeviation = 0.0f;
    public bool useGaussianBlur = false;
    void Start()
    {
        // blurMaterial ??= new Material(blurShader);
        // blurMaterial.hideFlags = HideFlags.HideAndDontSave;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        var temporaryTexture = RenderTexture.GetTemporary(src.width, src.height);
        blurMaterial.SetFloat("_BlurSize", blurSize);
        blurMaterial.SetFloat("_StandardDeviation", standartDeviation);
        blurMaterial.SetInt("_Samples", (int)blurType);
        blurMaterial.SetFloat("_Gauss", useGaussianBlur ? 1 : 0);

        Graphics.Blit(src, temporaryTexture, blurMaterial, 0);
        Graphics.Blit(temporaryTexture, dest, blurMaterial, 1);
        RenderTexture.ReleaseTemporary(temporaryTexture);
    }
}
