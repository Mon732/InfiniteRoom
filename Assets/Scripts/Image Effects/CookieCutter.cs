using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CookieCutter : ImageEffectBase
{
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (first)
        {
            buffer = RenderTexture.GetTemporary(src.width, src.height, 24);
        }

        Graphics.SetRenderTarget(buffer.colorBuffer, src.depthBuffer);
        Graphics.Blit(src, buffer, material);
        Graphics.Blit(buffer, dest);

        //Graphics.Blit(src, dest, material);
    }

    void OnPostRender()
    {
        if (last)
        {
            RenderTexture.ReleaseTemporary(buffer);
        }
    }
}
