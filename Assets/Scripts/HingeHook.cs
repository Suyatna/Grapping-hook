using UnityEngine;

public class HingeHook : MonoBehaviour
{
    private bool _grabbing;

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRenderer = gameObject.GetComponentInChildren<LineRenderer>();
        lineRenderer.startColor = Color.white;
        lineRenderer.SetWidth(0.08f, 0.08f);
        
        if (Input.GetMouseButtonDown(0))
        {
            _grabbing = true;
        }

        if (Input.GetMouseButton(0))
        {
            lineRenderer.positionCount = 2;
            GameObject closest = FindNearest();

            if (closest != null)
            {
                if (_grabbing)
                {
                    lineRenderer.SetPosition(1, closest.transform.position);
                
                    closest.GetComponentInChildren<HingeJoint2D>().connectedBody =
                        gameObject.GetComponentInChildren<Rigidbody2D>();

                    _grabbing = false;

                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                }
            
                lineRenderer.SetPosition(0, transform.position);   
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            GameObject[] hinges;
            hinges = GameObject.FindGameObjectsWithTag("Hinge");

            lineRenderer.positionCount = 0;
            
            foreach (var go in hinges)
            {
                go.GetComponentInChildren<HingeJoint2D>().connectedBody = null;
            }
            
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
    }

    private GameObject FindNearest()
    {
        GameObject[] hinges;
        hinges = GameObject.FindGameObjectsWithTag("Hinge");

        GameObject closest = null;

        float distance = 100;
        Vector3 position = transform.position;

        foreach (var go in hinges)
        {
            Vector3 different = go.transform.position - position;
            float currentDistance = different.sqrMagnitude;

            if (currentDistance < distance)
            {
                closest = go;
                distance = currentDistance;
            }
        }

        return closest;
    }
}
