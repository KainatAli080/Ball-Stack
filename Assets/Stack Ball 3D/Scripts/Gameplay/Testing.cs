using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject[] model;
    [HideInInspector]
    public GameObject[] modelPrefab = new GameObject[4];
    public GameObject winPrefab;

    private GameObject temp1, temp2;

    public int level = 1;
    public int addOn = 7;
    public float i = 0;

    private void Start()
    {
        if (level > 9)
            addOn = 0;

        ModelSelection();
        float random = Random.value;
        for (i = 0; i < -level - addOn; i -= 0.5f)
        {
            if (level <= 20)
                temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]);
            else if (level > 20 || level < 50)
                temp1 = Instantiate(modelPrefab[Random.Range(1, 3)]);
            else if (level >= 50 || level < 100)
                temp1 = Instantiate(modelPrefab[Random.Range(2, 4)]);
            else if (level >= 100)
                temp1 = Instantiate(modelPrefab[Random.Range(3, 4)]);

            temp1.transform.position = new Vector3(0, i - 0.01f, 0);
            temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);

            if(Mathf.Abs(i) >= level * 0.3f && Mathf.Abs(i) <= level * 0.6f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
                temp1.transform.eulerAngles += Vector3.up * 180;
            }
            else if(Mathf.Abs(i) >= level * .8f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
                if (random > .75f)
                {
                    temp1.transform.eulerAngles += Vector3.up * 180;
                }
            }

        }

        temp2 = Instantiate(winPrefab);
        temp2.transform.position = new Vector3(0, i - 0.01f, 0);    // placing the win platform below the final tile
    }

    void ModelSelection()
    {
        int randomModelSelection = Random.Range(0, 5);
        switch (randomModelSelection)
        {
            case 0:
                for (int j = 0; j < 4; j++)
                    modelPrefab[j] = model[j];
                break;

            case 1:
                for (int j = 0; j < 4; j++)
                    modelPrefab[j] = model[j];
                break;

            case 2:
                for (int j = 0; j < 4; j++)
                    modelPrefab[j] = model[j];
                break;

            case 3:
                for (int j = 0; j < 4; j++)
                    modelPrefab[j] = model[j];
                break;

            case 4:
                for (int j = 0; j < 4; j++)
                    modelPrefab[j] = model[j];
                break;
        }
    }
}