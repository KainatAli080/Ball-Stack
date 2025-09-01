using System;
using UnityEngine;

[Serializable]
public struct TowerShape
{
    public GameObject oneFourthCollider;
    public GameObject twoFourthCollider;
    public GameObject threeFourthCollider;
    public GameObject noneCollider;
}

public class LevelSpawner : MonoBehaviour
{
    [Header("Prefabs To Assign")]
    public GameObject[] model;      // All possible platform prefabs
    public GameObject winPrefab;    // The final win platform
    public TowerShape[] towerShapesArray;   // To Contain the serializable shapes possible for creating tower, each will have 4 variants

    [Header("Level Settings")]
    public int level = 1;        // Current level number
    public int addOn = 15;       // Extra platforms for early levels
    public float spacing = 1f;   // Vertical distance between platforms
    public float rotation = 45f;

    private void Start()
    {        
        GenerateTower();        
    }

    private void GenerateTower()
    {
        // So that once player beyond level 9, the tower doesn’t get extra height
        // Reduce tower height for higher levels
        if (level > 15)
            addOn = 0;

        // Decide how many platforms this tower should have
        int platformCountForCurrentLevel = level + addOn;
        TowerShape chosenShape = GetShapePrefabForLevel();

        // Spawn platforms
        for (int k = 0; k < platformCountForCurrentLevel; k++)
        {
            GameObject prefabToSpawn = GetCoveredPortionOfTowerShape(chosenShape);
            GameObject platform = Instantiate(prefabToSpawn, transform);

            // Place platform downwards (-Y axis)
            float yPos = -k * spacing;
            platform.transform.localPosition = new Vector3(0, yPos, 0);

            // Rotating slightly for variety
            platform.transform.localEulerAngles = new Vector3(0, k * rotation, 0);
        }

        // Spawn win platform at the bottom
        GameObject win = Instantiate(winPrefab, transform);
        win.transform.localPosition = new Vector3(0, -(platformCountForCurrentLevel * spacing), 0);
    }

    private TowerShape GetShapePrefabForLevel()
    {
        int len = towerShapesArray.Length;

        if (level <= 20)
            return towerShapesArray[UnityEngine.Random.Range(0, len/3)];     
        else if (level > 20 && level < 50)
            return towerShapesArray[UnityEngine.Random.Range(1, len/2)];     
        else if (level >= 50 && level < 100)
            return towerShapesArray[UnityEngine.Random.Range(2, len)];     
        else
            return towerShapesArray[UnityEngine.Random.Range(0, len)];      // Piick completely randomly
    }

    private GameObject GetCoveredPortionOfTowerShape(TowerShape chosenShape)
    {
        // Then pick one of its 4 variations
        int variationInTower = UnityEngine.Random.Range(0, 4);  // random.range for int is exclusive of upper limit but float is inclusive
        switch(variationInTower)
        {
            case 0:
                return chosenShape.noneCollider;
            case 1:
                return chosenShape.oneFourthCollider;
            case 2: 
                return chosenShape.twoFourthCollider;
            case 3: 
                return chosenShape.threeFourthCollider;
        }
        return chosenShape.noneCollider;
    }
}
