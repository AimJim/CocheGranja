using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BrakeTrail : MonoBehaviour
{

    [SerializeField]
    GameObject trailMesh;
    GameObject prev = null;
    [SerializeField]
    float horizontalOffset;
    [SerializeField]
    float verticalOffset;
    [SerializeField]
    Material bMaterial;

    List<Vector3> posiciones =  new List<Vector3>();
    
    int MAXPOSICIONES = 20;

    Mesh brakeMesh;
    
    private void Awake()
    {

        brakeMesh = new Mesh { name = "brakeMesh" };
        GetComponent<MeshFilter>().mesh = brakeMesh;
        GetComponent<MeshRenderer>().material = bMaterial;
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
            vertices.Add(new Vector3(p.x-horizontalOffset,p.y-verticalOffset,p.z));
            vertices.Add(new Vector3(p.x+horizontalOffset, p.y-verticalOffset, p.z));
            vertices.Add(new Vector3(p.x - horizontalOffset, p.y + verticalOffset, p.z));
            vertices.Add(new Vector3(p.x + horizontalOffset, p.y + verticalOffset, p.z));
        }
        Debug.Log(gameObject.transform.position + "GAMEOBJECT");
        Debug.Log(vertices[0]);
        while(vertices.Count < MAXPOSICIONES * 4)
        {
            vertices.Add(new Vector3(posiciones[posiciones.Count-1].x - horizontalOffset, posiciones[posiciones.Count - 1].y - verticalOffset, posiciones[posiciones.Count - 1].z));
            vertices.Add(new Vector3(posiciones[posiciones.Count - 1].x + horizontalOffset, posiciones[posiciones.Count - 1].y - verticalOffset, posiciones[posiciones.Count - 1].z));
            vertices.Add(new Vector3(posiciones[posiciones.Count - 1].x - horizontalOffset, posiciones[posiciones.Count - 1].y + verticalOffset, posiciones[posiciones.Count - 1].z));
            vertices.Add(new Vector3(posiciones[posiciones.Count - 1].x + horizontalOffset, posiciones[posiciones.Count - 1].y + verticalOffset, posiciones[posiciones.Count - 1].z));
        }

        brakeMesh.vertices = vertices.ToArray();
        brakeMesh.triangles = new int[]
        {
            0,2,1,1,2,3,
            4,2,0,6,4,2,
            1,7,5,3,7,5,
            8,6,4,6,2,4,
            5,7,9,7,1,9,
            12,10,8,14,10,12,
            9,11,13,11,15,13,
            16,14,12,18,14,16,
            15,19,13,13,19,17,
            20,18,16,22,18,20,
            19,23,17,17,23,21,
            24,22,20,26,22,24,
            23,27,12,21,27,25,
            28,26,24,30,26,28,
            27,31,25,25,31,29,
            32,30,28,34,30,32,
            31,35,29,29,35,33,
            36,34,32,38,34,36,
            35,39,33,33,39,37,
            40,38,36,42,38,40,
            39,43,37,37,43,41,
            44,42,40,46,42,44,
            43,37,41,41,47,45,
            48,46,44,59,46,48,
            47,51,45,45,51,49,
            54,50,52,52,50,48,
            51,55,49,49,55,53,
            56,54,52,58,54,56,
            55,59,53,53,59,57,
            60,58,56,62,58,60,
            59,63,57,57,63,61,
            64,62,60,66,62,64,
            63,67,61,61,67,65,
            68,66,64,70,66,68,
            67,71,65,65,72,69,
            72,70,68,74,70,72,
            71,75,69,69,75,73,
            76,74,72,78,74,76,
            75,79,73,73,79,77 // Horizontales, arreglarlas y añadir verticales (Techo y suelo)
        };
        /*
        GameObject coso = Instantiate(trailMesh);
        coso.transform.position = gameObject.transform.position;
        coso.transform.rotation = gameObject.transform.rotation;
        //if(prev!=null) coso.transform.LookAt(prev.transform);

        prev = coso;*/
    }
}
