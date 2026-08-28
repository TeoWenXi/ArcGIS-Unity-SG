# Singapore ArcGIS Unity Project

A Unity project using the **ArcGIS Maps SDK for Unity** to create a top-down satellite view of Singapore with building layers from Esri's ArcGIS services.

The project is intended as a base for future simulation and digital twin applications.

## Features

* Top-down satellite view of Singapore
* Building layers from ArcGIS/Esri services
* Geographic positioning using the ArcGIS Maps SDK for Unity
* Spawn Unity objects using **latitude and longitude**
* Import geographic data from CSV files
* Tree spawning demonstration using Singapore tree data
* Geometry functionality imported from Esri's ArcGIS Unity samples

## Requirements

* **Unity:** `6000.4.8f1`
* **ArcGIS Maps SDK for Unity**
* **ArcGIS Online account**
* **ArcGIS Online API key**

## Setup

The project follows the same setup process as Esri's official ArcGIS Maps SDK for Unity sample project.
Please refer to the official setup guide:
[ArcGIS Maps SDK for Unity - Sample Project Setup](https://github.com/Esri/arcgis-maps-sdk-unity-samples/blob/main/sample_project/README.md)

A valid **ArcGIS Online API key** is required to access ArcGIS services.

### Creating an ArcGIS API Key

Follow Esri's official guide for creating an API key:
[Create an ArcGIS API Key](https://developers.arcgis.com/documentation/security-and-authentication/api-key-authentication/tutorials/create-an-api-key/location-platform/)

The guide covers creating API key credentials, configuring the required privileges, setting an expiration date, and generating the API key.

Once created, configure the API key in: `Tool → ArcGIS Maps SDK → Project Settings`

> **Note:** Do not commit or share your API key publicly. API keys should be kept secure and configured with only the privileges required by the project.

## Geographic Object Spawning

The project includes functionality to spawn Unity GameObjects based on their real-world **latitude and longitude**.

The basic workflow is:

```text
CSV Dataset
    ↓
Latitude / Longitude
    ↓
ArcGIS Coordinate Conversion
    ↓
Unity World Position
    ↓
Ground Height
    ↓
Spawn GameObject
```

This allows geographic datasets to be imported into the Unity environment without manually positioning each object.

### Tree Dataset Demonstration

The system is demonstrated using Singapore tree data from the **SG Trees Data** repository.

The CSV contains the geographic coordinates of trees. The project reads these coordinates and spawns tree objects at their corresponding locations on the ArcGIS map.

This demonstrates how external geographic datasets can be visualised within the Unity environment.

The corresponding spawner script is in `Assets\Scripts\TreeSpawner.cs`

## Geographic Position & Terrain Elevation

Objects can be spawned using **Unity world coordinates** without requiring pre-defined longitude/latitude. The **ArcGIS Maps SDK** converts the Unity position to geographic coordinates and provides the corresponding terrain elevation.

**Workflow:**

1. Define the object's **Unity X/Z position** (e.g. a pedestrian spawn point on a street).
2. Use `WorldToGeographic()` to convert the Unity world position to an ArcGIS `ArcGISPoint`.
3. Obtain the terrain elevation at the corresponding geographic location using the ArcGIS elevation/heightmap system.
4. Apply the resulting elevation to the object's **Unity Y position**.

This allows simulation objects such as pedestrians to be positioned using Unity coordinates while maintaining accurate placement relative to the real-world ArcGIS terrain.

## Esri Sample Components

The project uses functionality from Esri's official **ArcGIS Maps SDK for Unity Samples** repository.

The **Geometry** feature was imported from the sample project and used as part of the geographic coordinate functionality.

The original repository can be found here:

[Esri ArcGIS Maps SDK for Unity Samples](https://github.com/Esri/arcgis-maps-sdk-unity-samples/tree/main)

This repository can also be used as a reference if the ArcGIS functionality needs to be updated or extended in the future.

## Data Sources

### ArcGIS / Esri

Satellite imagery, building layers, and geographic functionality are provided through the **ArcGIS Maps SDK for Unity** and ArcGIS services.

### Singapore Tree Data

The tree spawning demonstration uses Singapore tree data from the **SG Trees Data** repository.

[SG Trees Data](https://github.com/cheeaun/sgtreesdata/tree/main)

The repository provides CSV data containing tree locations and credits **Trees.sg** and the **National Parks Board (NParks)** as the data sources.

## References

* [ArcGIS Maps SDK for Unity Samples](https://github.com/Esri/arcgis-maps-sdk-unity-samples/tree/main)
* [ArcGIS Maps SDK for Unity - Sample Project Setup](https://github.com/Esri/arcgis-maps-sdk-unity-samples/blob/main/sample_project/README.md)
* [ArcGIS API Key Creation Guide](https://developers.arcgis.com/documentation/security-and-authentication/api-key-authentication/tutorials/create-an-api-key/location-platform/)
* [SG Trees Data](https://github.com/cheeaun/sgtreesdata/tree/main)

## License

ArcGIS Maps SDK for Unity and the associated Esri sample components are subject to their respective Esri licensing terms.

Refer to the original [Esri ArcGIS Maps SDK for Unity Samples repository](https://github.com/Esri/arcgis-maps-sdk-unity-samples/tree/main) for the applicable licensing information.

## Attribution

* **Geographic data and ArcGIS functionality:** Esri
* **Tree data:** [SG Trees Data](https://github.com/cheeaun/sgtreesdata/tree/main), sourced from Trees.sg / NParks