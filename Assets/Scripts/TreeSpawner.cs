using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Geometry;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

[System.Serializable]
public class TreeObject
{
    public string name;
    public GameObject treePrefabObj;
}

public class TreeSpawner : MonoBehaviour
{
    public List<TreeObject> treePrefabs;
    private string[] treesData;

    private int latCol = -1;
    private int lonCol = -1;
    private int girthSizeCol = -1;
    private int heightEstCol = -1;

    // Reference height (meters) that your prefab model represents at scale 1.0
    // Measure your tree prefab in the Scene view (bounding box Y size) and set this to match.
    public float prefabBaseHeight = 5f;

    void Start()
    {
        string filePath = Path.Combine(Application.dataPath, "Datasets", "trees.csv");
        if (File.Exists(filePath))
        {
            treesData = File.ReadAllLines(filePath);
            SpawnTrees();
        }
        else
            Debug.LogError($"CSV file not found at path: {filePath}");
    }

    void SpawnTrees()
    {
        //If no data, return
        if (treesData.Length == 0) 
            return;

        //Get csv data
        string[] headers = SplitCsvLine(treesData[0]);
        latCol = System.Array.FindIndex(headers, h => h.Trim().ToLower() is "lat" or "latitude");
        lonCol = System.Array.FindIndex(headers, h => h.Trim().ToLower() is "lng" or "lon" or "longitude");
        girthSizeCol = System.Array.FindIndex(headers, h => h.Trim().ToLower() == "girth_size");
        heightEstCol = System.Array.FindIndex(headers, h => h.Trim().ToLower() == "height_est");

        if (latCol == -1 || lonCol == -1)
        {
            Debug.LogError($"Could not find lat/lon columns. Headers: {string.Join(", ", headers)}");
            return;
        }

        int spawned = 0;
        int startingIndex = 0;
        //for (int i = 1; i < treesData.Length; i++)
        for (int i = 0; i < 2500; i++)
        {
            if (string.IsNullOrWhiteSpace(treesData[startingIndex + i])) 
                continue;

            string[] cols = SplitCsvLine(treesData[startingIndex + i]);
            if (cols.Length <= System.Math.Max(latCol, lonCol)) 
                continue;

            //Check if lat/lon are valid numbers
            if (!double.TryParse(cols[latCol], NumberStyles.Float, CultureInfo.InvariantCulture, out double lat)) 
                continue;
            if (!double.TryParse(cols[lonCol], NumberStyles.Float, CultureInfo.InvariantCulture, out double lon)) 
                continue;
            if (lat == 0 || lon == 0) 
                continue;

            string girthSize = girthSizeCol != -1 && cols.Length > girthSizeCol ? cols[girthSizeCol].Trim() : "M";
            float heightEst = 0f;
            if (heightEstCol != -1 && cols.Length > heightEstCol)
                float.TryParse(cols[heightEstCol], NumberStyles.Float, CultureInfo.InvariantCulture, out heightEst);

            SpawnSingleTree(lat, lon, girthSize, heightEst);
            spawned++;
        }

        Debug.Log($"Spawned {spawned} trees out of {treesData.Length - 1} rows.");
    }

    void SpawnSingleTree(double lat, double lon, string girthSize, float heightEst)
    {
        //Spawn object
        GameObject treePrefab = treePrefabs[Random.Range(0, treePrefabs.Count)].treePrefabObj;
        GameObject newTreeObj = Instantiate(treePrefab, transform);

        //Add ArcGISLocationComponent
        ArcGISLocationComponent location = newTreeObj.GetComponent<ArcGISLocationComponent>();
        if (location == null)
            location = newTreeObj.AddComponent<ArcGISLocationComponent>();

        //Set position
        location.Position = new ArcGISPoint(lon, lat, 0, ArcGISSpatialReference.WGS84());
        location.SurfacePlacementMode = ArcGISSurfacePlacementMode.OnTheGround;

        //Update scale based on girth and height
        Transform model = newTreeObj.transform.GetChild(0);
        float girthScale = girthSize == "L" ? 1.4f : girthSize == "S" ? 0.7f : 1.0f;
        float heightScale = heightEst > 0 ? heightEst / 10f : 1.0f;
        model.localScale = new Vector3(girthScale, heightScale, girthScale);
    }

    string[] SplitCsvLine(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        var current = new System.Text.StringBuilder();
        foreach (char c in line)
        {
            if (c == '"') inQuotes = !inQuotes;
            else if (c == ',' && !inQuotes) { result.Add(current.ToString()); current.Clear(); }
            else current.Append(c);
        }
        result.Add(current.ToString());
        return result.ToArray();
    }
}
