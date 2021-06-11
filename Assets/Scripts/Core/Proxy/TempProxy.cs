using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TempProxy : Proxy
{
    public new const string NAME = "TempProxy";
    public TempProxy () : base ( NAME ) {}

    //private TempProxy mTempProxy_ = null;
    //private TempProxy mTempProxy
    //{
    //    get
    //    {
    //        if (mTempProxy_ == null)
    //        {
    //            mTempProxy_ = Facade.Instance.RetrieveProxy ( TempProxy.NAME ) as TempProxy;
    //        }
    //        return mTempProxy_;
    //    }
    //}
}

