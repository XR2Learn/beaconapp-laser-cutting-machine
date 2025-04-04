using System;
using UnityEngine;
using System.Collections.Generic;
using XdeEngine.Core;
using XdeEngine.Core.Device;
using XdeEngine.Hand;
using Valve.VR;
using System.Threading.Tasks;
using System.Linq;
using xde.unity.math;
using XdeEngine.Human;

namespace VMachina
{
	public class TeleporterCustom : MonoBehaviour
	{
		private List<Vector3> m_parabolaPoints;
		public bool m_foundPointCloud = false;
		private xde.unity.OctopclRaycastScene m_scenePcl = null;
		private Vector3 m_initialVelocity = Vector3.forward * 2f;
		private Vector3 Acceleration = Vector3.up * -9.8f;
		public int m_pointCount = 2000;
		public float m_pointSpacing = 0.1f;
		public float m_flySpeedTranslation = 4.5f;
		public float m_flySpeedRotation = 90f;
		private float GraphicThickness = 0.02f;
		private float ParabolaSpeed = 6.0f;
		private Mesh m_parabolaMesh;
		private Material m_parabolaMaterial;
		public Vector3 TeleportationPoint { get; private set; }
		private bool m_validPosition = true;
		private GameObject m_validTeleportPad;
		private GameObject m_invalidTeleportPad;
		private Color validColor = new Color(0.12f, 0.42f, 0.61f, 1.0f);
		private Color invalidColor = new Color(0.6f, 0.12f, 0.12f, 1.0f);
		private GameObject m_player;
		private Transform m_headPos;

		private ArtFlystickMatcher[] m_flysticks;

		//private SteamVR_TrackedController m_controller;
		SteamVR_Input_Sources m_sources;
		private SteamVR_Action_Vector2 m_actionTrackPadPos;
		private XdeScene m_xdeScene;
		private bool m_useLeapMotion;
		private bool m_useXdeBody = false;
		private bool m_useFlysticks;
		private List<XdeSkeleton> skeletons = new List<XdeSkeleton>();
		private List<XdeLeapSmartAttach> smartAttachesLeap = new List<XdeLeapSmartAttach>();
		private List<XdeAsbHandsSmartAttach> smartAttachesVive = new List<XdeAsbHandsSmartAttach>();
		private List<Displacement> previousHandsPosition = new List<xde.unity.math.Displacement>();
		private List<Displacement> m_previousTargetBodiesPosition = new List<Displacement>();
		private List<XdeRigidBody> m_graspRigidBodies;

		private XdeHumanManager m_humanManager;

		private List<Renderer> m_avatarRenderers;
		bool m_avatarDisabled = false;
		private PlayerInfo m_info;


		public delegate void TeleportDoneHandler();
		/// Subscribe to this event to know when the playerId has been received
		public event TeleportDoneHandler Teleported;

		//public NavigationStyle NavigationStyle
		//{
		//	get { return m_info.n; }
		//}

		void OnEnable()
		{
      m_info = this.GetComponentInParent<PlayerInfo>();
      m_player = this.GetComponentInParent<PlayerInfo>().gameObject;
        m_headPos = m_player.transform.GetComponentInChildren<AvatarIK>(true).m_headPosition;

      m_xdeScene = FindObjectOfType<XdeScene>();

      //Set actions
      m_actionTrackPadPos = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("interact", "PadPose");

      for (int i = 0; i < this.transform.parent.parent.parent.GetComponentsInChildren<XdeSkeleton>().Length; i ++)
      {
        skeletons.Add(this.transform.parent.parent.parent.GetComponentsInChildren<XdeSkeleton>()[i]);
      }

      for (int i = 0; i < skeletons.Count; i++)
        if (skeletons[i].GetComponent<XdeAsbHandsSmartAttach>() != null)
          smartAttachesVive.Add(skeletons[i].GetComponent<XdeAsbHandsSmartAttach>());

      //Draw the Pointer
      m_parabolaPoints = new List<Vector3>(m_pointCount);
      m_parabolaMesh = new Mesh();
      m_parabolaMesh.MarkDynamic();
      m_parabolaMesh.name = "Parabolic Pointer";
      m_parabolaMesh.vertices = new Vector3[0];
      m_parabolaMesh.triangles = new int[0];


      m_parabolaMaterial = Resources.Load("Teleport/Materials/Parabola") as Material;
	  if (m_parabolaMaterial != null)
	  {
		  m_parabolaMaterial.enableInstancing = true;
		  m_parabolaMaterial = Instantiate(m_parabolaMaterial);
	  }

      if (!m_validTeleportPad)
      {
        m_validTeleportPad = Instantiate(Resources.Load("Teleport/Prefabs/ValidTeleport") as GameObject, this.transform,
        true);
        m_validTeleportPad.SetActive(false);
      }

      if (!m_invalidTeleportPad)
      {
        m_invalidTeleportPad = Instantiate(Resources.Load("Teleport/Prefabs/InvalidTeleport") as GameObject,
          this.transform, true);
        m_invalidTeleportPad.SetActive(false);
      }

      if (this.name.Contains("ToolAttach"))
        m_initialVelocity = new Vector3(10f, -5f, 0);
      //}
    }

    private void Update()
    {
      NavigationStyle l_currentNameMode = NavigationStyle.TELEPORT;
      if (l_currentNameMode == NavigationStyle.TELEPORT)
      {
        Transform l_transform = transform;
        // 1. Calculate Parabola Points
        m_initialVelocity = Vector3.forward * (8.0f * (90 / Vector3.Angle(l_transform.forward, Vector3.up)));

        Vector3 l_velocity = l_transform.TransformDirection(m_initialVelocity);
        //ClampInitialVelocity(ref velocity);
        Vector3 normal;
        //Acceleration = Vector3.up * -9.8f * (Vector3.Angle(this.transform.forward, Vector3.up)) / 180.0f;

        m_validPosition = CalculateParabolicCurve(l_transform.position, l_velocity, Acceleration, m_pointSpacing,
          m_pointCount, m_parabolaPoints, out normal);
        if (m_foundPointCloud)
          CheckPointCloudCollision(m_parabolaPoints, ref m_validPosition, ref normal);
        TeleportationPoint = m_parabolaPoints[m_parabolaPoints.Count - 1];

        //2. Render Teleport graphics
        m_validTeleportPad.SetActive(m_validPosition);
        m_invalidTeleportPad.SetActive(!m_validPosition);


        if (m_validPosition)
        {
          m_parabolaMaterial.color = validColor;
          m_validTeleportPad.transform.position = TeleportationPoint;
          m_validTeleportPad.transform.LookAt(this.transform.position);
          m_validTeleportPad.transform.Rotate(0, 180, 0);
          var l_up = m_validTeleportPad.transform.up;
          m_validTeleportPad.transform.Rotate(Vector3.Cross(l_up, normal), Vector3.Angle(l_up, normal), Space.World);

          if (PlayerPrefs.GetInt("Interact.OrientableTeleporter", 1) == 1)
          {
            if (this.name.Contains("ToolAttach"))
            {
              // Neutral rotation correction for forward jump. In degres. Positive values are clockside, negative for counterclockside
              float handNeutralRotation = 0f;
              m_validTeleportPad.transform.Rotate(0, -(this.transform.eulerAngles.z - handNeutralRotation) * 2,
                0); //Align teleporter orientation with hand
            }
            else
              m_validTeleportPad.transform.Rotate(0, -this.transform.eulerAngles.z * 2 + 180,
                0); //z: rotate with controller yaw
          }
        }
        else
        {
          m_parabolaMaterial.color = invalidColor;
          m_invalidTeleportPad.transform.position = TeleportationPoint;
          m_invalidTeleportPad.transform.rotation = Quaternion.LookRotation(normal);
          m_invalidTeleportPad.transform.Rotate(90, 0, 0);
        }

        // Draw parabola (BEFORE the outside faces of the selection pad, to avoid depth issues)
        GenerateParabolaMesh(ref m_parabolaMesh, m_parabolaPoints, l_velocity, Time.time * ParabolaSpeed);
        Graphics.DrawMesh(m_parabolaMesh, Matrix4x4.identity, m_parabolaMaterial, gameObject.layer);
      }
      else if (l_currentNameMode == NavigationStyle.FLY)
      {
        if (m_sources == SteamVR_Input_Sources.RightHand || m_sources == SteamVR_Input_Sources.LeftHand)
        {
          if (Mathf.Abs(m_actionTrackPadPos.GetAxis(m_sources).y) > 0.5f)
            m_player.transform.Translate(
              transform.forward * (m_flySpeedTranslation * Mathf.Sign(m_actionTrackPadPos.GetAxis(m_sources).y) * Time.deltaTime),
              Space.World);
          if (Mathf.Abs(m_actionTrackPadPos.GetAxis(m_sources).x) > 0.5f)
            m_player.transform.RotateAround(m_headPos.position, Vector3.up,
              Mathf.Sign(m_actionTrackPadPos.GetAxis(m_sources).x) * m_flySpeedRotation * Time.deltaTime);
        }

        if (m_useFlysticks)
        {
          float l_joystickX = m_flysticks.Select(p_flystick => p_flystick.joystickX).OrderByDescending(Mathf.Abs)
                           .FirstOrDefault(); //Get highest X joystick value from all flysticks
          float l_joystickY = m_flysticks.Select(p_flystick => p_flystick.joystickY).OrderByDescending(Mathf.Abs)
                           .FirstOrDefault(); //Get highest Y joystick value from all flysticks
          if (Mathf.Abs(l_joystickY) > 0.5f)
            m_player.transform.Translate(this.transform.forward * (m_flySpeedTranslation * Mathf.Sign(l_joystickY) * Time.deltaTime),
              Space.World);
          if (Mathf.Abs(l_joystickX) > 0.5f)
            m_player.transform.RotateAround(m_headPos.position, Vector3.up,
              Mathf.Sign(l_joystickX) * m_flySpeedRotation * Time.deltaTime);
        }
      }
    }

    public void AssignController(SteamVR_Input_Sources p_source)
    {
      m_sources = p_source;
    }

    public async void Teleport()
    {
      //HideAvatar();
      await TeleportAsync();
      //DisplayAvatar();
    }

    public async Task TeleportAsync()
    {
      if (m_validPosition)
      {
        await m_xdeScene.AcquireLock();
        await DetachAllXdeBodiesAsync(); //Disable body tracking

        TeleportVRPlayer();
        await TeleportPhy();

        await AttachAllXdeBodiesAsync(); //Reeanable body tracking
        await m_xdeScene.ReleaseLock();
        Teleported?.Invoke();
      }
      //await Task.Delay(50);
    }


    private void HideAvatar()
    {
      if (m_avatarDisabled == false)
      {
        m_avatarDisabled = true;
        m_avatarRenderers = m_player.GetComponentsInChildren<Renderer>().Where(p_x => p_x.enabled).ToList();
        foreach (Renderer l_mesh in m_avatarRenderers)
          l_mesh.enabled = false;
      }
    }

    private void DisplayAvatar()
    {
      if (m_avatarDisabled)
      {
        foreach (Renderer l_mesh in m_avatarRenderers)
        {
          if (l_mesh)
            l_mesh.enabled = true;
        }
        m_avatarDisabled = false;
      }
    }

    private void TeleportVRPlayer()
    {
      Vector3 l_destination = m_validTeleportPad.transform.position;
      Vector3 l_newPlayerPos = m_player.transform.position + (l_destination - m_headPos.position);
      float l_validpad = m_validTeleportPad.transform.eulerAngles.y;

      m_player.transform.position = new Vector3(l_newPlayerPos.x, l_destination.y, l_newPlayerPos.z);
      m_player.transform.RotateAround(m_headPos.position, Vector3.up, l_validpad - m_headPos.eulerAngles.y);
    }

    async Task TeleportPhy()
    {
      //update phy position of each skeleton (XdeHands and XdeFullBody)
      for (int l_i = 0; l_i < Math.Min(skeletons.Count, previousHandsPosition.Count); l_i++)
      {
        await skeletons[l_i].UpdateSkeletonPositionAsync();
        await skeletons[l_i].GetComponentInChildren<XdeFreeJoint>()
                  .SetPositionAsync(Displacement.FromTransformWorld(m_player.transform) *
                            previousHandsPosition[l_i]);
      }

      for (int l_i = 0; l_i < m_graspRigidBodies.Count; l_i++)
      {
        if (m_graspRigidBodies[l_i] != null)
        {
          Displacement l_dispHand2Rig = Displacement.FromTransformWorld(m_player.transform) *
                          m_previousTargetBodiesPosition[l_i];
          if (m_graspRigidBodies[l_i].GetComponent<XdeWeldableJoint>() != null)
          {
            await m_graspRigidBodies[l_i].GetComponent<XdeWeldableJoint>().SetPositionAsync(l_dispHand2Rig);
          }
          else if (m_graspRigidBodies[l_i].GetComponent<XdeFreeJoint>() != null)
          {
            await m_graspRigidBodies[l_i].GetComponent<XdeFreeJoint>().SetPositionAsync(l_dispHand2Rig);
          }
        }
      }
    }


    async Task DetachAllXdeBodiesAsync()
    {
      previousHandsPosition.Clear();
      for (int l_i = 0; l_i < skeletons.Count; l_i++)
      {
        previousHandsPosition.Add(Displacement.FromTransformWorld(m_player.transform).inverse *
                      Displacement.FromTransformWorld(skeletons[l_i].GetComponentInChildren<XdeFreeJoint>()
                        .transform));
      }
      Debug.Log(previousHandsPosition.Count());


      XdeGraspManipulator[] l_manipulators = m_player.GetComponentsInChildren<XdeGraspManipulator>();
      m_graspRigidBodies = new List<XdeRigidBody>();

      m_previousTargetBodiesPosition = new List<Displacement>();
      foreach (var l_manipulator in l_manipulators)
      {
        if (l_manipulator.AttachedRigidBody != null)
        {
          m_previousTargetBodiesPosition.Add(Displacement.FromTransformWorld(m_player.transform).inverse *
                             Displacement.FromTransformWorld(l_manipulator.AttachedRigidBody
                               .transform));
          m_graspRigidBodies.Add(l_manipulator.AttachedRigidBody);
        }
        else
        {
          m_previousTargetBodiesPosition.Add(Displacement.identity);
          m_graspRigidBodies.Add(null);
        }
      }

      XdeHandTracker[] l_handTrackers = m_player.GetComponentsInChildren<XdeHandTracker>();
      foreach (XdeHandTracker l_handTracker in l_handTrackers)
      {
        XdeLeapSmartAttach l_leapSmartAttach = l_handTracker.GetComponent<XdeLeapSmartAttach>();
        XdeAsbHandsSmartAttach l_smartAttach = l_handTracker.GetComponent<XdeAsbHandsSmartAttach>();

        if (l_leapSmartAttach != null)
        {
          await l_leapSmartAttach.EnableAsync(false);
        }
        else if (l_smartAttach != null)
        {
          await l_smartAttach.Disable();
        }
        else
        {
          if (l_handTracker.isActiveAndEnabled)
            await l_handTracker.DetachAsync();
        }
      }

      if (m_useXdeBody)
      {
        await m_humanManager.DetachUserAsync(0);
      }
    }

    async Task AttachAllXdeBodiesAsync()
    {
      if (m_useXdeBody)
      {
        await m_humanManager.AttachUserAsync(0);
      }

      XdeHandTracker[] l_handTrackers = m_player.GetComponentsInChildren<XdeHandTracker>();
      foreach (XdeHandTracker l_handTracker in l_handTrackers)
      {
        XdeLeapSmartAttach l_leapSmartAttach = l_handTracker.GetComponent<XdeLeapSmartAttach>();
        XdeAsbHandsSmartAttach l_smartAttach = l_handTracker.GetComponent<XdeAsbHandsSmartAttach>();

        if (l_leapSmartAttach != null)
        {
          await l_leapSmartAttach.EnableAsync(true);
        }
        else if (l_smartAttach != null)
        {
          l_smartAttach.Enable();
        }
        else
        {
          if (l_handTracker.isActiveAndEnabled)
            await l_handTracker.AttachAsync();
        }
      }
    }

    // Sample a bunch of points along a parabolic curve until hitting a collider.  At that point, cut off the parabola.
    // p0: starting point of parabola
    // v0: initial parabola velocity
    // a: initial acceleration
    // dist: distance between sample points
    // points: number of sample points
    // outPts: List that will be populated by new points
    // normal: normal of hit point
    private static bool CalculateParabolicCurve(Vector3 p_p0,
                          Vector3 p_v0,
                          Vector3 p_a,
                          float p_dist,
                          int p_points,
                          List<Vector3> p_outPts,
                          out Vector3 p_normal)
    {
      p_outPts.Clear();
      p_outPts.Add(p_p0);
      Vector3 l_last = p_p0;
      float l_t = 0;
      bool l_oldQueriesHitBackfaces = UnityEngine.Physics.queriesHitBackfaces;
      UnityEngine.Physics.queriesHitBackfaces = true;
      for (int l_i = 0; l_i < p_points; l_i++)
      {
        l_t += p_dist / ParabolicCurveDeriv(p_v0, p_a, l_t).magnitude;
        Vector3 l_next = ParabolicCurve(p_p0, p_v0, p_a, l_t);
        Vector3 l_castHit;
        RaycastHit hitinfoForward;
        RaycastHit hitinfoBackward;
        Vector3 l_normal2;
        float l_angle;

        bool l_cast = UnityEngine.Physics.Linecast(l_last, l_next, out hitinfoForward);

        if (l_cast)
        {
          l_castHit = hitinfoForward.point;
          //Debug.Log("HitObjectForward:___________ " + hitinfoForward.collider.name);
          p_outPts.Add(l_castHit);
          //set the normal of Pad object
          l_normal2 = new Vector3(0, 1, 0);
          p_normal = hitinfoForward.normal;
          l_angle = Vector3.Angle(p_normal, l_normal2);
          if (l_angle > 45f && l_angle < 135f)
            return false;
          else
            return true;
        }
        else
          p_outPts.Add(l_next);

        l_last = l_next;
      }

      p_normal = Vector3.up;
      UnityEngine.Physics.queriesHitBackfaces = l_oldQueriesHitBackfaces;

      return false;
    }

    private void CheckPointCloudCollision(List<Vector3> p_polyline, ref bool p_validPosition, ref Vector3 p_normal)
    {
      float l_polylineLength = 0;
      for (int l_i = 1; l_i < p_polyline.Count; l_i++)
      {
        l_polylineLength += Vector3.Distance(p_polyline[l_i], p_polyline[l_i - 1]);
      }

      xde.unity.RaycastHit[] l_hitsPcl =
        m_scenePcl.Raycast(this.gameObject.GetInstanceID(), p_polyline.ToArray(), l_polylineLength);

      if (l_hitsPcl.Length > 0)
      {
        //Remove parabola points after hit
        while (l_hitsPcl[0].distance < l_polylineLength)
        {
          l_polylineLength -= Vector3.Distance(p_polyline[p_polyline.Count - 1], p_polyline[p_polyline.Count - 2]);
          p_polyline.RemoveAt(p_polyline.Count - 1);
        }

        p_polyline.Add(l_hitsPcl[0].point);

        //set the normal of Pad object
        Vector3 l_normal2 = new Vector3(0, 1, 0);
        p_normal = l_hitsPcl[0].normal;
        float l_angle = Vector3.Angle(p_normal, l_normal2);
        if (l_angle > 45f && l_angle < 135f)
          p_validPosition = false;
        else
          p_validPosition = true;
      }
    }

    // Parabolic motion equation, y = p0 + v0*t + 1/2at^2
    private static float ParabolicCurve(float p_p0, float p_v0, float p_a, float p_t)
    {
      return p_p0 + p_v0 * p_t + 0.5f * p_a * p_t * p_t;
    }

    // Derivative of parabolic motion equation
    private static float ParabolicCurveDeriv(float p_v0, float p_a, float p_t)
    {
      return p_v0 + p_a * p_t;
    }

    // Parabolic motion equation applied to 3 dimensions
    private static Vector3 ParabolicCurve(Vector3 p_p0, Vector3 p_v0, Vector3 p_a, float p_t)
    {
      Vector3 l_ret = new Vector3();
      for (int l_x = 0; l_x < 3; l_x++)
        l_ret[l_x] = ParabolicCurve(p_p0[l_x], p_v0[l_x], p_a[l_x], p_t);
      return l_ret;
    }

    // Parabolic motion derivative applied to 3 dimensions
    private static Vector3 ParabolicCurveDeriv(Vector3 p_v0, Vector3 p_a, float p_t)
    {
      Vector3 l_ret = new Vector3();
      for (int l_x = 0; l_x < 3; l_x++)
        l_ret[l_x] = ParabolicCurveDeriv(p_v0[l_x], p_a[l_x], p_t);
      return l_ret;
    }

    // Clamps the given velocity vector so that it can't be more than 45 degrees above the horizontal.
    // This is done so that it is easier to leverage the maximum distance (at the 45 degree angle) of
    // parabolic motion.
    /*private void ClampInitialVelocity(ref Vector3 p_velocity)
		{
		    // Project the initial velocity onto the XZ plane.  This gives us the "forward" direction
		    Vector3 l_velocityFwd = ProjectVectorOntoPlane(Vector3.up, p_velocity);
		    // Find the angle between the XZ plane and the velocity
		    float l_angle = Vector3.Angle(l_velocityFwd, p_velocity);
		    // Calculate positivity/negativity of the angle using the cross product
		    // Below is "right" from controller's perspective (could also be left, but it doesn't matter for our purposes)
		    Vector3 l_right = Vector3.Cross(Vector3.up, l_velocityFwd);
		    // If the cross product between forward and the velocity is in the same direction as right, then we are below the vertical
		    if (Vector3.Dot(l_right, Vector3.Cross(l_velocityFwd, p_velocity)) > 0)
		        l_angle *= -1;
		    // Clamp the angle if it is greater than 45 degrees
		    if (l_angle > 45)
		    {
		        p_velocity = Vector3.Slerp(l_velocityFwd, p_velocity, 45f / l_angle);
		        p_velocity /= p_velocity.magnitude;
		        p_velocity *= m_initialVelocity.magnitude;
		    }
		}

		/*private static Vector3 ProjectVectorOntoPlane(Vector3 p_planeNormal, Vector3 p_point)
		{
		    Vector3 l_d = Vector3.Project(p_point, p_planeNormal.normalized);
		    return p_point - l_d;
		}*/

    private void GenerateParabolaMesh(ref Mesh p_m, List<Vector3> p_points, Vector3 p_fwd, float p_uvoffset)
		{
			Vector3[] l_verts = new Vector3[p_points.Count * 2];
			Vector2[] l_uv = new Vector2[p_points.Count * 2];
			Vector3 l_right = Vector3.Cross(p_fwd, Vector3.up).normalized;
			for (int l_x = 0; l_x < p_points.Count; l_x++)
			{
				l_verts[2 * l_x] = p_points[l_x] - l_right * GraphicThickness / 2;
				l_verts[2 * l_x + 1] = p_points[l_x] + l_right * GraphicThickness / 2;
				float l_uvoffsetMod = p_uvoffset;
				if (l_x == p_points.Count - 1 && l_x > 1)
				{
					float l_distLast = (p_points[l_x - 2] - p_points[l_x - 1]).magnitude;
					float l_distCur = (p_points[l_x] - p_points[l_x - 1]).magnitude;
					l_uvoffsetMod += 1 - l_distCur / l_distLast;
				}

				l_uv[2 * l_x] = new Vector2(0, l_x - l_uvoffsetMod);
				l_uv[2 * l_x + 1] = new Vector2(1, l_x - l_uvoffsetMod);
			}

			int[] l_indices = new int[2 * 3 * (l_verts.Length - 2)];
			for (int l_x = 0; l_x < l_verts.Length / 2 - 1; l_x++)
			{
				int l_p1 = 2 * l_x;
				int l_p2 = 2 * l_x + 1;
				int l_p3 = 2 * l_x + 2;
				int l_p4 = 2 * l_x + 3;
				l_indices[12 * l_x] = l_p1;
				l_indices[12 * l_x + 1] = l_p2;
				l_indices[12 * l_x + 2] = l_p3;
				l_indices[12 * l_x + 3] = l_p3;
				l_indices[12 * l_x + 4] = l_p2;
				l_indices[12 * l_x + 5] = l_p4;
				l_indices[12 * l_x + 6] = l_p3;
				l_indices[12 * l_x + 7] = l_p2;
				l_indices[12 * l_x + 8] = l_p1;
				l_indices[12 * l_x + 9] = l_p4;
				l_indices[12 * l_x + 10] = l_p2;
				l_indices[12 * l_x + 11] = l_p3;
			}

			p_m.Clear();
			p_m.vertices = l_verts;
			p_m.uv = l_uv;
			p_m.triangles = l_indices;
			p_m.RecalculateBounds();
			p_m.RecalculateNormals();
		}

		private void OnDisable()
		{
			if (m_scenePcl != null)
				m_scenePcl.StopServer();


			m_validTeleportPad.SetActive(false);
			m_invalidTeleportPad.SetActive(false);
			m_parabolaPoints.Clear();

		}

		void OnDestroy()
		{
			Destroy(m_validTeleportPad);
			Destroy(m_invalidTeleportPad);
			Destroy(m_parabolaMaterial);
		}
	}
}