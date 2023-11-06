using System.Collections.Generic;
using UnityEngine;
using Valve.Newtonsoft.Json.Utilities;
using XdeEngine.Core;
using XdeEngine.Core.Monitoring;

namespace VMachina
{
  public class XdeCollisionMonitor : MonoBehaviour
  {
    public List<XdeRigidBody> bodies = new List<XdeRigidBody>();
    public List<XdeContactMonitor> contactMonitors = new List<XdeContactMonitor>();
    private List<XdeInterferenceMonitor> interferenceMonitors = new List<XdeInterferenceMonitor>();
    public List<XdeBodyColliders> bodyColliders = new List<XdeBodyColliders>();
    
    public List<XdeBodyColliders> BodyColliders { get { return bodyColliders; } }

    public XdeLayers layers;

    [System.Serializable]
    public class XdeBodyColliderData
    {
      public Vector3 bodyContactPoint;//in world ref
      public Vector3 colliderContactPoint;//in world ref
      public float normalForce;
      public Vector3 normal;//in world ref

      public XdeBodyColliderData(Vector3 p1, Vector3 p2, float f, Vector3 n)
      {
        bodyContactPoint = p1;
        colliderContactPoint = p2;
        normalForce = f;
        normal = n;
      }
    }


    [System.Serializable]
    public class XdeBodyColliders
    {
      XdeRigidBody m_body;
      public List<XdeBody> colliders = new List<XdeBody>();
      public Dictionary<XdeBody, List<XdeBodyColliderData>> collidersData = new Dictionary<XdeBody, List<XdeBodyColliderData>>();

      public delegate void OnCollisionStartRigidBody(XdeBody avatarparts, XdeBody objecttouched);
      public event OnCollisionStartRigidBody OnCollisionStartBody;
      public delegate void OnCollisionEndRigidBody(XdeBody body);
      public event OnCollisionEndRigidBody OnCollisionEndBody;
      
      public delegate void OnCollisionDetectedHandler(XdeBody touchedBody, XdeBody otherTouchedBody, float impactForce, Vector3 worldPosition);
      /// Subscribe to this event to know when there is a collision detected
      public event OnCollisionDetectedHandler OnCollisionDetected;

      XsmSceneManager sceneManager;
      XsmSceneManager SceneManager
      {
        get
        {
          if (sceneManager == null)
            sceneManager = (FindObjectOfType<XdeScene>().UnityComponent as XdeEngine.Core.Component.UCScene).Manager;
          return sceneManager;
        }
      }

      public XdeBodyColliders(XdeRigidBody rb)
      {
        m_body = rb;
      }

      public void AddBody(XdeBody body)
      {
        if (!colliders.Contains(body))
          colliders.Add(body);
        if (!collidersData.ContainsKey(body))
          collidersData.Add(body, new List<XdeBodyColliderData>());
        OnCollisionStartBody?.Invoke(m_body, body);
      }

      public void RemoveBody(XdeBody body)
      {
        if (colliders.Contains(body))
          colliders.Remove(body);
        if (collidersData.ContainsKey(body))
          collidersData.Remove(body);
        OnCollisionEndBody?.Invoke(m_body);
      }

      public void AddContactPoints(xde_types.core.contact_bodypair p)
      {
        long bodyId = m_body.UnityComponent.ICComponent.getId();

        XdeBody collidedBody;
        XdeBody otherCollidedBody;

        if (bodyId == p.body_i)
        {
          collidedBody = SceneManager.GetBody((xde.client.core.Body)SceneManager.Client.getComponent(p.body_j));
          otherCollidedBody = SceneManager.GetBody((xde.client.core.Body)SceneManager.Client.getComponent(p.body_i));
        }
        else
        {
          collidedBody = SceneManager.GetBody((xde.client.core.Body)SceneManager.Client.getComponent(p.body_i));
          otherCollidedBody = SceneManager.GetBody((xde.client.core.Body)SceneManager.Client.getComponent(p.body_j));
        }

        if (collidersData.ContainsKey(collidedBody))
          collidersData[collidedBody].Clear();
        else
          collidersData.Add(collidedBody, new List<XdeBodyColliderData>());

        if (bodyId == p.body_i)
          for (int i=0; i<p.points.Count; i++)
          {
            collidersData[collidedBody].Add(new XdeBodyColliderData(p.points[i].a_i, p.points[i].a_j, p.points[i].n_force, p.points[i].n_i));
            OnCollisionDetected?.Invoke(otherCollidedBody, collidedBody, p.points[i].n_force, p.points[i].a_i);
          }
        else
          for (int i = 0; i < p.points.Count; i++)
          {
            collidersData[collidedBody].Add(new XdeBodyColliderData(p.points[i].a_j, p.points[i].a_i, p.points[i].n_force, p.points[i].n_j));
            OnCollisionDetected?.Invoke(otherCollidedBody, collidedBody, p.points[i].n_force, p.points[i].a_j);
          }

      }
    }

    public void Reset()
    {
      layers = FindObjectOfType<XdeScene>().GetComponent<XdeLayers>();
    }

    void Awake()
    {
      InitBodyMonitors();
      RegisterEvents();
    }

    public void Init()
    {
	    InitBodyMonitors();
	    RegisterEvents();
    }

    void OnDestroy()
    {
      UnregisterEvents();
    }

	  void RegisterEvents()
    {
      for (int i = 0; i < bodies.Count; i++)
      {
        if (contactMonitors[i] != null)
        {
          contactMonitors[i].OnBodyCollisionStart += bodyColliders[i].AddBody;
          contactMonitors[i].OnBodyCollisionEnd += bodyColliders[i].RemoveBody;
          contactMonitors[i].OnBodyCollision += bodyColliders[i].AddContactPoints;
        }
        if (interferenceMonitors[i] != null)
        {
          interferenceMonitors[i].OnBodyInterferenceStart += bodyColliders[i].AddBody;
          interferenceMonitors[i].OnBodyInterferenceEnd += bodyColliders[i].RemoveBody;
        }
      }
    }

    void UnregisterEvents()
    {
      for (int i = 0; i < bodies.Count; i++)
      {
        if (contactMonitors[i] != null)
        {
          contactMonitors[i].OnBodyCollisionStart -= bodyColliders[i].AddBody;
          contactMonitors[i].OnBodyCollisionEnd -= bodyColliders[i].RemoveBody;
          contactMonitors[i].OnBodyCollision -= bodyColliders[i].AddContactPoints;
        }
        if (interferenceMonitors[i] != null)
        {
          interferenceMonitors[i].OnBodyInterferenceStart -= bodyColliders[i].AddBody;
          interferenceMonitors[i].OnBodyInterferenceEnd -= bodyColliders[i].RemoveBody;
        }
      }
    }

    #region human utils

	  void InitBodyMonitors()
    {
      contactMonitors.Clear();
      interferenceMonitors.Clear();
      bodyColliders.Clear();
      for (int i = 0; i < bodies.Count; i++)
      {
        contactMonitors.Add(bodies[i].GetComponent<XdeContactMonitor>());
        interferenceMonitors.Add(bodies[i].GetComponent<XdeInterferenceMonitor>());
        bodyColliders.Add(new XdeBodyColliders(bodies[i]));
      }
    }

    public void CreateContactMonitors()
    {
      for (int i=0; i<bodies.Count; i++)
      {
        XdeContactMonitor monitor = bodies[i].GetComponent<XdeContactMonitor>();
        if (monitor == null)
          monitor = bodies[i].gameObject.AddComponent<XdeContactMonitor>();
        monitor.layers = layers;
        monitor.mode = XdeContactMonitor.MonitorMode.Body;
        monitor.bodyA = bodies[i].GetComponent<XdeBody>();
        monitor.layers = layers;
      }
    }

    public void CreateInterferenceMonitors()
    {
      for (int i = 0; i < bodies.Count; i++)
      {
        XdeInterferenceMonitor monitor = bodies[i].GetComponent<XdeInterferenceMonitor>();
        if (monitor == null)
          monitor = bodies[i].gameObject.AddComponent<XdeInterferenceMonitor>();
        monitor.mode = XdeInterferenceMonitor.MonitorMode.Body;
        monitor.bodyA = bodies[i].GetComponent<XdeBody>();
        monitor.layers = layers;
      }
    }

    #endregion
  }
}
