using Esri.ArcGISMapsSDK.Components;
using Esri.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LayerToggle : MonoBehaviour
{
    public GameObject ArcGIS_Map_Obj;
    public GameObject TreesParentObj;

    public void ToggleLayer(string layerName)
    {
        var mapComponent = ArcGIS_Map_Obj.GetComponent<ArcGISMapComponent>();
        var mapLayers = mapComponent.View.Map.Layers;

        switch(layerName)
        {
            case "Buildings":
                mapLayers.At(0).IsVisible = !(mapLayers.At(0).IsVisible);
                break;
            case "Trees":
                TreesParentObj.SetActive(!TreesParentObj.activeSelf);
                break;
            default:
                Debug.LogWarning($"Layer '{layerName}' not found.");
                break;
        }
    }

    public void UpdateButtonOpacity()
    {
        GameObject pressedButton = EventSystem.current.currentSelectedGameObject;
        Image buttonImage = pressedButton.GetComponent<Image>();
        if (buttonImage.color.a == 1f)
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0.3f);
        else
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 1f);
    }
}