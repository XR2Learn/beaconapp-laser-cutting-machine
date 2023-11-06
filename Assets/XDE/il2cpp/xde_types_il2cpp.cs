
// In order to use the serialization assembly in a unity project for an AOT platform
// (see https://docs.unity3d.com/Manual/ScriptingRestrictions.html)
// this file must be included as source in a unity project, in order to force generation of code

namespace xsm.serialization.msgpack.IL2CPP
{
  class BasicTypes
  {

    // This force AOT computation
    static void ForceRegistrationOfMethod()
    {

      // This specific Dictionary constructor must be registered
      xsm.serialization.msgpack.DictionarySerializer<string, System.Collections.Generic.Dictionary<string, string>> dico =
        new xsm.serialization.msgpack.DictionarySerializer<string, System.Collections.Generic.Dictionary<string, string>>();

      // templates for serialization of basic types
      System.IO.MemoryStream str = new System.IO.MemoryStream();
      byte[] bts = new byte[0];


      // xsm.serialization.msgpack.xmp_bool
      {
        bool elmt = xsm.serialization.msgpack.xmp_bool.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_bool.fixedSize;
        xsm.serialization.msgpack.xmp_bool.size(ref elmt);
        xsm.serialization.msgpack.xmp_bool.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_bool.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_bool.Write(elmt, str);
        xsm.serialization.msgpack.xmp_bool.Read(str);
      }

      //xsm.serialization.msgpack.xmp_integer (int)
      {
        int elmt = xsm.serialization.msgpack.xmp_integer.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_integer.fixedSize;
        xsm.serialization.msgpack.xmp_integer.size(ref elmt);
        xsm.serialization.msgpack.xmp_integer.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_integer.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_integer.Read(str);
        xsm.serialization.msgpack.xmp_integer.Write(elmt, str);
      }

      //xsm.serialization.msgpack.xmp_long_integer (long)
      {
        long elmt = xsm.serialization.msgpack.xmp_long_integer.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_long_integer.fixedSize;
        xsm.serialization.msgpack.xmp_long_integer.size(ref elmt);
        xsm.serialization.msgpack.xmp_long_integer.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_long_integer.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_long_integer.Read(str);
        xsm.serialization.msgpack.xmp_long_integer.Write(elmt, str);
      }

      //xsm.serialization.msgpack.xmp_fixed32_integer (uint)
      {
        System.UInt32 elmt = xsm.serialization.msgpack.xmp_fixed32_integer.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_fixed32_integer.fixedSize;
        xsm.serialization.msgpack.xmp_fixed32_integer.size(ref elmt);
        xsm.serialization.msgpack.xmp_fixed32_integer.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_fixed32_integer.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_fixed32_integer.Write(elmt, str);
        xsm.serialization.msgpack.xmp_fixed32_integer.Read(str);
      }



      // xsm.serialization.msgpack.xmp_float
      {
        float elmt = xsm.serialization.msgpack.xmp_float.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_float.fixedSize;
        xsm.serialization.msgpack.xmp_float.size(ref elmt);
        xsm.serialization.msgpack.xmp_float.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_float.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_float.Write(elmt, str);
        xsm.serialization.msgpack.xmp_float.Read(str);
      }

      // xsm.serialization.msgpack.xmp_double
      {
        double elmt = xsm.serialization.msgpack.xmp_double.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_double.fixedSize;
        xsm.serialization.msgpack.xmp_double.size(ref elmt);
        xsm.serialization.msgpack.xmp_double.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_double.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_double.Write(elmt, str);
        xsm.serialization.msgpack.xmp_double.Read(str);
      }

      // xsm.serialization.msgpack.xmp_string
      {
        string elmt = xsm.serialization.msgpack.xmp_string.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_string.fixedSize;
        xsm.serialization.msgpack.xmp_string.size(ref elmt);
        xsm.serialization.msgpack.xmp_string.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_string.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_string.Write(elmt, str);
        xsm.serialization.msgpack.xmp_string.Read(str);
      }

      //xsm.serialization.msgpack.xmp_bytearray
      {
        byte[] elmt = xsm.serialization.msgpack.xmp_bytearray.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_bytearray.fixedSize;
        xsm.serialization.msgpack.xmp_bytearray.size(ref elmt);
        xsm.serialization.msgpack.xmp_bytearray.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_bytearray.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_bytearray.Write(elmt, str);
        xsm.serialization.msgpack.xmp_bytearray.Read(str);
      }

      // xsm.serialization.msgpack.xmp_displacementf
      {
        xde.unity.math.Displacement elmt = xsm.serialization.msgpack.xmp_displacementf.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_displacementf.fixedSize;
        xsm.serialization.msgpack.xmp_displacementf.size(ref elmt);
        xsm.serialization.msgpack.xmp_displacementf.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_displacementf.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_displacementf.Write(elmt, str);
        xsm.serialization.msgpack.xmp_displacementf.Read(str);
      }

      // xsm.serialization.msgpack.xmp_displacementd
      {
        xde.unity.math.Displacementd elmt = xsm.serialization.msgpack.xmp_displacementd.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_displacementd.fixedSize;
        xsm.serialization.msgpack.xmp_displacementd.size(ref elmt);
        xsm.serialization.msgpack.xmp_displacementd.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_displacementd.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_displacementd.Write(elmt, str);
        xsm.serialization.msgpack.xmp_displacementd.Read(str);
      }

      // xsm.serialization.msgpack.xmp_wrenchd
      {
        xde.unity.math.Wrench elmt = xsm.serialization.msgpack.xmp_wrenchd.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_wrenchd.fixedSize;
        xsm.serialization.msgpack.xmp_wrenchd.size(ref elmt);
        xsm.serialization.msgpack.xmp_wrenchd.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_wrenchd.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_wrenchd.Write(elmt, str);
        xsm.serialization.msgpack.xmp_wrenchd.Read(str);
      }

      // xsm.serialization.msgpack.xmp_twistd
      {
        xde.unity.math.Twist elmt = xsm.serialization.msgpack.xmp_twistd.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_twistd.fixedSize;
        xsm.serialization.msgpack.xmp_twistd.size(ref elmt);
        xsm.serialization.msgpack.xmp_twistd.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_twistd.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_twistd.Write(elmt, str);
        xsm.serialization.msgpack.xmp_twistd.Read(str);
      }

      // xsm.serialization.msgpack.xmp_vector3f
      {
        UnityEngine.Vector3 elmt = xsm.serialization.msgpack.xmp_vector3f.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_vector3f.fixedSize;
        xsm.serialization.msgpack.xmp_vector3f.size(ref elmt);
        xsm.serialization.msgpack.xmp_vector3f.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_vector3f.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_vector3f.Write(elmt, str);
        xsm.serialization.msgpack.xmp_vector3f.Read(str);
      }

      // xsm.serialization.msgpack.xmp_vector3d
      {
        xde.unity.math.Vector3d elmt = xsm.serialization.msgpack.xmp_vector3d.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_vector3d.fixedSize;
        xsm.serialization.msgpack.xmp_vector3d.size(ref elmt);
        xsm.serialization.msgpack.xmp_vector3d.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_vector3d.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_vector3d.Write(elmt, str);
        xsm.serialization.msgpack.xmp_vector3d.Read(str);
      }

      // xsm.serialization.msgpack.xmp_vector2d
      {
        xde.unity.math.Vector2d elmt = xsm.serialization.msgpack.xmp_vector2d.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_vector2d.fixedSize;
        xsm.serialization.msgpack.xmp_vector2d.size(ref elmt);
        xsm.serialization.msgpack.xmp_vector2d.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_vector2d.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_vector2d.Write(elmt, str);
        xsm.serialization.msgpack.xmp_vector2d.Read(str);
      }

      // xsm.serialization.msgpack.xmp_vector2f
      {
        UnityEngine.Vector2 elmt = xsm.serialization.msgpack.xmp_vector2f.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_vector2f.fixedSize;
        xsm.serialization.msgpack.xmp_vector2f.size(ref elmt);
        xsm.serialization.msgpack.xmp_vector2f.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_vector2f.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_vector2f.Write(elmt, str);
        xsm.serialization.msgpack.xmp_vector2f.Read(str);
      }

      // xsm.serialization.msgpack.xmp_quaternionf
      {
        UnityEngine.Quaternion elmt = xsm.serialization.msgpack.xmp_quaternionf.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_quaternionf.fixedSize;
        xsm.serialization.msgpack.xmp_quaternionf.size(ref elmt);
        xsm.serialization.msgpack.xmp_quaternionf.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_quaternionf.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_quaternionf.Write(elmt, str);
        xsm.serialization.msgpack.xmp_quaternionf.Read(str);
      }

      // xsm.serialization.msgpack.xmp_quaterniond
      {
        xde.unity.math.Quaterniond elmt = xsm.serialization.msgpack.xmp_quaterniond.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_quaterniond.fixedSize;
        xsm.serialization.msgpack.xmp_quaterniond.size(ref elmt);
        xsm.serialization.msgpack.xmp_quaterniond.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_quaterniond.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_quaterniond.Write(elmt, str);
        xsm.serialization.msgpack.xmp_quaterniond.Read(str);
      }

      //xsm.serialization.msgpack.xmp_heterogeneous_array
      {
        xsm.serialization.msgpack.HeterogeneousArray elmt = xsm.serialization.msgpack.xmp_heterogeneous_array.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_heterogeneous_array.fixedSize;
        xsm.serialization.msgpack.xmp_heterogeneous_array.size(ref elmt);
        xsm.serialization.msgpack.xmp_heterogeneous_array.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_heterogeneous_array.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_heterogeneous_array.Write(elmt, str);
        xsm.serialization.msgpack.xmp_heterogeneous_array.Read(str);
      }

      //xsm.serialization.msgpack.xmp_heterogeneous_map
      {
        xsm.serialization.msgpack.HeterogeneousMap elmt = xsm.serialization.msgpack.xmp_heterogeneous_map.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_heterogeneous_map.fixedSize;
        xsm.serialization.msgpack.xmp_heterogeneous_map.size(ref elmt);
        xsm.serialization.msgpack.xmp_heterogeneous_map.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_heterogeneous_map.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_heterogeneous_map.Write(elmt, str);
        xsm.serialization.msgpack.xmp_heterogeneous_map.Read(str);
      }


    }
  }
}
