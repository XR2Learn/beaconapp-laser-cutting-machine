// ==========================
// GENERATED CODE DO NOT EDIT
// genererated at 2021-08-03 18:00:45.807000
// from template file 'cs/xde_types_uwp_cs.tpl'
// ===========================

#if ENABLE_IL2CPP

// https://docs.unity3d.com/Manual/ScriptingRestrictions.html 
// To work around an AOT issue like this, we force the compiler to generate the proper code.
namespace XdeEngine.IL2CPP
{
  class BasicTypes
  {
  
    // This force AOT computation
    static void ForceRegistrationOfMethod()
    {

      // templates for Subscriber, only for UWP 
      // all the type of arguments passed in XdeRPC methods must be listed below
      { xsm.wamp.Subscriber.GetArg<byte>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<bool>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<int>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<uint>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<float>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<double>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<string>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<UnityEngine.Vector3>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<xde.unity.math.Vector3d>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<xde.unity.math.Vector2d>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<UnityEngine.Vector2>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<UnityEngine.Quaternion>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<xde.unity.math.Quaterniond>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<xde.unity.math.Displacement>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<xde.unity.math.Displacementd>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<xde_types.NormalizedDisplacement>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<xde.unity.math.Twist>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<xde.unity.math.Wrench>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<long>(new xsm.wamp.ParsedMessage(), 0); }
      { xsm.wamp.Subscriber.GetArg<byte[]>(new xsm.wamp.ParsedMessage(), 0); }

      // templates for XdeSyncVarHolder
      { XdeNetwork.XdeSyncVarHolder<byte> tmp = new XdeNetwork.XdeSyncVarHolder<byte>(); }
      { XdeNetwork.XdeSyncVarHolder<bool> tmp = new XdeNetwork.XdeSyncVarHolder<bool>(); }
      { XdeNetwork.XdeSyncVarHolder<int> tmp = new XdeNetwork.XdeSyncVarHolder<int>(); }
      { XdeNetwork.XdeSyncVarHolder<uint> tmp = new XdeNetwork.XdeSyncVarHolder<uint>(); }
      { XdeNetwork.XdeSyncVarHolder<float> tmp = new XdeNetwork.XdeSyncVarHolder<float>(); }
      { XdeNetwork.XdeSyncVarHolder<double> tmp = new XdeNetwork.XdeSyncVarHolder<double>(); }
      { XdeNetwork.XdeSyncVarHolder<string> tmp = new XdeNetwork.XdeSyncVarHolder<string>(); }
      { XdeNetwork.XdeSyncVarHolder<UnityEngine.Vector3> tmp = new XdeNetwork.XdeSyncVarHolder<UnityEngine.Vector3>(); }
      { XdeNetwork.XdeSyncVarHolder<xde.unity.math.Vector3d> tmp = new XdeNetwork.XdeSyncVarHolder<xde.unity.math.Vector3d>(); }
      { XdeNetwork.XdeSyncVarHolder<xde.unity.math.Vector2d> tmp = new XdeNetwork.XdeSyncVarHolder<xde.unity.math.Vector2d>(); }
      { XdeNetwork.XdeSyncVarHolder<UnityEngine.Vector2> tmp = new XdeNetwork.XdeSyncVarHolder<UnityEngine.Vector2>(); }
      { XdeNetwork.XdeSyncVarHolder<UnityEngine.Quaternion> tmp = new XdeNetwork.XdeSyncVarHolder<UnityEngine.Quaternion>(); }
      { XdeNetwork.XdeSyncVarHolder<xde.unity.math.Quaterniond> tmp = new XdeNetwork.XdeSyncVarHolder<xde.unity.math.Quaterniond>(); }
      { XdeNetwork.XdeSyncVarHolder<xde.unity.math.Displacement> tmp = new XdeNetwork.XdeSyncVarHolder<xde.unity.math.Displacement>(); }
      { XdeNetwork.XdeSyncVarHolder<xde.unity.math.Displacementd> tmp = new XdeNetwork.XdeSyncVarHolder<xde.unity.math.Displacementd>(); }
      { XdeNetwork.XdeSyncVarHolder<xde_types.NormalizedDisplacement> tmp = new XdeNetwork.XdeSyncVarHolder<xde_types.NormalizedDisplacement>(); }
      { XdeNetwork.XdeSyncVarHolder<xde.unity.math.Twist> tmp = new XdeNetwork.XdeSyncVarHolder<xde.unity.math.Twist>(); }
      { XdeNetwork.XdeSyncVarHolder<xde.unity.math.Wrench> tmp = new XdeNetwork.XdeSyncVarHolder<xde.unity.math.Wrench>(); }
      { XdeNetwork.XdeSyncVarHolder<long> tmp = new XdeNetwork.XdeSyncVarHolder<long>(); }
      { XdeNetwork.XdeSyncVarHolder<byte[]> tmp = new XdeNetwork.XdeSyncVarHolder<byte[]>(); }
    }

    // Forces the registration of composite types 
    static void ForceRegistrationOfGeneratedTypes()
    {
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      // xde_types.xde_leap_bones
      {
        xde_types.leap_bones elmt = xde_types.xde_leap_bones.default_value();
        bool tmp = xde_types.xde_leap_bones.fixedSize;
        xde_types.xde_leap_bones.size(ref elmt);
        xde_types.xde_leap_bones.serialize(bts, elmt, 0);
        xde_types.xde_leap_bones.deserialize(bts, ref elmt, 0);
        xde_types.xde_leap_bones.Write(elmt, str);
        xde_types.xde_leap_bones.Read(str);
      }
      // xde_types.xde_leap_hands
      {
        xde_types.leap_hands elmt = xde_types.xde_leap_hands.default_value();
        bool tmp = xde_types.xde_leap_hands.fixedSize;
        xde_types.xde_leap_hands.size(ref elmt);
        xde_types.xde_leap_hands.serialize(bts, elmt, 0);
        xde_types.xde_leap_hands.deserialize(bts, ref elmt, 0);
        xde_types.xde_leap_hands.Write(elmt, str);
        xde_types.xde_leap_hands.Read(str);
      }
      // xde_types.xde_kinect_joints
      {
        xde_types.kinect_joints elmt = xde_types.xde_kinect_joints.default_value();
        bool tmp = xde_types.xde_kinect_joints.fixedSize;
        xde_types.xde_kinect_joints.size(ref elmt);
        xde_types.xde_kinect_joints.serialize(bts, elmt, 0);
        xde_types.xde_kinect_joints.deserialize(bts, ref elmt, 0);
        xde_types.xde_kinect_joints.Write(elmt, str);
        xde_types.xde_kinect_joints.Read(str);
      }
      // xde_types.xde_record_mode
      {
        xde_types.record_mode elmt = xde_types.xde_record_mode.default_value();
        bool tmp = xde_types.xde_record_mode.fixedSize;
        xde_types.xde_record_mode.size(ref elmt);
        xde_types.xde_record_mode.serialize(bts, elmt, 0);
        xde_types.xde_record_mode.deserialize(bts, ref elmt, 0);
        xde_types.xde_record_mode.Write(elmt, str);
        xde_types.xde_record_mode.Read(str);
      }
      // xde_types.xde_request_type
      {
        xde_types.request_type elmt = xde_types.xde_request_type.default_value();
        bool tmp = xde_types.xde_request_type.fixedSize;
        xde_types.xde_request_type.size(ref elmt);
        xde_types.xde_request_type.serialize(bts, elmt, 0);
        xde_types.xde_request_type.deserialize(bts, ref elmt, 0);
        xde_types.xde_request_type.Write(elmt, str);
        xde_types.xde_request_type.Read(str);
      }
      // xde_types.xde_lvc_command
      {
        xde_types.lvc_command elmt = xde_types.xde_lvc_command.default_value();
        bool tmp = xde_types.xde_lvc_command.fixedSize;
        xde_types.xde_lvc_command.size(ref elmt);
        xde_types.xde_lvc_command.serialize(bts, elmt, 0);
        xde_types.xde_lvc_command.deserialize(bts, ref elmt, 0);
        xde_types.xde_lvc_command.Write(elmt, str);
        xde_types.xde_lvc_command.Read(str);
      }
      // xde_types.xde_request_status
      {
        xde_types.request_status elmt = xde_types.xde_request_status.default_value();
        bool tmp = xde_types.xde_request_status.fixedSize;
        xde_types.xde_request_status.size(ref elmt);
        xde_types.xde_request_status.serialize(bts, elmt, 0);
        xde_types.xde_request_status.deserialize(bts, ref elmt, 0);
        xde_types.xde_request_status.Write(elmt, str);
        xde_types.xde_request_status.Read(str);
      }
      // xde_types.xde_response_type
      {
        xde_types.response_type elmt = xde_types.xde_response_type.default_value();
        bool tmp = xde_types.xde_response_type.fixedSize;
        xde_types.xde_response_type.size(ref elmt);
        xde_types.xde_response_type.serialize(bts, elmt, 0);
        xde_types.xde_response_type.deserialize(bts, ref elmt, 0);
        xde_types.xde_response_type.Write(elmt, str);
        xde_types.xde_response_type.Read(str);
      }
      // xde_types.xde_simulation_state
      {
        xde_types.simulation_state elmt = xde_types.xde_simulation_state.default_value();
        bool tmp = xde_types.xde_simulation_state.fixedSize;
        xde_types.xde_simulation_state.size(ref elmt);
        xde_types.xde_simulation_state.serialize(bts, elmt, 0);
        xde_types.xde_simulation_state.deserialize(bts, ref elmt, 0);
        xde_types.xde_simulation_state.Write(elmt, str);
        xde_types.xde_simulation_state.Read(str);
      }
      // xde_types.xde_k4a_joints
      {
        xde_types.k4a_joints elmt = xde_types.xde_k4a_joints.default_value();
        bool tmp = xde_types.xde_k4a_joints.fixedSize;
        xde_types.xde_k4a_joints.size(ref elmt);
        xde_types.xde_k4a_joints.serialize(bts, elmt, 0);
        xde_types.xde_k4a_joints.deserialize(bts, ref elmt, 0);
        xde_types.xde_k4a_joints.Write(elmt, str);
        xde_types.xde_k4a_joints.Read(str);
      }
      // xde_types.xde_openvr_device
      {
        xde_types.openvr_device elmt = xde_types.xde_openvr_device.default_value();
        bool tmp = xde_types.xde_openvr_device.fixedSize;
        xde_types.xde_openvr_device.size(ref elmt);
        xde_types.xde_openvr_device.serialize(bts, elmt, 0);
        xde_types.xde_openvr_device.deserialize(bts, ref elmt, 0);
        xde_types.xde_openvr_device.Write(elmt, str);
        xde_types.xde_openvr_device.Read(str);
      }

      
      // xde_types.xde_component_data
      {
        xde_types.component_data elmt = xde_types.xde_component_data.default_value();
        bool tmp = xde_types.xde_component_data.fixedSize;
        xde_types.xde_component_data.size(ref elmt);
        xde_types.xde_component_data.serialize(bts, elmt, 0);
        xde_types.xde_component_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_component_data.Write(elmt, str);
        xde_types.xde_component_data.Read(str);
      }
      // xde_types.xde_vector_bool
      {
        xde_types.vector_bool elmt = xde_types.xde_vector_bool.default_value();
        bool tmp = xde_types.xde_vector_bool.fixedSize;
        xde_types.xde_vector_bool.size(ref elmt);
        xde_types.xde_vector_bool.serialize(bts, elmt, 0);
        xde_types.xde_vector_bool.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_bool.Write(elmt, str);
        xde_types.xde_vector_bool.Read(str);
      }
      // xde_types.xde_vector_int
      {
        xde_types.vector_int elmt = xde_types.xde_vector_int.default_value();
        bool tmp = xde_types.xde_vector_int.fixedSize;
        xde_types.xde_vector_int.size(ref elmt);
        xde_types.xde_vector_int.serialize(bts, elmt, 0);
        xde_types.xde_vector_int.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_int.Write(elmt, str);
        xde_types.xde_vector_int.Read(str);
      }
      // xde_types.xde_vector_double
      {
        xde_types.vector_double elmt = xde_types.xde_vector_double.default_value();
        bool tmp = xde_types.xde_vector_double.fixedSize;
        xde_types.xde_vector_double.size(ref elmt);
        xde_types.xde_vector_double.serialize(bts, elmt, 0);
        xde_types.xde_vector_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_double.Write(elmt, str);
        xde_types.xde_vector_double.Read(str);
      }
      // xde_types.xde_vector_float
      {
        xde_types.vector_float elmt = xde_types.xde_vector_float.default_value();
        bool tmp = xde_types.xde_vector_float.fixedSize;
        xde_types.xde_vector_float.size(ref elmt);
        xde_types.xde_vector_float.serialize(bts, elmt, 0);
        xde_types.xde_vector_float.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_float.Write(elmt, str);
        xde_types.xde_vector_float.Read(str);
      }
      // xde_types.xde_vector_ref
      {
        xde_types.vector_ref elmt = xde_types.xde_vector_ref.default_value();
        bool tmp = xde_types.xde_vector_ref.fixedSize;
        xde_types.xde_vector_ref.size(ref elmt);
        xde_types.xde_vector_ref.serialize(bts, elmt, 0);
        xde_types.xde_vector_ref.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_ref.Write(elmt, str);
        xde_types.xde_vector_ref.Read(str);
      }
      // xde_types.xde_vector_string
      {
        xde_types.vector_string elmt = xde_types.xde_vector_string.default_value();
        bool tmp = xde_types.xde_vector_string.fixedSize;
        xde_types.xde_vector_string.size(ref elmt);
        xde_types.xde_vector_string.serialize(bts, elmt, 0);
        xde_types.xde_vector_string.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_string.Write(elmt, str);
        xde_types.xde_vector_string.Read(str);
      }
      // xde_types.xde_pair_int_Displacementd
      {
        xde_types.pair_int_Displacementd elmt = xde_types.xde_pair_int_Displacementd.default_value();
        bool tmp = xde_types.xde_pair_int_Displacementd.fixedSize;
        xde_types.xde_pair_int_Displacementd.size(ref elmt);
        xde_types.xde_pair_int_Displacementd.serialize(bts, elmt, 0);
        xde_types.xde_pair_int_Displacementd.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_int_Displacementd.Write(elmt, str);
        xde_types.xde_pair_int_Displacementd.Read(str);
      }
      // xde_types.xde_pair_int_int
      {
        xde_types.pair_int_int elmt = xde_types.xde_pair_int_int.default_value();
        bool tmp = xde_types.xde_pair_int_int.fixedSize;
        xde_types.xde_pair_int_int.size(ref elmt);
        xde_types.xde_pair_int_int.serialize(bts, elmt, 0);
        xde_types.xde_pair_int_int.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_int_int.Write(elmt, str);
        xde_types.xde_pair_int_int.Read(str);
      }
      // xde_types.xde_pair_double_double
      {
        xde_types.pair_double_double elmt = xde_types.xde_pair_double_double.default_value();
        bool tmp = xde_types.xde_pair_double_double.fixedSize;
        xde_types.xde_pair_double_double.size(ref elmt);
        xde_types.xde_pair_double_double.serialize(bts, elmt, 0);
        xde_types.xde_pair_double_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_double_double.Write(elmt, str);
        xde_types.xde_pair_double_double.Read(str);
      }
      // xde_types.xde_pair_int_double
      {
        xde_types.pair_int_double elmt = xde_types.xde_pair_int_double.default_value();
        bool tmp = xde_types.xde_pair_int_double.fixedSize;
        xde_types.xde_pair_int_double.size(ref elmt);
        xde_types.xde_pair_int_double.serialize(bts, elmt, 0);
        xde_types.xde_pair_int_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_int_double.Write(elmt, str);
        xde_types.xde_pair_int_double.Read(str);
      }
      // xde_types.xde_pair_string_displacementd
      {
        xde_types.pair_string_displacementd elmt = xde_types.xde_pair_string_displacementd.default_value();
        bool tmp = xde_types.xde_pair_string_displacementd.fixedSize;
        xde_types.xde_pair_string_displacementd.size(ref elmt);
        xde_types.xde_pair_string_displacementd.serialize(bts, elmt, 0);
        xde_types.xde_pair_string_displacementd.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_string_displacementd.Write(elmt, str);
        xde_types.xde_pair_string_displacementd.Read(str);
      }
      // xde_types.xde_pair_string_double
      {
        xde_types.pair_string_double elmt = xde_types.xde_pair_string_double.default_value();
        bool tmp = xde_types.xde_pair_string_double.fixedSize;
        xde_types.xde_pair_string_double.size(ref elmt);
        xde_types.xde_pair_string_double.serialize(bts, elmt, 0);
        xde_types.xde_pair_string_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_string_double.Write(elmt, str);
        xde_types.xde_pair_string_double.Read(str);
      }
      // xde_types.xde_pair_string_vector_double
      {
        xde_types.pair_string_vector_double elmt = xde_types.xde_pair_string_vector_double.default_value();
        bool tmp = xde_types.xde_pair_string_vector_double.fixedSize;
        xde_types.xde_pair_string_vector_double.size(ref elmt);
        xde_types.xde_pair_string_vector_double.serialize(bts, elmt, 0);
        xde_types.xde_pair_string_vector_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_string_vector_double.Write(elmt, str);
        xde_types.xde_pair_string_vector_double.Read(str);
      }
      // xde_types.xde_pair_int_string
      {
        xde_types.pair_int_string elmt = xde_types.xde_pair_int_string.default_value();
        bool tmp = xde_types.xde_pair_int_string.fixedSize;
        xde_types.xde_pair_int_string.size(ref elmt);
        xde_types.xde_pair_int_string.serialize(bts, elmt, 0);
        xde_types.xde_pair_int_string.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_int_string.Write(elmt, str);
        xde_types.xde_pair_int_string.Read(str);
      }
      // xde_types.xde_pair_ref_int
      {
        xde_types.pair_ref_int elmt = xde_types.xde_pair_ref_int.default_value();
        bool tmp = xde_types.xde_pair_ref_int.fixedSize;
        xde_types.xde_pair_ref_int.size(ref elmt);
        xde_types.xde_pair_ref_int.serialize(bts, elmt, 0);
        xde_types.xde_pair_ref_int.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_ref_int.Write(elmt, str);
        xde_types.xde_pair_ref_int.Read(str);
      }
      // xde_types.xde_pair_Wrenchd_Displacementd
      {
        xde_types.pair_Wrenchd_Displacementd elmt = xde_types.xde_pair_Wrenchd_Displacementd.default_value();
        bool tmp = xde_types.xde_pair_Wrenchd_Displacementd.fixedSize;
        xde_types.xde_pair_Wrenchd_Displacementd.size(ref elmt);
        xde_types.xde_pair_Wrenchd_Displacementd.serialize(bts, elmt, 0);
        xde_types.xde_pair_Wrenchd_Displacementd.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_Wrenchd_Displacementd.Write(elmt, str);
        xde_types.xde_pair_Wrenchd_Displacementd.Read(str);
      }
      // xde_types.xde_pair_Displacementd_Twistd
      {
        xde_types.pair_Displacementd_Twistd elmt = xde_types.xde_pair_Displacementd_Twistd.default_value();
        bool tmp = xde_types.xde_pair_Displacementd_Twistd.fixedSize;
        xde_types.xde_pair_Displacementd_Twistd.size(ref elmt);
        xde_types.xde_pair_Displacementd_Twistd.serialize(bts, elmt, 0);
        xde_types.xde_pair_Displacementd_Twistd.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_Displacementd_Twistd.Write(elmt, str);
        xde_types.xde_pair_Displacementd_Twistd.Read(str);
      }
      // xde_types.xde_pair_ref_double
      {
        xde_types.pair_ref_double elmt = xde_types.xde_pair_ref_double.default_value();
        bool tmp = xde_types.xde_pair_ref_double.fixedSize;
        xde_types.xde_pair_ref_double.size(ref elmt);
        xde_types.xde_pair_ref_double.serialize(bts, elmt, 0);
        xde_types.xde_pair_ref_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_pair_ref_double.Write(elmt, str);
        xde_types.xde_pair_ref_double.Read(str);
      }
      // xde_types.xde_vector_pair_Displacementd_Twistd
      {
        xde_types.vector_pair_Displacementd_Twistd elmt = xde_types.xde_vector_pair_Displacementd_Twistd.default_value();
        bool tmp = xde_types.xde_vector_pair_Displacementd_Twistd.fixedSize;
        xde_types.xde_vector_pair_Displacementd_Twistd.size(ref elmt);
        xde_types.xde_vector_pair_Displacementd_Twistd.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_Displacementd_Twistd.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_Displacementd_Twistd.Write(elmt, str);
        xde_types.xde_vector_pair_Displacementd_Twistd.Read(str);
      }
      // xde_types.xde_vector_pair_string_displacementd
      {
        xde_types.vector_pair_string_displacementd elmt = xde_types.xde_vector_pair_string_displacementd.default_value();
        bool tmp = xde_types.xde_vector_pair_string_displacementd.fixedSize;
        xde_types.xde_vector_pair_string_displacementd.size(ref elmt);
        xde_types.xde_vector_pair_string_displacementd.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_string_displacementd.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_string_displacementd.Write(elmt, str);
        xde_types.xde_vector_pair_string_displacementd.Read(str);
      }
      // xde_types.xde_vector_pair_string_double
      {
        xde_types.vector_pair_string_double elmt = xde_types.xde_vector_pair_string_double.default_value();
        bool tmp = xde_types.xde_vector_pair_string_double.fixedSize;
        xde_types.xde_vector_pair_string_double.size(ref elmt);
        xde_types.xde_vector_pair_string_double.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_string_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_string_double.Write(elmt, str);
        xde_types.xde_vector_pair_string_double.Read(str);
      }
      // xde_types.xde_vector_pair_string_vector_double
      {
        xde_types.vector_pair_string_vector_double elmt = xde_types.xde_vector_pair_string_vector_double.default_value();
        bool tmp = xde_types.xde_vector_pair_string_vector_double.fixedSize;
        xde_types.xde_vector_pair_string_vector_double.size(ref elmt);
        xde_types.xde_vector_pair_string_vector_double.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_string_vector_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_string_vector_double.Write(elmt, str);
        xde_types.xde_vector_pair_string_vector_double.Read(str);
      }
      // xde_types.xde_vector_pair_int_int
      {
        xde_types.vector_pair_int_int elmt = xde_types.xde_vector_pair_int_int.default_value();
        bool tmp = xde_types.xde_vector_pair_int_int.fixedSize;
        xde_types.xde_vector_pair_int_int.size(ref elmt);
        xde_types.xde_vector_pair_int_int.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_int_int.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_int_int.Write(elmt, str);
        xde_types.xde_vector_pair_int_int.Read(str);
      }
      // xde_types.xde_vector_pair_int_double
      {
        xde_types.vector_pair_int_double elmt = xde_types.xde_vector_pair_int_double.default_value();
        bool tmp = xde_types.xde_vector_pair_int_double.fixedSize;
        xde_types.xde_vector_pair_int_double.size(ref elmt);
        xde_types.xde_vector_pair_int_double.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_int_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_int_double.Write(elmt, str);
        xde_types.xde_vector_pair_int_double.Read(str);
      }
      // xde_types.xde_vector_pair_int_string
      {
        xde_types.vector_pair_int_string elmt = xde_types.xde_vector_pair_int_string.default_value();
        bool tmp = xde_types.xde_vector_pair_int_string.fixedSize;
        xde_types.xde_vector_pair_int_string.size(ref elmt);
        xde_types.xde_vector_pair_int_string.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_int_string.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_int_string.Write(elmt, str);
        xde_types.xde_vector_pair_int_string.Read(str);
      }
      // xde_types.xde_vector_pair_ref_int
      {
        xde_types.vector_pair_ref_int elmt = xde_types.xde_vector_pair_ref_int.default_value();
        bool tmp = xde_types.xde_vector_pair_ref_int.fixedSize;
        xde_types.xde_vector_pair_ref_int.size(ref elmt);
        xde_types.xde_vector_pair_ref_int.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_ref_int.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_ref_int.Write(elmt, str);
        xde_types.xde_vector_pair_ref_int.Read(str);
      }
      // xde_types.xde_vector_pair_int_Displacementd
      {
        xde_types.vector_pair_int_Displacementd elmt = xde_types.xde_vector_pair_int_Displacementd.default_value();
        bool tmp = xde_types.xde_vector_pair_int_Displacementd.fixedSize;
        xde_types.xde_vector_pair_int_Displacementd.size(ref elmt);
        xde_types.xde_vector_pair_int_Displacementd.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_int_Displacementd.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_int_Displacementd.Write(elmt, str);
        xde_types.xde_vector_pair_int_Displacementd.Read(str);
      }
      // xde_types.xde_vector_pair_Wrenchd_Displacementd
      {
        xde_types.vector_pair_Wrenchd_Displacementd elmt = xde_types.xde_vector_pair_Wrenchd_Displacementd.default_value();
        bool tmp = xde_types.xde_vector_pair_Wrenchd_Displacementd.fixedSize;
        xde_types.xde_vector_pair_Wrenchd_Displacementd.size(ref elmt);
        xde_types.xde_vector_pair_Wrenchd_Displacementd.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_Wrenchd_Displacementd.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_Wrenchd_Displacementd.Write(elmt, str);
        xde_types.xde_vector_pair_Wrenchd_Displacementd.Read(str);
      }
      // xde_types.xde_vector_pair_ref_double
      {
        xde_types.vector_pair_ref_double elmt = xde_types.xde_vector_pair_ref_double.default_value();
        bool tmp = xde_types.xde_vector_pair_ref_double.fixedSize;
        xde_types.xde_vector_pair_ref_double.size(ref elmt);
        xde_types.xde_vector_pair_ref_double.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_ref_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_ref_double.Write(elmt, str);
        xde_types.xde_vector_pair_ref_double.Read(str);
      }
      // xde_types.xde_vector_pair_double_double
      {
        xde_types.vector_pair_double_double elmt = xde_types.xde_vector_pair_double_double.default_value();
        bool tmp = xde_types.xde_vector_pair_double_double.fixedSize;
        xde_types.xde_vector_pair_double_double.size(ref elmt);
        xde_types.xde_vector_pair_double_double.serialize(bts, elmt, 0);
        xde_types.xde_vector_pair_double_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_pair_double_double.Write(elmt, str);
        xde_types.xde_vector_pair_double_double.Read(str);
      }
      // xde_types.xde_vector2i
      {
        xde_types.vector2i elmt = xde_types.xde_vector2i.default_value();
        bool tmp = xde_types.xde_vector2i.fixedSize;
        xde_types.xde_vector2i.size(ref elmt);
        xde_types.xde_vector2i.serialize(bts, elmt, 0);
        xde_types.xde_vector2i.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector2i.Write(elmt, str);
        xde_types.xde_vector2i.Read(str);
      }
      // xde_types.xde_vector3i
      {
        xde_types.vector3i elmt = xde_types.xde_vector3i.default_value();
        bool tmp = xde_types.xde_vector3i.fixedSize;
        xde_types.xde_vector3i.size(ref elmt);
        xde_types.xde_vector3i.serialize(bts, elmt, 0);
        xde_types.xde_vector3i.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector3i.Write(elmt, str);
        xde_types.xde_vector3i.Read(str);
      }
      // xde_types.xde_fixed_vector2i
      {
        xde_types.fixed_vector2i elmt = xde_types.xde_fixed_vector2i.default_value();
        bool tmp = xde_types.xde_fixed_vector2i.fixedSize;
        xde_types.xde_fixed_vector2i.size(ref elmt);
        xde_types.xde_fixed_vector2i.serialize(bts, elmt, 0);
        xde_types.xde_fixed_vector2i.deserialize(bts, ref elmt, 0);
        xde_types.xde_fixed_vector2i.Write(elmt, str);
        xde_types.xde_fixed_vector2i.Read(str);
      }
      // xde_types.xde_fixed_vector3i
      {
        xde_types.fixed_vector3i elmt = xde_types.xde_fixed_vector3i.default_value();
        bool tmp = xde_types.xde_fixed_vector3i.fixedSize;
        xde_types.xde_fixed_vector3i.size(ref elmt);
        xde_types.xde_fixed_vector3i.serialize(bts, elmt, 0);
        xde_types.xde_fixed_vector3i.deserialize(bts, ref elmt, 0);
        xde_types.xde_fixed_vector3i.Write(elmt, str);
        xde_types.xde_fixed_vector3i.Read(str);
      }
      // xde_types.xde_frame_stats
      {
        xde_types.frame_stats elmt = xde_types.xde_frame_stats.default_value();
        bool tmp = xde_types.xde_frame_stats.fixedSize;
        xde_types.xde_frame_stats.size(ref elmt);
        xde_types.xde_frame_stats.serialize(bts, elmt, 0);
        xde_types.xde_frame_stats.deserialize(bts, ref elmt, 0);
        xde_types.xde_frame_stats.Write(elmt, str);
        xde_types.xde_frame_stats.Read(str);
      }
      // xde_types.xde_integration_result
      {
        xde_types.integration_result elmt = xde_types.xde_integration_result.default_value();
        bool tmp = xde_types.xde_integration_result.fixedSize;
        xde_types.xde_integration_result.size(ref elmt);
        xde_types.xde_integration_result.serialize(bts, elmt, 0);
        xde_types.xde_integration_result.deserialize(bts, ref elmt, 0);
        xde_types.xde_integration_result.Write(elmt, str);
        xde_types.xde_integration_result.Read(str);
      }
      // xde_types.xde_mesh_batch
      {
        xde_types.mesh_batch elmt = xde_types.xde_mesh_batch.default_value();
        bool tmp = xde_types.xde_mesh_batch.fixedSize;
        xde_types.xde_mesh_batch.size(ref elmt);
        xde_types.xde_mesh_batch.serialize(bts, elmt, 0);
        xde_types.xde_mesh_batch.deserialize(bts, ref elmt, 0);
        xde_types.xde_mesh_batch.Write(elmt, str);
        xde_types.xde_mesh_batch.Read(str);
      }
      // xde_types.xde_mesh_info
      {
        xde_types.mesh_info elmt = xde_types.xde_mesh_info.default_value();
        bool tmp = xde_types.xde_mesh_info.fixedSize;
        xde_types.xde_mesh_info.size(ref elmt);
        xde_types.xde_mesh_info.serialize(bts, elmt, 0);
        xde_types.xde_mesh_info.deserialize(bts, ref elmt, 0);
        xde_types.xde_mesh_info.Write(elmt, str);
        xde_types.xde_mesh_info.Read(str);
      }
      // xde_types.xde_position_data
      {
        xde_types.position_data elmt = xde_types.xde_position_data.default_value();
        bool tmp = xde_types.xde_position_data.fixedSize;
        xde_types.xde_position_data.size(ref elmt);
        xde_types.xde_position_data.serialize(bts, elmt, 0);
        xde_types.xde_position_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_position_data.Write(elmt, str);
        xde_types.xde_position_data.Read(str);
      }
      // xde_types.xde_art_body_data
      {
        xde_types.art_body_data elmt = xde_types.xde_art_body_data.default_value();
        bool tmp = xde_types.xde_art_body_data.fixedSize;
        xde_types.xde_art_body_data.size(ref elmt);
        xde_types.xde_art_body_data.serialize(bts, elmt, 0);
        xde_types.xde_art_body_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_art_body_data.Write(elmt, str);
        xde_types.xde_art_body_data.Read(str);
      }
      // xde_types.xde_art_model_body_data
      {
        xde_types.art_model_body_data elmt = xde_types.xde_art_model_body_data.default_value();
        bool tmp = xde_types.xde_art_model_body_data.fixedSize;
        xde_types.xde_art_model_body_data.size(ref elmt);
        xde_types.xde_art_model_body_data.serialize(bts, elmt, 0);
        xde_types.xde_art_model_body_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_art_model_body_data.Write(elmt, str);
        xde_types.xde_art_model_body_data.Read(str);
      }
      // xde_types.xde_art_flystick_data
      {
        xde_types.art_flystick_data elmt = xde_types.xde_art_flystick_data.default_value();
        bool tmp = xde_types.xde_art_flystick_data.fixedSize;
        xde_types.xde_art_flystick_data.size(ref elmt);
        xde_types.xde_art_flystick_data.serialize(bts, elmt, 0);
        xde_types.xde_art_flystick_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_art_flystick_data.Write(elmt, str);
        xde_types.xde_art_flystick_data.Read(str);
      }
      // xde_types.xde_art_finger_data
      {
        xde_types.art_finger_data elmt = xde_types.xde_art_finger_data.default_value();
        bool tmp = xde_types.xde_art_finger_data.fixedSize;
        xde_types.xde_art_finger_data.size(ref elmt);
        xde_types.xde_art_finger_data.serialize(bts, elmt, 0);
        xde_types.xde_art_finger_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_art_finger_data.Write(elmt, str);
        xde_types.xde_art_finger_data.Read(str);
      }
      // xde_types.xde_art_hand_data
      {
        xde_types.art_hand_data elmt = xde_types.xde_art_hand_data.default_value();
        bool tmp = xde_types.xde_art_hand_data.fixedSize;
        xde_types.xde_art_hand_data.size(ref elmt);
        xde_types.xde_art_hand_data.serialize(bts, elmt, 0);
        xde_types.xde_art_hand_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_art_hand_data.Write(elmt, str);
        xde_types.xde_art_hand_data.Read(str);
      }
      // xde_types.xde_art_frame
      {
        xde_types.art_frame elmt = xde_types.xde_art_frame.default_value();
        bool tmp = xde_types.xde_art_frame.fixedSize;
        xde_types.xde_art_frame.size(ref elmt);
        xde_types.xde_art_frame.serialize(bts, elmt, 0);
        xde_types.xde_art_frame.deserialize(bts, ref elmt, 0);
        xde_types.xde_art_frame.Write(elmt, str);
        xde_types.xde_art_frame.Read(str);
      }
      // xde_types.xde_optitrack_body_data
      {
        xde_types.optitrack_body_data elmt = xde_types.xde_optitrack_body_data.default_value();
        bool tmp = xde_types.xde_optitrack_body_data.fixedSize;
        xde_types.xde_optitrack_body_data.size(ref elmt);
        xde_types.xde_optitrack_body_data.serialize(bts, elmt, 0);
        xde_types.xde_optitrack_body_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_optitrack_body_data.Write(elmt, str);
        xde_types.xde_optitrack_body_data.Read(str);
      }
      // xde_types.xde_optitrack_frame
      {
        xde_types.optitrack_frame elmt = xde_types.xde_optitrack_frame.default_value();
        bool tmp = xde_types.xde_optitrack_frame.fixedSize;
        xde_types.xde_optitrack_frame.size(ref elmt);
        xde_types.xde_optitrack_frame.serialize(bts, elmt, 0);
        xde_types.xde_optitrack_frame.deserialize(bts, ref elmt, 0);
        xde_types.xde_optitrack_frame.Write(elmt, str);
        xde_types.xde_optitrack_frame.Read(str);
      }
      // xde_types.xde_kinect_joint_data
      {
        xde_types.kinect_joint_data elmt = xde_types.xde_kinect_joint_data.default_value();
        bool tmp = xde_types.xde_kinect_joint_data.fixedSize;
        xde_types.xde_kinect_joint_data.size(ref elmt);
        xde_types.xde_kinect_joint_data.serialize(bts, elmt, 0);
        xde_types.xde_kinect_joint_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_kinect_joint_data.Write(elmt, str);
        xde_types.xde_kinect_joint_data.Read(str);
      }
      // xde_types.xde_kinect_body_data
      {
        xde_types.kinect_body_data elmt = xde_types.xde_kinect_body_data.default_value();
        bool tmp = xde_types.xde_kinect_body_data.fixedSize;
        xde_types.xde_kinect_body_data.size(ref elmt);
        xde_types.xde_kinect_body_data.serialize(bts, elmt, 0);
        xde_types.xde_kinect_body_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_kinect_body_data.Write(elmt, str);
        xde_types.xde_kinect_body_data.Read(str);
      }
      // xde_types.xde_kinect_frame
      {
        xde_types.kinect_frame elmt = xde_types.xde_kinect_frame.default_value();
        bool tmp = xde_types.xde_kinect_frame.fixedSize;
        xde_types.xde_kinect_frame.size(ref elmt);
        xde_types.xde_kinect_frame.serialize(bts, elmt, 0);
        xde_types.xde_kinect_frame.deserialize(bts, ref elmt, 0);
        xde_types.xde_kinect_frame.Write(elmt, str);
        xde_types.xde_kinect_frame.Read(str);
      }
      // xde_types.xde_leap_bone_data
      {
        xde_types.leap_bone_data elmt = xde_types.xde_leap_bone_data.default_value();
        bool tmp = xde_types.xde_leap_bone_data.fixedSize;
        xde_types.xde_leap_bone_data.size(ref elmt);
        xde_types.xde_leap_bone_data.serialize(bts, elmt, 0);
        xde_types.xde_leap_bone_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_leap_bone_data.Write(elmt, str);
        xde_types.xde_leap_bone_data.Read(str);
      }
      // xde_types.xde_leap_finger_data
      {
        xde_types.leap_finger_data elmt = xde_types.xde_leap_finger_data.default_value();
        bool tmp = xde_types.xde_leap_finger_data.fixedSize;
        xde_types.xde_leap_finger_data.size(ref elmt);
        xde_types.xde_leap_finger_data.serialize(bts, elmt, 0);
        xde_types.xde_leap_finger_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_leap_finger_data.Write(elmt, str);
        xde_types.xde_leap_finger_data.Read(str);
      }
      // xde_types.xde_leap_hand_data
      {
        xde_types.leap_hand_data elmt = xde_types.xde_leap_hand_data.default_value();
        bool tmp = xde_types.xde_leap_hand_data.fixedSize;
        xde_types.xde_leap_hand_data.size(ref elmt);
        xde_types.xde_leap_hand_data.serialize(bts, elmt, 0);
        xde_types.xde_leap_hand_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_leap_hand_data.Write(elmt, str);
        xde_types.xde_leap_hand_data.Read(str);
      }
      // xde_types.xde_leap_frame
      {
        xde_types.leap_frame elmt = xde_types.xde_leap_frame.default_value();
        bool tmp = xde_types.xde_leap_frame.fixedSize;
        xde_types.xde_leap_frame.size(ref elmt);
        xde_types.xde_leap_frame.serialize(bts, elmt, 0);
        xde_types.xde_leap_frame.deserialize(bts, ref elmt, 0);
        xde_types.xde_leap_frame.Write(elmt, str);
        xde_types.xde_leap_frame.Read(str);
      }
      // xde_types.xde_xsens_sensor
      {
        xde_types.xsens_sensor elmt = xde_types.xde_xsens_sensor.default_value();
        bool tmp = xde_types.xde_xsens_sensor.fixedSize;
        xde_types.xde_xsens_sensor.size(ref elmt);
        xde_types.xde_xsens_sensor.serialize(bts, elmt, 0);
        xde_types.xde_xsens_sensor.deserialize(bts, ref elmt, 0);
        xde_types.xde_xsens_sensor.Write(elmt, str);
        xde_types.xde_xsens_sensor.Read(str);
      }
      // xde_types.xde_xsens_frame
      {
        xde_types.xsens_frame elmt = xde_types.xde_xsens_frame.default_value();
        bool tmp = xde_types.xde_xsens_frame.fixedSize;
        xde_types.xde_xsens_frame.size(ref elmt);
        xde_types.xde_xsens_frame.serialize(bts, elmt, 0);
        xde_types.xde_xsens_frame.deserialize(bts, ref elmt, 0);
        xde_types.xde_xsens_frame.Write(elmt, str);
        xde_types.xde_xsens_frame.Read(str);
      }
      // xde_types.xde_openvr_sensor
      {
        xde_types.openvr_sensor elmt = xde_types.xde_openvr_sensor.default_value();
        bool tmp = xde_types.xde_openvr_sensor.fixedSize;
        xde_types.xde_openvr_sensor.size(ref elmt);
        xde_types.xde_openvr_sensor.serialize(bts, elmt, 0);
        xde_types.xde_openvr_sensor.deserialize(bts, ref elmt, 0);
        xde_types.xde_openvr_sensor.Write(elmt, str);
        xde_types.xde_openvr_sensor.Read(str);
      }
      // xde_types.xde_openvr_frame
      {
        xde_types.openvr_frame elmt = xde_types.xde_openvr_frame.default_value();
        bool tmp = xde_types.xde_openvr_frame.fixedSize;
        xde_types.xde_openvr_frame.size(ref elmt);
        xde_types.xde_openvr_frame.serialize(bts, elmt, 0);
        xde_types.xde_openvr_frame.deserialize(bts, ref elmt, 0);
        xde_types.xde_openvr_frame.Write(elmt, str);
        xde_types.xde_openvr_frame.Read(str);
      }
      // xde_types.xde_moticon_sole
      {
        xde_types.moticon_sole elmt = xde_types.xde_moticon_sole.default_value();
        bool tmp = xde_types.xde_moticon_sole.fixedSize;
        xde_types.xde_moticon_sole.size(ref elmt);
        xde_types.xde_moticon_sole.serialize(bts, elmt, 0);
        xde_types.xde_moticon_sole.deserialize(bts, ref elmt, 0);
        xde_types.xde_moticon_sole.Write(elmt, str);
        xde_types.xde_moticon_sole.Read(str);
      }
      // xde_types.xde_moticon_frame
      {
        xde_types.moticon_frame elmt = xde_types.xde_moticon_frame.default_value();
        bool tmp = xde_types.xde_moticon_frame.fixedSize;
        xde_types.xde_moticon_frame.size(ref elmt);
        xde_types.xde_moticon_frame.serialize(bts, elmt, 0);
        xde_types.xde_moticon_frame.deserialize(bts, ref elmt, 0);
        xde_types.xde_moticon_frame.Write(elmt, str);
        xde_types.xde_moticon_frame.Read(str);
      }
      // xde_types.xde_position_sensor_data
      {
        xde_types.position_sensor_data elmt = xde_types.xde_position_sensor_data.default_value();
        bool tmp = xde_types.xde_position_sensor_data.fixedSize;
        xde_types.xde_position_sensor_data.size(ref elmt);
        xde_types.xde_position_sensor_data.serialize(bts, elmt, 0);
        xde_types.xde_position_sensor_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_position_sensor_data.Write(elmt, str);
        xde_types.xde_position_sensor_data.Read(str);
      }
      // xde_types.xde_vector_position_sensor_data
      {
        xde_types.vector_position_sensor_data elmt = xde_types.xde_vector_position_sensor_data.default_value();
        bool tmp = xde_types.xde_vector_position_sensor_data.fixedSize;
        xde_types.xde_vector_position_sensor_data.size(ref elmt);
        xde_types.xde_vector_position_sensor_data.serialize(bts, elmt, 0);
        xde_types.xde_vector_position_sensor_data.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_position_sensor_data.Write(elmt, str);
        xde_types.xde_vector_position_sensor_data.Read(str);
      }
      // xde_types.xde_sample_type
      {
        xde_types.sample_type elmt = xde_types.xde_sample_type.default_value();
        bool tmp = xde_types.xde_sample_type.fixedSize;
        xde_types.xde_sample_type.size(ref elmt);
        xde_types.xde_sample_type.serialize(bts, elmt, 0);
        xde_types.xde_sample_type.deserialize(bts, ref elmt, 0);
        xde_types.xde_sample_type.Write(elmt, str);
        xde_types.xde_sample_type.Read(str);
      }
      // xde_types.xde_vector_vector3d
      {
        xde_types.vector_vector3d elmt = xde_types.xde_vector_vector3d.default_value();
        bool tmp = xde_types.xde_vector_vector3d.fixedSize;
        xde_types.xde_vector_vector3d.size(ref elmt);
        xde_types.xde_vector_vector3d.serialize(bts, elmt, 0);
        xde_types.xde_vector_vector3d.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_vector3d.Write(elmt, str);
        xde_types.xde_vector_vector3d.Read(str);
      }
      // xde_types.xde_vector_vector3f
      {
        xde_types.vector_vector3f elmt = xde_types.xde_vector_vector3f.default_value();
        bool tmp = xde_types.xde_vector_vector3f.fixedSize;
        xde_types.xde_vector_vector3f.size(ref elmt);
        xde_types.xde_vector_vector3f.serialize(bts, elmt, 0);
        xde_types.xde_vector_vector3f.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_vector3f.Write(elmt, str);
        xde_types.xde_vector_vector3f.Read(str);
      }
      // xde_types.xde_matrix_double
      {
        xde_types.matrix_double elmt = xde_types.xde_matrix_double.default_value();
        bool tmp = xde_types.xde_matrix_double.fixedSize;
        xde_types.xde_matrix_double.size(ref elmt);
        xde_types.xde_matrix_double.serialize(bts, elmt, 0);
        xde_types.xde_matrix_double.deserialize(bts, ref elmt, 0);
        xde_types.xde_matrix_double.Write(elmt, str);
        xde_types.xde_matrix_double.Read(str);
      }
      // xde_types.xde_vector_displacementd
      {
        xde_types.vector_displacementd elmt = xde_types.xde_vector_displacementd.default_value();
        bool tmp = xde_types.xde_vector_displacementd.fixedSize;
        xde_types.xde_vector_displacementd.size(ref elmt);
        xde_types.xde_vector_displacementd.serialize(bts, elmt, 0);
        xde_types.xde_vector_displacementd.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_displacementd.Write(elmt, str);
        xde_types.xde_vector_displacementd.Read(str);
      }
      // xde_types.xde_vector_displacementf
      {
        xde_types.vector_displacementf elmt = xde_types.xde_vector_displacementf.default_value();
        bool tmp = xde_types.xde_vector_displacementf.fixedSize;
        xde_types.xde_vector_displacementf.size(ref elmt);
        xde_types.xde_vector_displacementf.serialize(bts, elmt, 0);
        xde_types.xde_vector_displacementf.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_displacementf.Write(elmt, str);
        xde_types.xde_vector_displacementf.Read(str);
      }
      // xde_types.xde_vector_twistd
      {
        xde_types.vector_twistd elmt = xde_types.xde_vector_twistd.default_value();
        bool tmp = xde_types.xde_vector_twistd.fixedSize;
        xde_types.xde_vector_twistd.size(ref elmt);
        xde_types.xde_vector_twistd.serialize(bts, elmt, 0);
        xde_types.xde_vector_twistd.deserialize(bts, ref elmt, 0);
        xde_types.xde_vector_twistd.Write(elmt, str);
        xde_types.xde_vector_twistd.Read(str);
      }
    }
  }
}
#endif //ENABLE_IL2CPP