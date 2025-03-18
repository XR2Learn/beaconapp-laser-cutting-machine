//======= Copyright (c) EIT Manufacturing - VME ===============
//
// Script realize by Light & Shadows
//
//=============================================================================

using UnityEngine;
using UnityEngine.Serialization;
using XdeNetwork;
using XdeEngine.Hand;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Animator))]
// ReSharper disable once InconsistentNaming
public class AvatarIKPerso : MonoBehaviour
{
    protected Animator m_avatar;
    private PlayerInfo m_playerInfo;

    //Rotation offset between hand bone and IK hand goal (calculate with Quaternion.Inverse(avatar.GetBoneTransform(HumanBodyBones.LeftHand).rotation) * avatar.GetIKRotation(AvatarIKGoal.LeftHand))
    private readonly Vector3 m_handRotOffsetIkRight = new Vector3(-4.3f, -8.6f, -11.5f);
    private readonly Vector3 m_handRotOffsetIkLeft = new Vector3(-5.0f, 5.7f, 10.9f);
    
    private HandTracking m_handTracking;
    //Rotation and position offsets between XDE hand model and Manikin hand model
    private readonly Vector3 m_handPosOffsetXdeRight = new Vector3(-0.061f, -0.012f, -0.004f);
    private readonly Vector3 m_handRotOffsetXdeRight = new Vector3(-11.2f, 86.8f, 7.2f);
    private readonly Vector3 m_handPosOffsetXdeManusRight = new Vector3(0.03f, -0.03f, -0.01f);
    private readonly Vector3 m_handPosOffsetXdeLeft = new Vector3(-0.064f, -0.007f, 0.001f);
    private readonly Vector3 m_handRotOffsetXdeLeft = new Vector3(-9.2f, -267.2f, -9.35f);
    private readonly Vector3 m_handPosOffsetXdeManusLeft = new Vector3(0.03f, -0.03f, 0.005f);
    private Vector3 m_handPosOffsetControllerRight = new Vector3(0.04f, 0.0f, -0.14f);
    private Vector3 m_handRotOffsetControllerRight = new Vector3(25.0f, 0.0f, -70.0f);
    private Vector3 m_handPosOffsetControllerLeft = new Vector3(-0.04f, 0.0f, -0.14f);
    private Vector3 m_handRotOffsetControllerLeft = new Vector3(25.0f, 0.0f, 70.0f);

    private float armLength = 0.8f;
    private Vector3 m_previousHeadPos;
    private Vector3 m_previousHeadRot;
    private Vector3 m_playerTransSpeedAvg = Vector3.zero;
    private float m_playerRotSpeedAvg = 0;
    private Vector3 m_currentPlayerTransSpeed;
    private float m_currentPlayerRotSpeed;
    private int m_count = 0;
    private bool m_isStanding = true;
    private float m_forwardSpeed = 0;
    private float m_strafeSpeed = 0;
    private float m_useRightHandIk = 1;
    private float m_useLeftHandIk = 1;
    private float m_useFootIk = 1;
    private bool m_rightHandAttached = false;
    private bool m_leftHandAttached = false;

    [Header("Avatar Controllers")]
    [FormerlySerializedAs("HeadPosition")]
    public Transform m_headPosition;
    private Transform m_manikinGaze;
    [FormerlySerializedAs("rightLeapHand")]
    public Transform m_rightLeapHand;
    [FormerlySerializedAs("leftLeapHand")]    
    public Transform m_leftLeapHand;
    [FormerlySerializedAs("rightController")]
    public Transform m_rightController;
    [FormerlySerializedAs("leftController")]
    public Transform m_leftController;
    [FormerlySerializedAs("rightHandRestPos")]
    public Transform m_rightHandRestPos;
    [FormerlySerializedAs("leftHandRestPos")]
    public Transform m_leftHandRestPos;

    [FormerlySerializedAs("leftRaycastOrigin")]
    public Transform m_leftRaycastOrigin;
    [FormerlySerializedAs("RightRaycastOrigin")]
    public Transform m_RightRaycastOrigin;

    private Transform m_rightHandPalm;
    private Transform m_leftHandPalm;
    private XdeHandSkinning m_rightHandSkinner;
    private XdeHandSkinning m_leftHandSkinner;

    private bool isUpdatingHands = false;

    [Header("Inverse Kinematics Targets")]
    [FormerlySerializedAs("bodyTarget")]    
    public Transform m_bodyTarget;

    [FormerlySerializedAs("lookAtTarget")]
    public Transform m_lookAtTarget;

    [FormerlySerializedAs("leftFootTarget")]
    public Transform m_leftFootTarget;

    [FormerlySerializedAs("rightFootTarget")]
    public Transform m_rightFootTarget;

    [FormerlySerializedAs("leftHandTarget")]
    public Transform m_leftHandTarget;

    [FormerlySerializedAs("rightHandTarget")]
    public Transform m_rightHandTarget;

    private static readonly int IsStanding = Animator.StringToHash("isStanding");
    private static readonly int ForwardSpeed = Animator.StringToHash("ForwardSpeed");
    private static readonly int StrafeSpeed = Animator.StringToHash("StrafeSpeed");
    private static readonly int RotationSpeed = Animator.StringToHash("RotationSpeed");

    private float m_Playerheight = 1.7f;
    /*[Inject]
    XdeEngine.Hand.Device.LeapMotionManager m_leapManager = null;*/

    // Use this for initialization
    void Start()
    {
        m_avatar = GetComponent<Animator>();
        m_playerInfo = this.transform.parent.GetComponent<PlayerInfo>();
        m_manikinGaze = this.transform.Find("Hips/Spine1/Spine2/Ribcage/Neck/Head/ManikinGaze");
        m_handTracking = m_playerInfo.m_handTracking;

        if (m_playerInfo != null && m_playerInfo.m_gender == GenderPlayer.FEMALE)
        {
            m_handPosOffsetControllerRight = new Vector3(0.03f, 0.05f, -0.12f);
            m_handRotOffsetControllerRight = new Vector3(70.0f, 4.5f, -70f);
            m_handPosOffsetControllerLeft = new Vector3(-0.04f, 0.05f, -0.12f);
            m_handRotOffsetControllerLeft = new Vector3(70.0f, -4.5f, 70.0f);
        }

        m_avatar.logWarnings = false;
        m_avatar.SetBool(IsStanding, false);
        m_avatar.SetFloat(ForwardSpeed, 0.0f);
        m_avatar.SetFloat(StrafeSpeed, 0.0f);

        if (m_rightLeapHand != null && m_leftLeapHand != null)
        {
            m_rightHandPalm = m_rightLeapHand.Find("palm");
            m_leftHandPalm = m_leftLeapHand.Find("palm");

            m_rightHandSkinner = m_rightLeapHand.GetComponent<XdeHandSkinning>();
            m_leftHandSkinner = m_leftLeapHand.GetComponent<XdeHandSkinning>();
        }
        
        m_Playerheight = m_manikinGaze.position.y;
        isUpdatingHands = true;
    }

    void Update()
    {
        /**************************** BODY IK ******************************/
        Vector3 l_position = m_rightFootTarget.position;
        Vector3 l_position1 = m_leftFootTarget.position;
        Transform l_parent = transform.parent; 
        Vector3 l_position2 = l_parent.position; //Position of the avatar

        l_position = new Vector3(l_position.x, l_position2.y + 0.1f, l_position.z);
        m_rightFootTarget.position = l_position;
        l_position1 = new Vector3(l_position1.x, l_position2.y + 0.1f, l_position1.z);
        m_leftFootTarget.position = l_position1;

        /**************************** HANDS ******************************/
        switch (m_handTracking) //Switch for handtracking method (leap/manus/controller)
        {
            case HandTracking.LEAPMOTION:
                UpdateHand(m_rightHandTarget, false, m_handPosOffsetXdeRight, m_handRotOffsetXdeRight, false);
                UpdateHand(m_leftHandTarget, true, m_handPosOffsetXdeLeft, m_handRotOffsetXdeLeft, false);
                break;

            case HandTracking.MANUS:
                UpdateHand(m_rightHandTarget, false, m_handPosOffsetXdeManusRight, m_handRotOffsetXdeRight, false);
                UpdateHand(m_leftHandTarget, true, m_handPosOffsetXdeManusLeft, m_handRotOffsetXdeLeft, false);
                break;

            case HandTracking.HANDCONTROLLER:
                UpdateHand(m_rightHandTarget, false, m_handPosOffsetXdeRight, m_handRotOffsetXdeRight, false);
                UpdateHand(m_leftHandTarget, true, m_handPosOffsetXdeLeft, m_handRotOffsetXdeLeft, false);
                break;

            case HandTracking.NONE:
                // Right Hand
                if (Vector3.Distance(m_headPosition.position, m_rightController.position) < armLength)   //Attach hand to controller if controller is close enough 
                {
                    UpdateHand(m_rightHandTarget, false, m_handPosOffsetControllerRight, m_handRotOffsetControllerRight, true);
                }
                else
                {
                    m_rightHandTarget.position = m_rightHandRestPos.position;
                    m_rightHandTarget.rotation = m_rightHandRestPos.rotation;
                    m_rightHandAttached = false;
                }

                // Left Hand
                if (Vector3.Distance(m_headPosition.position, m_leftController.position) < armLength)   //Attach hand to controller if controller is close enough 
                {
                    UpdateHand(m_leftHandTarget, true, m_handPosOffsetControllerLeft, m_handRotOffsetControllerLeft, true);
                }
                else
                {
                    m_leftHandTarget.position = m_leftHandRestPos.position;
                    m_leftHandTarget.rotation = m_leftHandRestPos.rotation;
                    m_leftHandAttached = false;
                }
                break;
        }

        //Calculating player speed (moving average over 10 samples)
        Vector3 l_forwardGazeProjectiontoXz = Vector3.Normalize(Vector3.ProjectOnPlane(m_headPosition.forward, Vector3.up));
        Vector3 l_rightGazeProjectiontoXz = Vector3.Normalize(Vector3.ProjectOnPlane(m_headPosition.right, Vector3.up));

        m_currentPlayerTransSpeed = (m_headPosition.localPosition - m_previousHeadPos) / Time.deltaTime;
        m_currentPlayerRotSpeed = Vector3.SignedAngle(l_forwardGazeProjectiontoXz, m_previousHeadRot, Vector3.up) / Time.deltaTime;

        m_count++;
        if (m_count > 10)
        {
            m_playerTransSpeedAvg += (m_currentPlayerTransSpeed - m_playerTransSpeedAvg) / 11;
            m_playerRotSpeedAvg += (m_currentPlayerRotSpeed - m_playerRotSpeedAvg) / 11;
        }
        else
        {
            m_playerTransSpeedAvg += m_currentPlayerTransSpeed;
            m_playerRotSpeedAvg += m_currentPlayerRotSpeed;
            if (m_count == 10)
            {
                m_playerTransSpeedAvg += m_currentPlayerTransSpeed / 10;
                m_playerRotSpeedAvg += m_currentPlayerRotSpeed / 10;
            }
        }

        Vector3 l_playerSpeedProjectiontoXz = Vector3.ProjectOnPlane(m_playerTransSpeedAvg, Vector3.up);
        m_forwardSpeed = Vector3.Dot(l_playerSpeedProjectiontoXz, l_forwardGazeProjectiontoXz);
        m_strafeSpeed = Vector3.Dot(l_playerSpeedProjectiontoXz, l_rightGazeProjectiontoXz);

        //Debug.Log(ForwardSpeed + " " + StrafeSpeed + " / " + playerRotSpeedAvg);

        Vector3 l_localPosition = m_headPosition.localPosition;
        //m_isStanding = l_localPosition.y > 1.60f * this.transform.localScale.x;
        m_isStanding = l_localPosition.y > m_Playerheight-0.2f;
        

        m_avatar.SetBool(IsStanding, m_isStanding);
        m_avatar.SetFloat(ForwardSpeed, m_forwardSpeed);
        m_avatar.SetFloat(StrafeSpeed, m_strafeSpeed);
        m_avatar.SetFloat(RotationSpeed, -m_playerRotSpeedAvg);

        m_useFootIk = (m_isStanding ? 0 : 1);
        m_useRightHandIk = (m_rightHandAttached ? 1 : 0);
        m_useLeftHandIk = (m_leftHandAttached ? 1 : 0);

        m_previousHeadPos = l_localPosition;
        m_previousHeadRot = l_forwardGazeProjectiontoXz;

        if (!m_leftHandSkinner && m_leftController.transform.parent.gameObject.active)
        {
            m_leftLeapHand.transform.position = m_leftController.transform.position;
            m_leftLeapHand.transform.rotation = m_leftController.transform.rotation;
        }
        if (!m_rightHandSkinner && m_rightController.transform.parent.gameObject.active)
        {
            m_rightLeapHand.transform.position = m_rightController.transform.position;
            m_rightLeapHand.transform.rotation = m_rightController.transform.rotation;
        }

        if (isUpdatingHands)
        {
            if (m_leftRaycastOrigin)
            {
                m_leftRaycastOrigin.transform.position = m_leftController.transform.position;
                m_leftRaycastOrigin.transform.rotation = m_leftController.transform.rotation;
            }
            if (m_RightRaycastOrigin)
            {
                m_RightRaycastOrigin.transform.position = m_rightController.transform.position;
                m_RightRaycastOrigin.transform.rotation = m_rightController.transform.rotation;
            }
        }
    }

    void UpdateHand(Transform p_handTarget, bool p_isLeftHand, Vector3 p_positionOffset, Vector3 p_rotationOffset, bool p_useController)
    {
        if (p_isLeftHand)
        {
            Transform l_reference = (p_useController) ? m_leftController : m_leftHandPalm;

            if (l_reference != null)
            {
                p_handTarget.position = l_reference.position;
                p_handTarget.rotation = l_reference.rotation;
                m_leftHandTarget.Translate(p_positionOffset, Space.Self);
                m_leftHandTarget.Rotate(p_rotationOffset, Space.Self);
                m_leftHandAttached = true;
            }
        }
        else
        {
            Transform l_reference = (p_useController) ? m_rightController : m_rightHandPalm;

            if (l_reference != null)
            {
                p_handTarget.position = l_reference.position;
                p_handTarget.rotation = l_reference.rotation;
                m_rightHandTarget.Translate(p_positionOffset, Space.Self);
                m_rightHandTarget.Rotate(p_rotationOffset, Space.Self);
                m_rightHandAttached = true;
            }
        }
    }

    void OnAnimatorIK(int p_layerIndex)
    {
        //Set look position 1m in front of camera
        m_lookAtTarget.position = m_headPosition.position + m_headPosition.forward * 1.0f;

        //Body position
        m_avatar.bodyPosition = m_bodyTarget.position;
        m_avatar.bodyRotation = m_bodyTarget.rotation;

        //Foot IK
        m_avatar.SetIKPosition(AvatarIKGoal.LeftFoot, m_leftFootTarget.position);
        m_avatar.SetIKRotation(AvatarIKGoal.LeftFoot, m_leftFootTarget.rotation);
        m_avatar.SetIKPosition(AvatarIKGoal.RightFoot, m_rightFootTarget.position);
        m_avatar.SetIKRotation(AvatarIKGoal.RightFoot, m_rightFootTarget.rotation);

        //Hand IK
        m_avatar.SetIKPosition(AvatarIKGoal.LeftHand, m_leftHandTarget.position);
        m_avatar.SetIKRotation(AvatarIKGoal.LeftHand, m_leftHandTarget.rotation * Quaternion.Euler(m_handRotOffsetIkLeft));
        m_avatar.SetIKPosition(AvatarIKGoal.RightHand, m_rightHandTarget.position);
        m_avatar.SetIKRotation(AvatarIKGoal.RightHand, m_rightHandTarget.rotation * Quaternion.Euler(m_handRotOffsetIkRight));

        //Look target
        m_avatar.SetLookAtPosition(m_lookAtTarget.position);

        //Send joints to IK targets
        m_avatar.SetLookAtWeight(1, 0.4f, 1.0f, 0.0f, 1.0f);
        m_avatar.SetIKPositionWeight(AvatarIKGoal.LeftFoot, m_useFootIk);
        m_avatar.SetIKRotationWeight(AvatarIKGoal.LeftFoot, m_useFootIk);
        m_avatar.SetIKPositionWeight(AvatarIKGoal.RightFoot, m_useFootIk);
        m_avatar.SetIKRotationWeight(AvatarIKGoal.RightFoot, m_useFootIk);
        m_avatar.SetIKPositionWeight(AvatarIKGoal.LeftHand, m_useLeftHandIk);
        m_avatar.SetIKRotationWeight(AvatarIKGoal.LeftHand, m_useLeftHandIk);
        m_avatar.SetIKPositionWeight(AvatarIKGoal.RightHand, m_useRightHandIk);
        m_avatar.SetIKRotationWeight(AvatarIKGoal.RightHand, m_useRightHandIk);

    }

    void LateUpdate()
    {
        Quaternion l_rotation1 = m_headPosition.rotation;
        Transform l_transform = transform;
        Quaternion l_rotation = l_transform.rotation;
        m_avatar.GetBoneTransform(HumanBodyBones.Neck).localEulerAngles += new Vector3(0.0f, (l_rotation1.eulerAngles.z - m_manikinGaze.rotation.eulerAngles.z), 0.0f);
        l_transform.position += (m_headPosition.position - m_manikinGaze.position);
        l_rotation = Quaternion.Euler(l_rotation.eulerAngles.x, l_rotation1.eulerAngles.y, l_rotation.eulerAngles.z);
        transform.rotation = l_rotation;

        //Move shoulder if target IK is not within reach
        if (m_rightHandAttached)
            m_avatar.GetBoneTransform(HumanBodyBones.RightShoulder).Translate(m_avatar.GetIKPosition(AvatarIKGoal.RightHand) - m_avatar.GetBoneTransform(HumanBodyBones.RightHand).position, Space.World);

        if (m_leftHandAttached)
            m_avatar.GetBoneTransform(HumanBodyBones.LeftShoulder).Translate(m_avatar.GetIKPosition(AvatarIKGoal.LeftHand) - m_avatar.GetBoneTransform(HumanBodyBones.LeftHand).position, Space.World);
        
        if (m_rightLeapHand != null && m_leftLeapHand != null)
        {
            if (m_rightHandSkinner)
                m_rightHandSkinner.UpdateSkinBones();
            if (m_leftHandSkinner)
                m_leftHandSkinner.UpdateSkinBones();
        }
        
    }
/*
    [XdeRPC]
    public void CalibrateAvatar()
    {
        Transform l_transform = transform;
        Transform l_parent = l_transform.parent;
        float l_scaleFactor = (m_headPosition.position.y - l_parent.position.y )/ 1.6875f;
        l_transform.localScale = new Vector3(l_scaleFactor, l_scaleFactor, l_scaleFactor);

        //Keep size of XDE Hand
        m_avatar.GetBoneTransform(HumanBodyBones.RightHand).localScale = 0.9f * 1 / l_scaleFactor * Vector3.one;
        m_avatar.GetBoneTransform(HumanBodyBones.LeftHand).localScale = 0.9f * 1 / l_scaleFactor * Vector3.one;

        Debug.Log("Calibrated " + l_parent.name);

    }*/
}