using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [Range(0f, 360f)]
    public float rotation = 0f;

    void Awake()
    {
        Apply();
    }

    void Apply()
    {
        if (RenderSettings.skybox != null)
        {
            RenderSettings.skybox.SetFloat("_Rotation", rotation);
            DynamicGI.UpdateEnvironment();
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        Apply();
    }
#endif
}
