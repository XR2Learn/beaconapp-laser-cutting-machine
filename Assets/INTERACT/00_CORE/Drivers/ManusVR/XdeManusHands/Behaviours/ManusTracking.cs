using System;
using ManusVR.Core.Apollo;
using ManusVR.Core.Hands;
using ManusVR.Core.Tracking;
using ManusVR.Core.Utilities;
using UnityEngine;

namespace INTERACT.CORE.Drivers
{
	
/// <summary>
/// This scripts attach a TransformFollow to hands to make them follow vive trackers
/// Had to create this script in oder to not edit Manus scripts and to fix bugs on offset not taken in account.
/// </summary>
///

	public class ManusTracking : MonoBehaviour
	{
		[SerializeField]
		private TrackingManager.TrackerLocation m_location;

		[SerializeField]
		private XdeManusVRManager m_vrManager;

		[Header("Hand transform")]
		[SerializeField]
		private Transform m_rightLowerArm;

		[SerializeField]
		private Transform m_leftLowerArm;

		[Header("Custom right offsets")]
		[SerializeField]
		private Vector3 m_rightPositionOffset;

		[SerializeField]
		private Vector3 m_rightRotationOffset;

		[Header("Custom left offsets")]
		[SerializeField]
		private Vector3 m_leftPositionOffset;

		[SerializeField]
		private Vector3 m_leftRotationOffset;

		private Transform m_rightHandTracker;
		private Transform m_leftHandTracker;

		private void Start()
		{
			var l_handController = GetComponent<HandController>();
			if (l_handController == null) return;
			m_rightLowerArm = m_rightLowerArm != null ? m_rightLowerArm : GameObject.Find("hand_r").transform;
			m_leftLowerArm = m_leftLowerArm != null ? m_leftLowerArm : GameObject.Find("hand_l").transform;
			m_rightHandTracker = m_vrManager.RightTargetTracker;
			m_leftHandTracker = m_vrManager.LeftTargetTracker;
			foreach (device_type_t deviceType in Enum.GetValues(typeof(device_type_t)))
			{
				l_handController.Hands.TryGetValue(deviceType, out Hand hand);

				if (hand == null)
				{
					Debug.LogError("Hand not found!");
					continue;
				}

				if (m_location == TrackingManager.TrackerLocation.Hand)
				{
					if (deviceType == device_type_t.GLOVE_LEFT)
					{
						AddTransformFollow(m_leftLowerArm, m_leftHandTracker, m_leftPositionOffset, m_leftRotationOffset);
					}
					else
					{
						AddTransformFollow(m_rightLowerArm, m_rightHandTracker, m_rightPositionOffset, m_rightRotationOffset);
					}
				}
			}
		}

		private void AddTransformFollow(Transform p_arm, Transform p_tracker, Vector3 p_positionOffset, Vector3 p_rotationOffset)
		{
			TransformFollow l_follow = p_arm.gameObject.AddComponent<TransformFollow>();

			l_follow.transformToMove = p_arm;
			l_follow.transformToFollow = p_tracker;
			l_follow.positionOffset = new Vector3(p_positionOffset.x, p_positionOffset.y, p_positionOffset.z);
			l_follow.rotationOffset = p_rotationOffset;
		}
	}
}