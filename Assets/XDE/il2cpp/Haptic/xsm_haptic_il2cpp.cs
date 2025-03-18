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
  class Basic_haptic_virtuose
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.haptic.PluginFactory tmp2 = new xde.client.haptic.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      //xde_types.haptic.xde_trigger_input
      {
        xde_types.haptic.trigger_input elmt = xde_types.haptic.xde_trigger_input.default_value();
        bool tmp = xde_types.haptic.xde_trigger_input.fixedSize;
        xde_types.haptic.xde_trigger_input.size(ref elmt);
        xde_types.haptic.xde_trigger_input.serialize(bts, elmt, 0);
        xde_types.haptic.xde_trigger_input.deserialize(bts, ref elmt, 0);
        xde_types.haptic.xde_trigger_input.Write(elmt, str);
        xde_types.haptic.xde_trigger_input.Read(str);
      }
      //xde_types.haptic.xde_button_mode
      {
        xde_types.haptic.button_mode elmt = xde_types.haptic.xde_button_mode.default_value();
        bool tmp = xde_types.haptic.xde_button_mode.fixedSize;
        xde_types.haptic.xde_button_mode.size(ref elmt);
        xde_types.haptic.xde_button_mode.serialize(bts, elmt, 0);
        xde_types.haptic.xde_button_mode.deserialize(bts, ref elmt, 0);
        xde_types.haptic.xde_button_mode.Write(elmt, str);
        xde_types.haptic.xde_button_mode.Read(str);
      }
      

    }
  };
}
#endif // ENABLE_IL2CPP