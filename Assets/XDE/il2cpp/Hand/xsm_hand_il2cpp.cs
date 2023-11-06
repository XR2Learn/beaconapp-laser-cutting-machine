// ===========================
// GENERATED CODE, DO NOT EDIT
// generated at 2021-08-03 18:00:45.807000
// from template 'cs/modules_components_uwp_cs.tpl'
// ===========================

#if ENABLE_IL2CPP


// https://docs.unity3d.com/Manual/ScriptingRestrictions.html 
// To work around an AOT issue like this, we force the compiler to generate the proper code.
namespace XdeEngine.IL2CPP
{
  class Basic_hand_hand
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.hand.PluginFactory tmp2 = new xde.client.hand.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      //xde_types.hand.xde_hand_parameters_generator
      {
        xde_types.hand.hand_parameters_generator elmt = xde_types.hand.xde_hand_parameters_generator.default_value();
        bool tmp = xde_types.hand.xde_hand_parameters_generator.fixedSize;
        xde_types.hand.xde_hand_parameters_generator.size(ref elmt);
        xde_types.hand.xde_hand_parameters_generator.serialize(bts, elmt, 0);
        xde_types.hand.xde_hand_parameters_generator.deserialize(bts, ref elmt, 0);
        xde_types.hand.xde_hand_parameters_generator.Write(elmt, str);
        xde_types.hand.xde_hand_parameters_generator.Read(str);
      }
      
      //xde_types.hand.xde_finger_bone_parameters
      {
        xde_types.hand.finger_bone_parameters elmt = xde_types.hand.xde_finger_bone_parameters.default_value();
        bool tmp = xde_types.hand.xde_finger_bone_parameters.fixedSize;
        xde_types.hand.xde_finger_bone_parameters.size(ref elmt);
        xde_types.hand.xde_finger_bone_parameters.serialize(bts, elmt, 0);
        xde_types.hand.xde_finger_bone_parameters.deserialize(bts, ref elmt, 0);
        xde_types.hand.xde_finger_bone_parameters.Write(elmt, str);
        xde_types.hand.xde_finger_bone_parameters.Read(str);
      }
      //xde_types.hand.xde_finger_parameters
      {
        xde_types.hand.finger_parameters elmt = xde_types.hand.xde_finger_parameters.default_value();
        bool tmp = xde_types.hand.xde_finger_parameters.fixedSize;
        xde_types.hand.xde_finger_parameters.size(ref elmt);
        xde_types.hand.xde_finger_parameters.serialize(bts, elmt, 0);
        xde_types.hand.xde_finger_parameters.deserialize(bts, ref elmt, 0);
        xde_types.hand.xde_finger_parameters.Write(elmt, str);
        xde_types.hand.xde_finger_parameters.Read(str);
      }
      //xde_types.hand.xde_hand_parameters
      {
        xde_types.hand.hand_parameters elmt = xde_types.hand.xde_hand_parameters.default_value();
        bool tmp = xde_types.hand.xde_hand_parameters.fixedSize;
        xde_types.hand.xde_hand_parameters.size(ref elmt);
        xde_types.hand.xde_hand_parameters.serialize(bts, elmt, 0);
        xde_types.hand.xde_hand_parameters.deserialize(bts, ref elmt, 0);
        xde_types.hand.xde_hand_parameters.Write(elmt, str);
        xde_types.hand.xde_hand_parameters.Read(str);
      }

    }
  };
}
#endif // ENABLE_IL2CPP// ===========================
// GENERATED CODE, DO NOT EDIT
// generated at 2021-08-03 18:00:45.807000
// from template 'cs/modules_components_uwp_cs.tpl'
// ===========================

#if ENABLE_IL2CPP


// https://docs.unity3d.com/Manual/ScriptingRestrictions.html 
// To work around an AOT issue like this, we force the compiler to generate the proper code.
namespace XdeEngine.IL2CPP
{
  class Basic_hand_mitten_hand
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.hand.PluginFactory tmp2 = new xde.client.hand.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      

    }
  };
}
#endif // ENABLE_IL2CPP// ===========================
// GENERATED CODE, DO NOT EDIT
// generated at 2021-08-03 18:00:45.807000
// from template 'cs/modules_components_uwp_cs.tpl'
// ===========================

#if ENABLE_IL2CPP


// https://docs.unity3d.com/Manual/ScriptingRestrictions.html 
// To work around an AOT issue like this, we force the compiler to generate the proper code.
namespace XdeEngine.IL2CPP
{
  class Basic_hand_skeletondevices
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.hand.PluginFactory tmp2 = new xde.client.hand.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      

    }
  };
}
#endif // ENABLE_IL2CPP
// https://docs.unity3d.com/Manual/ScriptingRestrictions.html 
// To work around an AOT issue like this, we force the compiler to generate the proper code.

#if ENABLE_IL2CPP
namespace XdeEngine.IL2CPP
{
  class Basic_hand
  {
    static void ForceRegistrationOfMethod()
    {
       XdeEngine.Hand.Device.LeapMotionManager lmm = new Hand.Device.LeapMotionManager();
    }
  }
}
#endif
