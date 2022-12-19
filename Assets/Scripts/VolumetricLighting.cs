using UnityEngine;

public class VolumetricLighting : MonoBehaviour
{
    public Shader volumetricLightingShader; // Shader used to render the volumetric lighting effect
    public Color lightColor = Color.white; // Color of the volumetric lighting
    public float lightIntensity = 1.0f; // Intensity of the volumetric lighting
    public float lightDensity = 1.0f; // Density of the volumetric lighting

    private Material volumetricLightingMaterial; // Material used to render the volumetric lighting effect
    private Camera mainCamera; // The main camera in the scene

    private void Start()
    {
        // Get the main camera in the scene
        mainCamera = Camera.main;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // Create the material if it does not exist
        if (volumetricLightingMaterial == null)
        {
            volumetricLightingMaterial = new Material(volumetricLightingShader);
        }

        // Set the material properties
        volumetricLightingMaterial.SetColor("_LightColor", lightColor);
        volumetricLightingMaterial.SetFloat("_LightIntensity", lightIntensity);
        volumetricLightingMaterial.SetFloat("_LightDensity", lightDensity);

        // Set the view and projection matrices for the lighting shader
        volumetricLightingMaterial.SetMatrix("_ViewMatrix", mainCamera.worldToCameraMatrix);
        volumetricLightingMaterial.SetMatrix("_ProjectionMatrix", mainCamera.projectionMatrix);

        // Render the volumetric lighting effect
        Graphics.Blit(src, dest, volumetricLightingMaterial);
    }
}
