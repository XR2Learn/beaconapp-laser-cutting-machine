
// In order to use the serialization assembly in a unity project for an AOT platform
// (see https://docs.unity3d.com/Manual/ScriptingRestrictions.html)
// this file must be included as source in a unity project, in order to force generation of code

namespace xsm.serialization.msgpack.IL2CPP
{
  class XmpTypes
  {

    // This force AOT computation
    static void ForceRegistrationOfMethod()
    {
      // templates for serialization of basic types
      System.IO.MemoryStream str = new System.IO.MemoryStream();
      byte[] bts = new byte[0];

      //xsm.serialization.msgpack.xmp_peer_info
      {
        xsm2.PeerInfo elmt = xsm.serialization.msgpack.xmp_peer_info.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_peer_info.fixedSize;
        xsm.serialization.msgpack.xmp_peer_info.size(ref elmt);
        xsm.serialization.msgpack.xmp_peer_info.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_peer_info.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_peer_info.Write(elmt, str);
        xsm.serialization.msgpack.xmp_peer_info.Read(str);
      }

      //xsm.serialization.msgpack.xmp_peer_type
      {
        xsm2.XsmPeerType elmt = xsm.serialization.msgpack.xmp_peer_type.default_value();
        bool tmp = xsm.serialization.msgpack.xmp_peer_type.fixedSize;
        xsm.serialization.msgpack.xmp_peer_type.size(ref elmt);
        xsm.serialization.msgpack.xmp_peer_type.serialize(bts, elmt, 0);
        xsm.serialization.msgpack.xmp_peer_type.deserialize(bts, ref elmt, 0);
        xsm.serialization.msgpack.xmp_peer_type.Write(elmt, str);
        xsm.serialization.msgpack.xmp_peer_type.Read(str);
      }
    }
  }
}
