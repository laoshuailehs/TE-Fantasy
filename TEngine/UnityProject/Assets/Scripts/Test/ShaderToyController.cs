using System;
using TEngine;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderToyController : MonoBehaviour
{
    public Shader shaderToy;        // 主Shader
    public Shader defaultShader;    // 默认Shader
    private Material shaderToyMaterial = null;
    private bool hasLoggedUnsupportedShader = false;
    public Material Material
    {
        get
        {
            shaderToyMaterial = GetMat(shaderToy, shaderToyMaterial);
            return shaderToyMaterial;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, Material);
    }

    Material GetMat(Shader shader, Material material)
    {
        if (shader == null)
        {
            Log.Error("提供的Shader为null！");
            return null;
        }

        // 如果材质已存在且使用的是同一个Shader，直接返回
        if (material != null && material.shader == shader)
        {
            return material;
        }

        if (!shader.isSupported)
        {
            if (!hasLoggedUnsupportedShader)
            {
                Log.Info("主Shader不支持，尝试使用默认Shader...");
                hasLoggedUnsupportedShader = true;
            }

            if (defaultShader != null && defaultShader.isSupported)
            {
                shader = defaultShader;
            }
            else
            {
                Shader fallbackShader = FindFallbackShader();
                if (fallbackShader != null && fallbackShader.isSupported)
                {
                    return CreateMaterial(fallbackShader);
                }
                else
                {
                    Log.Error("无法找到任何可用的Shader！");
                    return null;
                }
            }
        }

        // 如果材质已存在但Shader不同，则销毁旧材质
        if (material != null && material.shader != shader)
        {
            DestroyImmediate(material);
            material = null;
        }

        return CreateMaterial(shader);
    }


    Material CreateMaterial(Shader shader)
    {
        Material material = new Material(shader)
        {
            hideFlags = HideFlags.DontSave
        };
        return material;
    }

    Shader FindFallbackShader()
    {
        string[] candidates = new string[]
        {
            "Unlit/Color",
            "Standard",
            "Legacy Shaders/Diffuse",
            "Hidden/InternalErrorShader"
        };

        foreach (var name in candidates)
        {
            Shader candidate = Shader.Find(name);
            if (candidate != null && candidate.isSupported)
            {
                Log.Info($"使用回退Shader: {name}");
                return candidate;
            }
        }

        return null;
    }
    void OnDestroy()
    {
        if (shaderToyMaterial != null)
        {
            DestroyImmediate(shaderToyMaterial);
        }
    }

}
