using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BrakeTrail : MonoBehaviour
{
    [SerializeField]
    Transform parentTransform;
    [SerializeField]
    Transform e1;
    [SerializeField]
    Transform e2;
    [SerializeField]
    Transform e3;
    [SerializeField]
    Transform e4;
    
    
    [SerializeField]
    float horizontalOffset;
    [SerializeField]
    float verticalOffset;
    [SerializeField]
    Material bMaterial;

  

    List<Vector3> posE1 = new List<Vector3>();
    List<Vector3> posE2 = new List<Vector3>();
    List<Vector3> posE3 = new List<Vector3>();
    List<Vector3> posE4 = new List<Vector3>();

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
        for (int i = 0; i< posE1.Count; i++)
        {
            
            posE1.RemoveAt(i);
            posE2.RemoveAt(i);
            posE3.RemoveAt(i);
            posE4.RemoveAt(i);
        }
        Color color = bMaterial.color;
        color.a = maxAlpha;
        bMaterial.color = color;
        stopAnim = true;
        Debug.Log(color.a);
       
    }
    
    private void FixedUpdate()
    {
                
        if (posE1.Count >= MAXPOSICIONES)
        {
            
            posE1.RemoveAt(0);
            posE2.RemoveAt(0);
            posE3.RemoveAt(0);
            posE4.RemoveAt(0);

        }
        

        posE1.Add(e1.position);
        posE2.Add(e2.position);
        posE3.Add(e3.position);
        posE4.Add(e4.position);

        RenderTrail();
        
        
        
        
    }

    private void RenderTrail()
    {

        List<Vector3> vertices = new List<Vector3>();
        
        for (int i = 0; i< posE1.Count; i++)
        {
            //TODO añadir la rotacion (calcularla con sencos)

            vertices.Add(gameObject.transform.InverseTransformPoint(posE1[i]));
            vertices.Add(gameObject.transform.InverseTransformPoint(posE2[i]));
            vertices.Add(gameObject.transform.InverseTransformPoint(posE3[i]));
            vertices.Add(gameObject.transform.InverseTransformPoint(posE4[i]));

            

        }

       
        brakeMesh.vertices = vertices.ToArray();
        List<int> intList = new List<int>();
        

        //Renderizar caras (Bien)
        for (int i = 0; i < posE1.Count; i++)
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
            else if (i == posE1.Count - 1)
            {// No se dibuja
                intList.Add(3 + (4 * i));
                intList.Add(1 + (4 * i));
                intList.Add(0 + (4 * i));

                intList.Add(2 + (4 * i));
                intList.Add(3 + (4 * i));
                intList.Add(0 + (4 * i));
            }

            if (i < posE1.Count - 1)
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
