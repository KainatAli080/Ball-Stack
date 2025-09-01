using System.Collections;
using UnityEngine;

// manager of the whole stack section.
public class StackController : MonoBehaviour
{
    [SerializeField] private StackPartController[] stackPartController = null;

    public void ShatterAllParts()
    {
        if(transform.parent != null)
        {
            transform.parent = null;
        }

        foreach(var part in stackPartController)
        {
            part.ShatterObject();
        }
        StartCoroutine(RemoveParts());
    }

    IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
