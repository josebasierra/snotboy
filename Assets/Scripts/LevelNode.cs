using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNode : MonoBehaviour
{
    [SerializeField] string LevelName;
    [SerializeField] List<Transform> dependantNodes;

    [SerializeField] GameObject edgePrefab;

    void Start()
    {
        foreach(var dependantNode in dependantNodes)
        {
            var lineRenderer = Instantiate(edgePrefab, this.transform).GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, dependantNode.transform.position);
        }
    }


    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(LevelName);
            GameManager.Instance().LoadScene(LevelName);
        }
    }
}
