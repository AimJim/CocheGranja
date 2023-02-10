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
    
    [SerializeField]
    int MAXPOSICIONES = 20;

    Mesh brakeMesh;
    
    private void Awake()
    {

        brakeMesh = new Mesh { name = "brakeMesh" };
        GetComponent<MeshFilter>().mesh = brakeMesh;
        GetComponent<MeshRenderer>().material = bMaterial;
    }

    private void OnDisable()
    {
        brakeMesh.Clear();
    }
    private void Update()
    {
                
        if (posiciones.Count >= MAXPOSICIONES)
        {
            posiciones.RemoveAt(0);
        }
        

        posiciones.Add(gameObject.transform.position);

        
        //Hasta aqui funciona
        //Dibujar la mesh a mano (XD momento)
        List<Vector3> vertices = new List<Vector3>();
        foreach(Vector3 p in posiciones)
        {
            //TODO añadir la rotacion (calcularla con sencos)
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(p.x-horizontalOffset,p.y-verticalOffset,p.z)));
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(p.x+horizontalOffset, p.y-verticalOffset, p.z)));
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(p.x - horizontalOffset, p.y + verticalOffset, p.z)));
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(p.x + horizontalOffset, p.y + verticalOffset, p.z)));
        }
       
        while(vertices.Count < MAXPOSICIONES * 4)
        {
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(posiciones[posiciones.Count-1].x - horizontalOffset, posiciones[posiciones.Count - 1].y - verticalOffset, posiciones[posiciones.Count - 1].z)));
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(posiciones[posiciones.Count - 1].x + horizontalOffset, posiciones[posiciones.Count - 1].y - verticalOffset, posiciones[posiciones.Count - 1].z)));
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(posiciones[posiciones.Count - 1].x - horizontalOffset, posiciones[posiciones.Count - 1].y + verticalOffset, posiciones[posiciones.Count - 1].z)));
            vertices.Add(gameObject.transform.InverseTransformPoint(new Vector3(posiciones[posiciones.Count - 1].x + horizontalOffset, posiciones[posiciones.Count - 1].y + verticalOffset, posiciones[posiciones.Count - 1].z)));
        }

        brakeMesh.vertices = vertices.ToArray();
        List<int> intList = new List<int>();
        //Primera cara
        
        for(int i = 0; i <posiciones.Count; i++)
        {
            
            //Primera y ultimta (tapas)
            if(i== 0)
            {
                intList.Add(0+4*i);
                intList.Add(2+4*i);
                intList.Add(1+4*i);

                intList.Add(1+4*i);
                intList.Add(2+4*i);
                intList.Add(3+4*i);
            } else if(i == posiciones.Count - 2)
            {
                intList.Add(1 + (4 * (i + 1)));
                intList.Add(3 + (4 * (i + 1)));
                intList.Add(0 + (4 * (i + 1)));

                intList.Add(0 + (4 * (i + 1)));
                intList.Add(3 + (4 * (i + 1)));
                intList.Add(2 + (4 * (i + 1)));
            }

            if(i < posiciones.Count - 2)
            {
                //Cara izquierda
                intList.Add(1 + (4*(i+1)));
                intList.Add(3 + 4 * i);
                intList.Add(1 + 4 * i);

                intList.Add(3 + (4 * (i +1)));
                intList.Add(3 + 4 * i);
                intList.Add(1 + (4 * (i+1)));

                //Cara derecha
                intList.Add(0 + 4 * i);
                intList.Add(2 + 4 * i);
                intList.Add(0 + (4 * (i+1)));

                intList.Add(0 + (4 * (i+1)));
                intList.Add(2 + 4 * i);
                intList.Add(2 + (4 * (i+1)));

                //Arriba
                intList.Add(0 + 4 * i);
                intList.Add(0 + (4 * (i+1)));
                intList.Add(1 + 4 * i);

                intList.Add(1 + 4 * i);
                intList.Add(0 + (4 * (i+1)));
                intList.Add(1 + (4 * (i+1)));

                //Abajo
                intList.Add(2 + (4 * (i + 1)));
                intList.Add(2 + 4 * i);
                intList.Add(3 + (4 * (i + 1)));
                
                
                intList.Add(3 + (4 * (i + 1)));
                intList.Add(2 + 4 * i);
                intList.Add(3 + 4 * i);
                
            }
        }
        brakeMesh.triangles = intList.ToArray(); // Horizontales, arreglarlas y añadir verticales (Techo y suelo)
        
        /*
        GameObject coso = Instantiate(trailMesh);
        coso.transform.position = gameObject.transform.position;
        coso.transform.rotation = gameObject.transform.rotation;
        //if(prev!=null) coso.transform.LookAt(prev.transform);

        prev = coso;*/
    }
}
