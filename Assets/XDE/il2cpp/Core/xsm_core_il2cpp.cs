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
  class Basic_core_beam
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      //xde_types.core.xde_beam_configuration
      {
        xde_types.core.beam_configuration elmt = xde_types.core.xde_beam_configuration.default_value();
        bool tmp = xde_types.core.xde_beam_configuration.fixedSize;
        xde_types.core.xde_beam_configuration.size(ref elmt);
        xde_types.core.xde_beam_configuration.serialize(bts, elmt, 0);
        xde_types.core.xde_beam_configuration.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_beam_configuration.Write(elmt, str);
        xde_types.core.xde_beam_configuration.Read(str);
      }
      //xde_types.core.xde_beam_stiffness_computation_mode
      {
        xde_types.core.beam_stiffness_computation_mode elmt = xde_types.core.xde_beam_stiffness_computation_mode.default_value();
        bool tmp = xde_types.core.xde_beam_stiffness_computation_mode.fixedSize;
        xde_types.core.xde_beam_stiffness_computation_mode.size(ref elmt);
        xde_types.core.xde_beam_stiffness_computation_mode.serialize(bts, elmt, 0);
        xde_types.core.xde_beam_stiffness_computation_mode.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_beam_stiffness_computation_mode.Write(elmt, str);
        xde_types.core.xde_beam_stiffness_computation_mode.Read(str);
      }
      //xde_types.core.xde_beam_attach_mode
      {
        xde_types.core.beam_attach_mode elmt = xde_types.core.xde_beam_attach_mode.default_value();
        bool tmp = xde_types.core.xde_beam_attach_mode.fixedSize;
        xde_types.core.xde_beam_attach_mode.size(ref elmt);
        xde_types.core.xde_beam_attach_mode.serialize(bts, elmt, 0);
        xde_types.core.xde_beam_attach_mode.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_beam_attach_mode.Write(elmt, str);
        xde_types.core.xde_beam_attach_mode.Read(str);
      }
      
      //xde_types.core.xde_beam_material
      {
        xde_types.core.beam_material elmt = xde_types.core.xde_beam_material.default_value();
        bool tmp = xde_types.core.xde_beam_material.fixedSize;
        xde_types.core.xde_beam_material.size(ref elmt);
        xde_types.core.xde_beam_material.serialize(bts, elmt, 0);
        xde_types.core.xde_beam_material.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_beam_material.Write(elmt, str);
        xde_types.core.xde_beam_material.Read(str);
      }
      //xde_types.core.xde_beam_state
      {
        xde_types.core.beam_state elmt = xde_types.core.xde_beam_state.default_value();
        bool tmp = xde_types.core.xde_beam_state.fixedSize;
        xde_types.core.xde_beam_state.size(ref elmt);
        xde_types.core.xde_beam_state.serialize(bts, elmt, 0);
        xde_types.core.xde_beam_state.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_beam_state.Write(elmt, str);
        xde_types.core.xde_beam_state.Read(str);
      }
      //xde_types.core.xde_beam_state_complete
      {
        xde_types.core.beam_state_complete elmt = xde_types.core.xde_beam_state_complete.default_value();
        bool tmp = xde_types.core.xde_beam_state_complete.fixedSize;
        xde_types.core.xde_beam_state_complete.size(ref elmt);
        xde_types.core.xde_beam_state_complete.serialize(bts, elmt, 0);
        xde_types.core.xde_beam_state_complete.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_beam_state_complete.Write(elmt, str);
        xde_types.core.xde_beam_state_complete.Read(str);
      }
      //xde_types.core.xde_beam_composition
      {
        xde_types.core.beam_composition elmt = xde_types.core.xde_beam_composition.default_value();
        bool tmp = xde_types.core.xde_beam_composition.fixedSize;
        xde_types.core.xde_beam_composition.size(ref elmt);
        xde_types.core.xde_beam_composition.serialize(bts, elmt, 0);
        xde_types.core.xde_beam_composition.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_beam_composition.Write(elmt, str);
        xde_types.core.xde_beam_composition.Read(str);
      }
      //xde_types.core.xde_beam_compression
      {
        xde_types.core.beam_compression elmt = xde_types.core.xde_beam_compression.default_value();
        bool tmp = xde_types.core.xde_beam_compression.fixedSize;
        xde_types.core.xde_beam_compression.size(ref elmt);
        xde_types.core.xde_beam_compression.serialize(bts, elmt, 0);
        xde_types.core.xde_beam_compression.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_beam_compression.Write(elmt, str);
        xde_types.core.xde_beam_compression.Read(str);
      }
      //xde_types.core.xde_beam_positions
      {
        xde_types.core.beam_positions elmt = xde_types.core.xde_beam_positions.default_value();
        bool tmp = xde_types.core.xde_beam_positions.fixedSize;
        xde_types.core.xde_beam_positions.size(ref elmt);
        xde_types.core.xde_beam_positions.serialize(bts, elmt, 0);
        xde_types.core.xde_beam_positions.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_beam_positions.Write(elmt, str);
        xde_types.core.xde_beam_positions.Read(str);
      }
      //xde_types.core.xde_beam_velocities
      {
        xde_types.core.beam_velocities elmt = xde_types.core.xde_beam_velocities.default_value();
        bool tmp = xde_types.core.xde_beam_velocities.fixedSize;
        xde_types.core.xde_beam_velocities.size(ref elmt);
        xde_types.core.xde_beam_velocities.serialize(bts, elmt, 0);
        xde_types.core.xde_beam_velocities.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_beam_velocities.Write(elmt, str);
        xde_types.core.xde_beam_velocities.Read(str);
      }
      //xde_types.core.xde_leak_A
      {
        xde_types.core.leak_A elmt = xde_types.core.xde_leak_A.default_value();
        bool tmp = xde_types.core.xde_leak_A.fixedSize;
        xde_types.core.xde_leak_A.size(ref elmt);
        xde_types.core.xde_leak_A.serialize(bts, elmt, 0);
        xde_types.core.xde_leak_A.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_leak_A.Write(elmt, str);
        xde_types.core.xde_leak_A.Read(str);
      }
      //xde_types.core.xde_leak_B
      {
        xde_types.core.leak_B elmt = xde_types.core.xde_leak_B.default_value();
        bool tmp = xde_types.core.xde_leak_B.fixedSize;
        xde_types.core.xde_leak_B.size(ref elmt);
        xde_types.core.xde_leak_B.serialize(bts, elmt, 0);
        xde_types.core.xde_leak_B.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_leak_B.Write(elmt, str);
        xde_types.core.xde_leak_B.Read(str);
      }
      //xde_types.core.xde_leak_C
      {
        xde_types.core.leak_C elmt = xde_types.core.xde_leak_C.default_value();
        bool tmp = xde_types.core.xde_leak_C.fixedSize;
        xde_types.core.xde_leak_C.size(ref elmt);
        xde_types.core.xde_leak_C.serialize(bts, elmt, 0);
        xde_types.core.xde_leak_C.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_leak_C.Write(elmt, str);
        xde_types.core.xde_leak_C.Read(str);
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
  class Basic_core_collision
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      //xde_types.core.xde_collision_mode
      {
        xde_types.core.collision_mode elmt = xde_types.core.xde_collision_mode.default_value();
        bool tmp = xde_types.core.xde_collision_mode.fixedSize;
        xde_types.core.xde_collision_mode.size(ref elmt);
        xde_types.core.xde_collision_mode.serialize(bts, elmt, 0);
        xde_types.core.xde_collision_mode.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_collision_mode.Write(elmt, str);
        xde_types.core.xde_collision_mode.Read(str);
      }
      //xde_types.core.xde_intersect_query_type
      {
        xde_types.core.intersect_query_type elmt = xde_types.core.xde_intersect_query_type.default_value();
        bool tmp = xde_types.core.xde_intersect_query_type.fixedSize;
        xde_types.core.xde_intersect_query_type.size(ref elmt);
        xde_types.core.xde_intersect_query_type.serialize(bts, elmt, 0);
        xde_types.core.xde_intersect_query_type.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_intersect_query_type.Write(elmt, str);
        xde_types.core.xde_intersect_query_type.Read(str);
      }
      //xde_types.core.xde_monitoring_mode
      {
        xde_types.core.monitoring_mode elmt = xde_types.core.xde_monitoring_mode.default_value();
        bool tmp = xde_types.core.xde_monitoring_mode.fixedSize;
        xde_types.core.xde_monitoring_mode.size(ref elmt);
        xde_types.core.xde_monitoring_mode.serialize(bts, elmt, 0);
        xde_types.core.xde_monitoring_mode.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_monitoring_mode.Write(elmt, str);
        xde_types.core.xde_monitoring_mode.Read(str);
      }
      
      //xde_types.core.xde_interference_event
      {
        xde_types.core.interference_event elmt = xde_types.core.xde_interference_event.default_value();
        bool tmp = xde_types.core.xde_interference_event.fixedSize;
        xde_types.core.xde_interference_event.size(ref elmt);
        xde_types.core.xde_interference_event.serialize(bts, elmt, 0);
        xde_types.core.xde_interference_event.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_interference_event.Write(elmt, str);
        xde_types.core.xde_interference_event.Read(str);
      }
      //xde_types.core.xde_interference_list
      {
        xde_types.core.interference_list elmt = xde_types.core.xde_interference_list.default_value();
        bool tmp = xde_types.core.xde_interference_list.fixedSize;
        xde_types.core.xde_interference_list.size(ref elmt);
        xde_types.core.xde_interference_list.serialize(bts, elmt, 0);
        xde_types.core.xde_interference_list.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_interference_list.Write(elmt, str);
        xde_types.core.xde_interference_list.Read(str);
      }
      //xde_types.core.xde_layer_interaction
      {
        xde_types.core.layer_interaction elmt = xde_types.core.xde_layer_interaction.default_value();
        bool tmp = xde_types.core.xde_layer_interaction.fixedSize;
        xde_types.core.xde_layer_interaction.size(ref elmt);
        xde_types.core.xde_layer_interaction.serialize(bts, elmt, 0);
        xde_types.core.xde_layer_interaction.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_layer_interaction.Write(elmt, str);
        xde_types.core.xde_layer_interaction.Read(str);
      }
      //xde_types.core.xde_layer_interaction_list
      {
        xde_types.core.layer_interaction_list elmt = xde_types.core.xde_layer_interaction_list.default_value();
        bool tmp = xde_types.core.xde_layer_interaction_list.fixedSize;
        xde_types.core.xde_layer_interaction_list.size(ref elmt);
        xde_types.core.xde_layer_interaction_list.serialize(bts, elmt, 0);
        xde_types.core.xde_layer_interaction_list.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_layer_interaction_list.Write(elmt, str);
        xde_types.core.xde_layer_interaction_list.Read(str);
      }
      //xde_types.core.xde_contact_point
      {
        xde_types.core.contact_point elmt = xde_types.core.xde_contact_point.default_value();
        bool tmp = xde_types.core.xde_contact_point.fixedSize;
        xde_types.core.xde_contact_point.size(ref elmt);
        xde_types.core.xde_contact_point.serialize(bts, elmt, 0);
        xde_types.core.xde_contact_point.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_contact_point.Write(elmt, str);
        xde_types.core.xde_contact_point.Read(str);
      }
      //xde_types.core.xde_contact_bodypair
      {
        xde_types.core.contact_bodypair elmt = xde_types.core.xde_contact_bodypair.default_value();
        bool tmp = xde_types.core.xde_contact_bodypair.fixedSize;
        xde_types.core.xde_contact_bodypair.size(ref elmt);
        xde_types.core.xde_contact_bodypair.serialize(bts, elmt, 0);
        xde_types.core.xde_contact_bodypair.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_contact_bodypair.Write(elmt, str);
        xde_types.core.xde_contact_bodypair.Read(str);
      }
      //xde_types.core.xde_contact_layerpair
      {
        xde_types.core.contact_layerpair elmt = xde_types.core.xde_contact_layerpair.default_value();
        bool tmp = xde_types.core.xde_contact_layerpair.fixedSize;
        xde_types.core.xde_contact_layerpair.size(ref elmt);
        xde_types.core.xde_contact_layerpair.serialize(bts, elmt, 0);
        xde_types.core.xde_contact_layerpair.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_contact_layerpair.Write(elmt, str);
        xde_types.core.xde_contact_layerpair.Read(str);
      }
      //xde_types.core.xde_contact_events
      {
        xde_types.core.contact_events elmt = xde_types.core.xde_contact_events.default_value();
        bool tmp = xde_types.core.xde_contact_events.fixedSize;
        xde_types.core.xde_contact_events.size(ref elmt);
        xde_types.core.xde_contact_events.serialize(bts, elmt, 0);
        xde_types.core.xde_contact_events.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_contact_events.Write(elmt, str);
        xde_types.core.xde_contact_events.Read(str);
      }
      //xde_types.core.xde_distance
      {
        xde_types.core.distance elmt = xde_types.core.xde_distance.default_value();
        bool tmp = xde_types.core.xde_distance.fixedSize;
        xde_types.core.xde_distance.size(ref elmt);
        xde_types.core.xde_distance.serialize(bts, elmt, 0);
        xde_types.core.xde_distance.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_distance.Write(elmt, str);
        xde_types.core.xde_distance.Read(str);
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
  class Basic_core_couplings
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

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
  class Basic_core_devices
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      //xde_types.core.xde_grasp_offset_mode
      {
        xde_types.core.grasp_offset_mode elmt = xde_types.core.xde_grasp_offset_mode.default_value();
        bool tmp = xde_types.core.xde_grasp_offset_mode.fixedSize;
        xde_types.core.xde_grasp_offset_mode.size(ref elmt);
        xde_types.core.xde_grasp_offset_mode.serialize(bts, elmt, 0);
        xde_types.core.xde_grasp_offset_mode.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_grasp_offset_mode.Write(elmt, str);
        xde_types.core.xde_grasp_offset_mode.Read(str);
      }
      
      //xde_types.core.xde_pbm_state
      {
        xde_types.core.pbm_state elmt = xde_types.core.xde_pbm_state.default_value();
        bool tmp = xde_types.core.xde_pbm_state.fixedSize;
        xde_types.core.xde_pbm_state.size(ref elmt);
        xde_types.core.xde_pbm_state.serialize(bts, elmt, 0);
        xde_types.core.xde_pbm_state.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_pbm_state.Write(elmt, str);
        xde_types.core.xde_pbm_state.Read(str);
      }
      //xde_types.core.xde_spacemouse_state
      {
        xde_types.core.spacemouse_state elmt = xde_types.core.xde_spacemouse_state.default_value();
        bool tmp = xde_types.core.xde_spacemouse_state.fixedSize;
        xde_types.core.xde_spacemouse_state.size(ref elmt);
        xde_types.core.xde_spacemouse_state.serialize(bts, elmt, 0);
        xde_types.core.xde_spacemouse_state.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_spacemouse_state.Write(elmt, str);
        xde_types.core.xde_spacemouse_state.Read(str);
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
  class Basic_core_joints
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      //xde_types.core.xde_coupling_mode
      {
        xde_types.core.coupling_mode elmt = xde_types.core.xde_coupling_mode.default_value();
        bool tmp = xde_types.core.xde_coupling_mode.fixedSize;
        xde_types.core.xde_coupling_mode.size(ref elmt);
        xde_types.core.xde_coupling_mode.serialize(bts, elmt, 0);
        xde_types.core.xde_coupling_mode.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_coupling_mode.Write(elmt, str);
        xde_types.core.xde_coupling_mode.Read(str);
      }
      //xde_types.core.xde_mapped_joint_type
      {
        xde_types.core.mapped_joint_type elmt = xde_types.core.xde_mapped_joint_type.default_value();
        bool tmp = xde_types.core.xde_mapped_joint_type.fixedSize;
        xde_types.core.xde_mapped_joint_type.size(ref elmt);
        xde_types.core.xde_mapped_joint_type.serialize(bts, elmt, 0);
        xde_types.core.xde_mapped_joint_type.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_mapped_joint_type.Write(elmt, str);
        xde_types.core.xde_mapped_joint_type.Read(str);
      }
      
      //xde_types.core.xde_joint_state
      {
        xde_types.core.joint_state elmt = xde_types.core.xde_joint_state.default_value();
        bool tmp = xde_types.core.xde_joint_state.fixedSize;
        xde_types.core.xde_joint_state.size(ref elmt);
        xde_types.core.xde_joint_state.serialize(bts, elmt, 0);
        xde_types.core.xde_joint_state.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_joint_state.Write(elmt, str);
        xde_types.core.xde_joint_state.Read(str);
      }
      //xde_types.core.xde_unit_joint_state
      {
        xde_types.core.unit_joint_state elmt = xde_types.core.xde_unit_joint_state.default_value();
        bool tmp = xde_types.core.xde_unit_joint_state.fixedSize;
        xde_types.core.xde_unit_joint_state.size(ref elmt);
        xde_types.core.xde_unit_joint_state.serialize(bts, elmt, 0);
        xde_types.core.xde_unit_joint_state.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_unit_joint_state.Write(elmt, str);
        xde_types.core.xde_unit_joint_state.Read(str);
      }
      //xde_types.core.xde_ball_joint_state
      {
        xde_types.core.ball_joint_state elmt = xde_types.core.xde_ball_joint_state.default_value();
        bool tmp = xde_types.core.xde_ball_joint_state.fixedSize;
        xde_types.core.xde_ball_joint_state.size(ref elmt);
        xde_types.core.xde_ball_joint_state.serialize(bts, elmt, 0);
        xde_types.core.xde_ball_joint_state.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_ball_joint_state.Write(elmt, str);
        xde_types.core.xde_ball_joint_state.Read(str);
      }
      //xde_types.core.xde_dry_friction_state
      {
        xde_types.core.dry_friction_state elmt = xde_types.core.xde_dry_friction_state.default_value();
        bool tmp = xde_types.core.xde_dry_friction_state.fixedSize;
        xde_types.core.xde_dry_friction_state.size(ref elmt);
        xde_types.core.xde_dry_friction_state.serialize(bts, elmt, 0);
        xde_types.core.xde_dry_friction_state.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_dry_friction_state.Write(elmt, str);
        xde_types.core.xde_dry_friction_state.Read(str);
      }
      //xde_types.core.xde_unit_joint_PD_state
      {
        xde_types.core.unit_joint_PD_state elmt = xde_types.core.xde_unit_joint_PD_state.default_value();
        bool tmp = xde_types.core.xde_unit_joint_PD_state.fixedSize;
        xde_types.core.xde_unit_joint_PD_state.size(ref elmt);
        xde_types.core.xde_unit_joint_PD_state.serialize(bts, elmt, 0);
        xde_types.core.xde_unit_joint_PD_state.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_unit_joint_PD_state.Write(elmt, str);
        xde_types.core.xde_unit_joint_PD_state.Read(str);
      }
      //xde_types.core.xde_unit_joint_PID_state
      {
        xde_types.core.unit_joint_PID_state elmt = xde_types.core.xde_unit_joint_PID_state.default_value();
        bool tmp = xde_types.core.xde_unit_joint_PID_state.fixedSize;
        xde_types.core.xde_unit_joint_PID_state.size(ref elmt);
        xde_types.core.xde_unit_joint_PID_state.serialize(bts, elmt, 0);
        xde_types.core.xde_unit_joint_PID_state.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_unit_joint_PID_state.Write(elmt, str);
        xde_types.core.xde_unit_joint_PID_state.Read(str);
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
  class Basic_core_raycasting
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

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
  class Basic_core_record
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

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
  class Basic_core_scene
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      //xde_types.core.xde_collision_solver
      {
        xde_types.core.collision_solver elmt = xde_types.core.xde_collision_solver.default_value();
        bool tmp = xde_types.core.xde_collision_solver.fixedSize;
        xde_types.core.xde_collision_solver.size(ref elmt);
        xde_types.core.xde_collision_solver.serialize(bts, elmt, 0);
        xde_types.core.xde_collision_solver.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_collision_solver.Write(elmt, str);
        xde_types.core.xde_collision_solver.Read(str);
      }
      //xde_types.core.xde_gvm_evolution
      {
        xde_types.core.gvm_evolution elmt = xde_types.core.xde_gvm_evolution.default_value();
        bool tmp = xde_types.core.xde_gvm_evolution.fixedSize;
        xde_types.core.xde_gvm_evolution.size(ref elmt);
        xde_types.core.xde_gvm_evolution.serialize(bts, elmt, 0);
        xde_types.core.xde_gvm_evolution.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_gvm_evolution.Write(elmt, str);
        xde_types.core.xde_gvm_evolution.Read(str);
      }
      //xde_types.core.xde_contact_restitution_mode
      {
        xde_types.core.contact_restitution_mode elmt = xde_types.core.xde_contact_restitution_mode.default_value();
        bool tmp = xde_types.core.xde_contact_restitution_mode.fixedSize;
        xde_types.core.xde_contact_restitution_mode.size(ref elmt);
        xde_types.core.xde_contact_restitution_mode.serialize(bts, elmt, 0);
        xde_types.core.xde_contact_restitution_mode.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_contact_restitution_mode.Write(elmt, str);
        xde_types.core.xde_contact_restitution_mode.Read(str);
      }
      //xde_types.core.xde_contact_type
      {
        xde_types.core.contact_type elmt = xde_types.core.xde_contact_type.default_value();
        bool tmp = xde_types.core.xde_contact_type.fixedSize;
        xde_types.core.xde_contact_type.size(ref elmt);
        xde_types.core.xde_contact_type.serialize(bts, elmt, 0);
        xde_types.core.xde_contact_type.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_contact_type.Write(elmt, str);
        xde_types.core.xde_contact_type.Read(str);
      }
      //xde_types.core.xde_performance_timer_data
      {
        xde_types.core.performance_timer_data elmt = xde_types.core.xde_performance_timer_data.default_value();
        bool tmp = xde_types.core.xde_performance_timer_data.fixedSize;
        xde_types.core.xde_performance_timer_data.size(ref elmt);
        xde_types.core.xde_performance_timer_data.serialize(bts, elmt, 0);
        xde_types.core.xde_performance_timer_data.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_performance_timer_data.Write(elmt, str);
        xde_types.core.xde_performance_timer_data.Read(str);
      }
      //xde_types.core.xde_gvm_direct_solver
      {
        xde_types.core.gvm_direct_solver elmt = xde_types.core.xde_gvm_direct_solver.default_value();
        bool tmp = xde_types.core.xde_gvm_direct_solver.fixedSize;
        xde_types.core.xde_gvm_direct_solver.size(ref elmt);
        xde_types.core.xde_gvm_direct_solver.serialize(bts, elmt, 0);
        xde_types.core.xde_gvm_direct_solver.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_gvm_direct_solver.Write(elmt, str);
        xde_types.core.xde_gvm_direct_solver.Read(str);
      }
      //xde_types.core.xde_limiter_mode
      {
        xde_types.core.limiter_mode elmt = xde_types.core.xde_limiter_mode.default_value();
        bool tmp = xde_types.core.xde_limiter_mode.fixedSize;
        xde_types.core.xde_limiter_mode.size(ref elmt);
        xde_types.core.xde_limiter_mode.serialize(bts, elmt, 0);
        xde_types.core.xde_limiter_mode.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_limiter_mode.Write(elmt, str);
        xde_types.core.xde_limiter_mode.Read(str);
      }
      
      //xde_types.core.xde_contact_law
      {
        xde_types.core.contact_law elmt = xde_types.core.xde_contact_law.default_value();
        bool tmp = xde_types.core.xde_contact_law.fixedSize;
        xde_types.core.xde_contact_law.size(ref elmt);
        xde_types.core.xde_contact_law.serialize(bts, elmt, 0);
        xde_types.core.xde_contact_law.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_contact_law.Write(elmt, str);
        xde_types.core.xde_contact_law.Read(str);
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
  class Basic_core_skeleton
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      
      //xde_types.core.xde_skeleton_tracker_state
      {
        xde_types.core.skeleton_tracker_state elmt = xde_types.core.xde_skeleton_tracker_state.default_value();
        bool tmp = xde_types.core.xde_skeleton_tracker_state.fixedSize;
        xde_types.core.xde_skeleton_tracker_state.size(ref elmt);
        xde_types.core.xde_skeleton_tracker_state.serialize(bts, elmt, 0);
        xde_types.core.xde_skeleton_tracker_state.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_skeleton_tracker_state.Write(elmt, str);
        xde_types.core.xde_skeleton_tracker_state.Read(str);
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
  class Basic_core_sweptVolume
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

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
  class Basic_core_variables
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      //xde_types.core.xde_variable_type
      {
        xde_types.core.variable_type elmt = xde_types.core.xde_variable_type.default_value();
        bool tmp = xde_types.core.xde_variable_type.fixedSize;
        xde_types.core.xde_variable_type.size(ref elmt);
        xde_types.core.xde_variable_type.serialize(bts, elmt, 0);
        xde_types.core.xde_variable_type.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_variable_type.Write(elmt, str);
        xde_types.core.xde_variable_type.Read(str);
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
  class Basic_core_webservice
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

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
  class Basic_core_welder
  {
    static void ForceRegistrationOfMethod()
    {
      // The factory must be registered.
      xde.client.core.PluginFactory tmp2 = new xde.client.core.PluginFactory();

      // Register all the types
      System.IO.FileStream str = new System.IO.FileStream("/C/tmp/", System.IO.FileMode.Create);
      byte[] bts = new byte[0];
      
      //xde_types.core.xde_weld_joint_state
      {
        xde_types.core.weld_joint_state elmt = xde_types.core.xde_weld_joint_state.default_value();
        bool tmp = xde_types.core.xde_weld_joint_state.fixedSize;
        xde_types.core.xde_weld_joint_state.size(ref elmt);
        xde_types.core.xde_weld_joint_state.serialize(bts, elmt, 0);
        xde_types.core.xde_weld_joint_state.deserialize(bts, ref elmt, 0);
        xde_types.core.xde_weld_joint_state.Write(elmt, str);
        xde_types.core.xde_weld_joint_state.Read(str);
      }

    }
  };
}
#endif // ENABLE_IL2CPP