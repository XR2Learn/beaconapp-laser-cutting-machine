using System;
using UnityEngine;
using System.Collections.Generic;
using XdeEngine.Core;

namespace VMachina
{
  public class XdeCollisionUVGetter : MonoBehaviour
  {
    public XdeCollisionMonitor monitor;

    public Material pointMaterial = null;
    public Material avatarMaterial = null;
    public float pointScale = 0.02f;
    Mesh pointMesh = null;
    List<Matrix4x4> contact_points_matrix = new List<Matrix4x4>();
    private List<Material> bodyMat = new List<Material>();

    public delegate void BodyCollideWithGameObject(XdeRigidBody p_body, XdeRigidBody p_collide);
    public event BodyCollideWithGameObject OnStartCollision;

    void Reset()
    {
      monitor = GetComponent<XdeCollisionMonitor>();

      ResetTemplate();
    }

    void ResetTemplate()
    {
      if (pointMaterial == null)
      {
        Shader templateShader = Resources.Load("ContactArrowShader") as Shader;
        if (templateShader != null)
        {
          pointMaterial = new Material(templateShader);
          pointMaterial.SetColor("_Color", Color.red);
          pointMaterial.enableInstancing = true;
        }
      }

      GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      pointMesh = tmp.GetComponent<MeshFilter>().sharedMesh;
      DestroyImmediate(tmp);

    }

    public void updateAvatarsCollisionInfo()
    {
	    foreach (Transform child in transform.parent.GetChild(1))
	    {
		    monitor.bodies.Add(child.GetComponent<XdeRigidBody>());
	    }
	    monitor.Reset();
	    monitor.CreateContactMonitors();
	    monitor.Init();

	    for (int i = 0; i < monitor.BodyColliders.Count; i++)
	    {
		    monitor.BodyColliders[i].OnCollisionStartBody += OnCollisionStartOnBody;
		    //monitor.BodyColliders[i].OnCollisionDetected += OnCollisionStayOnBody;
		    monitor.BodyColliders[i].OnCollisionEndBody += OnCollisionEndOnBody;
	    }
	    
	    //initialize and set materials instance on Avatar
	    for (int i = 0; i < monitor.BodyColliders.Count; i++)
	    {
		    Renderer rend = monitor.bodies[i].gameObject.transform.GetChild(0).GetComponent<Renderer>();
		    Material l_mat = new Material(avatarMaterial);
		    rend.sharedMaterial = l_mat;
		    bodyMat.Add(l_mat); //TODO list a remplir plus tot
		    bodyMat[i].SetFloat("_ImpactScale", 0.0f); //0.02f);
	    }
    }

    void Start()
    {
	    for (int i = 0; i < monitor.BodyColliders.Count; i++)
	    {
		    Renderer rend = monitor.bodies[i].gameObject.transform.GetChild(0).GetComponent<Renderer>();
		    Material l_mat = new Material(avatarMaterial);
		    rend.sharedMaterial = l_mat;
		    bodyMat.Add(l_mat);                        //TODO list a remplir plus tot
		    bodyMat[i].SetFloat("_ImpactScale", 0.0f); //0.02f);
	    }
      ResetTemplate();
    }

    private void OnCollisionStartOnBody(XdeBody p_body, XdeBody p_objecttouched)
    {
	    XdeRigidBody l_rigidBody = p_body as XdeRigidBody;
	    OnStartCollision?.Invoke(l_rigidBody, p_objecttouched as XdeRigidBody);
	    FindImpactPoint(l_rigidBody);
    }
    
    private void OnCollisionEndOnBody(XdeBody p_body)
    {
	    XdeRigidBody l_rigidBody = p_body as XdeRigidBody;
	    bodyMat[monitor.bodies.IndexOf(l_rigidBody)].SetFloat("_ImpactScale", 0.0f);
    }

    void OnCollisionStayOnBody(XdeBody touchedBody,
                                        XdeBody otherTouchedBody,
                                        float impactForce,
                                        Vector3 worldPosition)
    {
	    //Debug.Log("On Collision Stay between our bodie : " + touchedBody.name + "and the object : " + otherTouchedBody + " on position : " + worldPosition + " with force " + impactForce);
	    //OnBodyCollision?.Invoke(touchedBody.gameObject, otherTouchedBody.gameObject);
	    FindImpactPoint(touchedBody as XdeRigidBody);
    }

    void FindImpactPoint(XdeRigidBody p_body)
    {
	    contact_points_matrix.Clear();

	    int index = monitor.bodies.IndexOf(p_body);
	    
	    foreach (var cps in monitor.BodyColliders[index].collidersData.Values)
		    for (int j = 0; j < cps.Count; j++)
		    {
			    Vector3 p1 = cps[j].colliderContactPoint;
			    Vector3 p2 = cps[j].bodyContactPoint;
			    Vector3 dir = -cps[j].normal;
			    dir.Normalize();
			    //if (Physics.Raycast(p1 - 0.01f * dir, dir, out RaycastHit hitInfo))
			    //{
				    //Renderer rend = hitInfo.transform.GetComponent<Renderer>(); //TODO list a remplir plus tot
				    //Vector2 uv = hitInfo.textureCoord;
				    //int triangleId = hitInfo.triangleIndex;
				    //Material mat = rend.sharedMaterial;
				    bodyMat[index].SetFloat("_ImpactScale", 8.0f); //0.02f);
				    //mat.SetColor("_ImpactPosition", new Color(uv.x,uv.y,0,0));

				    //Vector3 p = hitInfo.point;

				    //for debug
				    contact_points_matrix.Add(Matrix4x4.TRS(p1, Quaternion.identity, pointScale * Vector3.one));

				    //do sthg...
			    //}
		    }

	    Graphics.DrawMeshInstanced(pointMesh, 0, pointMaterial, contact_points_matrix);

    }
    
    void FindImpactPointOnAll()
    {
	    for (int i = 0; i < monitor.bodies.Count; i++)
	    {
		    FindImpactPoint(monitor.bodies[i]);
	    }
    }
    
   //  void Update()
   //  {
   //     FindImpactPointOnAll();
   //  }

    private void OnApplicationQuit()
    {
	    for (int i = 0; i < monitor.BodyColliders.Count; i++)
	    {
		    bodyMat[i].SetFloat("_ImpactScale", 0.0f); //0.02f);
	    }
    }
  }
}
