using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BrakeTrail : MonoBehaviour
{
    [SerializeField]
    Transform parentTransform;
    
    
    [SerializeField]
    float horizontalOffset;
    [SerializeField]
    float verticalOffset;
    [SerializeField]
    Material bMaterial;

    List<Vector3> posiciones =  new List<Vector3>();
    List<Vector3> rotaciones = new List<Vector3>();
    
    [SerializeField]
    int MAXPOSICIONES = 20;
    [SerializeField]
    float maxAlpha;

    Mesh brakeMesh;
    
    private void Awake()
    {

        brakeMesh = new Mesh { name = "brakeMesh" };
        GetComponent<MeshFilter>().mesh = brakeMesh;
        GetComponent<MeshRenderer>().material = bMaterial;
    }

    bool stopAnim = false;
    private void OnDisable()
    {
        stopAnim = false;
        
        StartCoroutine(difuseBrake());
    }

    IEnumerator difuseBrake()
    {
        if (!stopAnim)
        {
            
            Color color = bMaterial.color;
            if(color.a > 0)
            {
                //Debug.Log(color.a);
                color.a -= 0.01f;
                bMaterial.color = color;
            }
            RenderTrail();
            yield return (new WaitForSeconds(0.1f));
            StartCoroutine(difuseBrake());
            
        }
        
    
        stopAnim = false;
    }

    private void OnEnable()
    {
        brakeMesh.Clear();
        for (int i = 0; i< posiciones.Count; i++)
        {
            posiciones.RemoveAt(i);
            rotaciones.RemoveAt(i);
        }
        Color color = bMaterial.color;
        color.a = maxAlpha;
        bMaterial.color = color;
        stopAnim = true;
        Debug.Log(color.a);
       
    }
    
    private void FixedUpdate()
    {
                
        if (posiciones.Count >= MAXPOSICIONES)
        {
            posiciones.RemoveAt(0);
            rotaciones.RemoveAt(0);
        }
        

        posiciones.Add(gameObject.transform.position);
        rotaciones.Add(transform.rotation.eulerAngles);
        RenderTrail();
        Debug.Log(gameObject.transform.rotation.eulerAngles);
        
        
        
    }

    private void RenderTrail()
    {

        List<Vector3> vertices = new List<Vector3>();
        int counter = 0;
        foreach (Vector3 p in posiciones)
        {
            //TODO añadir la rotacion (calcularla con sencos)
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(p.x + horizontalOffset, p.y - verticalOffset, p.z)));
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(p.x - horizontalOffset, p.y - verticalOffset, p.z)));
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(p.x + horizontalOffset, p.y + verticalOffset, p.z)));
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(p.x - horizontalOffset, p.y + verticalOffset, p.z)));
            counter++;
        }

       
        brakeMesh.vertices = vertices.ToArray();
        List<int> intList = new List<int>();
        

        //Renderizar caras (Bien)
        for (int i = 0; i < posiciones.Count; i++)
        {

            //Primera y ultimta (tapas)
            if (i == 0)
            {
                intList.Add(1 + 4 * i);
                intList.Add(3 + 4 * i);
                intList.Add(0 + 4 * i);

                intList.Add(0 + 4 * i);
                intList.Add(3 + 4 * i);
                intList.Add(2 + 4 * i);
            }
            else if (i == posiciones.Count - 1)
            {// No se dibuja
                intList.Add(3 + (4 * i));
                intList.Add(1 + (4 * i));
                intList.Add(0 + (4 * i));

                intList.Add(2 + (4 * i));
                intList.Add(3 + (4 * i));
                intList.Add(0 + (4 * i));
            }

            if (i < posiciones.Count - 1)
            {
                //Cara izquierda
                intList.Add(1 + (4 * (i + 1)));
                intList.Add(3 + 4 * i);
                intList.Add(1 + 4 * i);

                intList.Add(3 + (4 * (i + 1)));
                intList.Add(3 + 4 * i);
                intList.Add(1 + (4 * (i + 1)));

                //Cara derecha
                intList.Add(0 + 4 * i);
                intList.Add(2 + 4 * i);
                intList.Add(0 + (4 * (i + 1)));

                intList.Add(0 + (4 * (i + 1)));
                intList.Add(2 + 4 * i);
                intList.Add(2 + (4 * (i + 1)));

                //Arriba
                intList.Add(0 + 4 * i);
                intList.Add(0 + (4 * (i + 1)));
                intList.Add(1 + 4 * i);

                intList.Add(1 + 4 * i);
                intList.Add(0 + (4 * (i + 1)));
                intList.Add(1 + (4 * (i + 1)));

                //Abajo
                intList.Add(2 + (4 * (i + 1)));
                intList.Add(2 + 4 * i);
                intList.Add(3 + (4 * (i + 1)));


                intList.Add(3 + (4 * (i + 1)));
                intList.Add(2 + 4 * i);
                intList.Add(3 + 4 * i);

            }
        }
        brakeMesh.triangles = intList.ToArray();

    }
}
