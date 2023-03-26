/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.4
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace BerkeleyDB.Internal {

using System;
using System.Runtime.InteropServices;

internal class DB_REPMGR_SITE : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal DB_REPMGR_SITE(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(DB_REPMGR_SITE obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~DB_REPMGR_SITE() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          libdb_csharpPINVOKE.delete_DB_REPMGR_SITE(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  internal int eid {
    get {
      int ret = libdb_csharpPINVOKE.DB_REPMGR_SITE_eid_get(swigCPtr);
      return ret;
    } 
  }

  internal string host {
    get {
      string ret = libdb_csharpPINVOKE.DB_REPMGR_SITE_host_get(swigCPtr);
      return ret;
    } 
  }

  internal uint port {
    get {
      uint ret = libdb_csharpPINVOKE.DB_REPMGR_SITE_port_get(swigCPtr);
      return ret;
    } 
  }

  internal uint status {
    get {
      uint ret = libdb_csharpPINVOKE.DB_REPMGR_SITE_status_get(swigCPtr);
      return ret;
    } 
  }

  internal uint flags {
    get {
      uint ret = libdb_csharpPINVOKE.DB_REPMGR_SITE_flags_get(swigCPtr);
      return ret;
    } 
  }

  internal DB_REPMGR_SITE() : this(libdb_csharpPINVOKE.new_DB_REPMGR_SITE(), true) {
  }

}

}