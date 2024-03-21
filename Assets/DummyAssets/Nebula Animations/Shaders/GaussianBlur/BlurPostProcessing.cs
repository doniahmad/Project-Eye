using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class BlurPostProcessing : MonoBehaviour
{
    [SerializeField] private Material postProcessMaterial;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        var temporaryTexture = RenderTexture.GetTemporary(src.width, src.height);
        Graphics.Blit(src, temporaryTexture, postProcessMaterial, 0);
        Graphics.Blit(temporaryTexture, dest, postProcessMaterial, 1);
        RenderTexture.ReleaseTemporary(temporaryTexture);
    }
}
