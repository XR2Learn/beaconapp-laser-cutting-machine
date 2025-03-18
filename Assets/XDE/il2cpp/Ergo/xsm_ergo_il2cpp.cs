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
  class Basic_ergo_posture
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.ergo.PluginFactory tmp2 = new xde.client.ergo.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      //xde_types.ergo.xde_posture_parameters
      {
        xde_types.ergo.posture_parameters elmt = xde_types.ergo.xde_posture_parameters.default_value();
        bool tmp = xde_types.ergo.xde_posture_parameters.fixedSize;
        xde_types.ergo.xde_posture_parameters.size(ref elmt);
        xde_types.ergo.xde_posture_parameters.serialize(bts, elmt, 0);
        xde_types.ergo.xde_posture_parameters.deserialize(bts, ref elmt, 0);
        xde_types.ergo.xde_posture_parameters.Write(elmt, str);
        xde_types.ergo.xde_posture_parameters.Read(str);
      }
      

    }
  };
}
#endif // ENABLE_IL2CPP
// https://docs.unity3d.com/Manual/ScriptingRestrictions.html 
// To work around an AOT issue like this, we force the compiler to generate the proper code.

#if ENABLE_IL2CPP
namespace XdeEngine.IL2CPP
{
  class Basic_ergo
  {
    static void ForceRegistrationOfMethod()
    {
      XdeEngine.Ergo.Device.MoticonManager mm = new XdeEngine.Ergo.Device.MoticonManager();
    }
  }
}
#endif
