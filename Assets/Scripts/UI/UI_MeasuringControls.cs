using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_MeasuringControls : MonoBehaviour
{
    private Geometries geometryScriptRef;

    private void Awake()
    {
        geometryScriptRef = GetComponent<Geometries>();
    }

    void Update()
    {
        if (geometryScriptRef.isEnvelopeMode && geometryScriptRef.isDragging && Mouse.current.leftButton.wasPressedThisFrame)
        {
            geometryScriptRef.OnGeometryEnd();
        }
        else if (Keyboard.current.shiftKey.isPressed && Mouse.current.leftButton.wasPressedThisFrame)
        {
            geometryScriptRef.StartGeometry();
        }
    }
}
