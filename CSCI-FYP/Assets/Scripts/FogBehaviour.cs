using UnityEngine;
using System.Collections;

public class FogBehaviour : MonoBehaviour
{

    public bool fog;
    public bool lava;
    public Color fogColor;
    public Color lavaColor;
    public float fogDensity;
    public Color ambientLight;
    public float haloStrength;
    public float flareStrength;

    bool previousFog;
    Color previousFogColor;
    float previousFogDensity;
    Color previousAmbientLight;
    float previousHaloStrength;
    float previousFlareStrength;

    void OnPreRender()
    {
        previousFog = RenderSettings.fog;
        previousFogColor = RenderSettings.fogColor;
        previousFogDensity = RenderSettings.fogDensity;
        previousAmbientLight = RenderSettings.ambientLight;
        previousHaloStrength = RenderSettings.haloStrength;
        previousFlareStrength = RenderSettings.flareStrength;
        if (fog)
        {
            RenderSettings.fog = fog;
            if (lava)
                RenderSettings.fogColor = lavaColor;
            else
                RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
            RenderSettings.ambientLight = ambientLight;
            RenderSettings.haloStrength = haloStrength;
            RenderSettings.flareStrength = flareStrength;
        }
    }

    void OnPostRender()
    {
        RenderSettings.fog = previousFog;
        RenderSettings.fogColor = previousFogColor;
        RenderSettings.fogDensity = previousFogDensity;
        RenderSettings.ambientLight = previousAmbientLight;
        RenderSettings.haloStrength = previousHaloStrength;
        RenderSettings.flareStrength = previousFlareStrength;
    }
}