using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Facade : System.Object {
	private bool mInit = false;
	protected Dictionary<string, Proxy> m_proxyMap = new Dictionary<string, Proxy>();

	public void RegisterProxy(Proxy proxy){
		lock (m_staticSyncRoot)
		{
			m_proxyMap[proxy.ProxyName] = proxy;
		}		
		proxy.OnRegister();
	}

	public Proxy RetrieveProxy(string proxyName){
		lock (m_staticSyncRoot)
		{
			if (!m_proxyMap.ContainsKey(proxyName)) return null;
			return m_proxyMap[proxyName];
		}
	}

	public virtual Proxy RemoveProxy(string proxyName)
	{
		Proxy proxy = null;
		
		lock (m_staticSyncRoot)
		{
			if (m_proxyMap.ContainsKey(proxyName))
			{
				proxy = RetrieveProxy(proxyName);
				m_proxyMap.Remove(proxyName);
			}
		}
		
		if (proxy != null) proxy.OnRemove();
		return proxy;
	}

	
	public virtual bool HasProxy(string proxyName)
	{
		lock (m_staticSyncRoot)
		{
			return m_proxyMap.ContainsKey(proxyName);
		}
	}

	public void InitProxy(){
		if (mInit) { //运行后保证只注册一次，防止常驻界面取的对象还是旧的
			return ;  
		}
		mInit = true;

        Facade.Instance.RegisterProxy(new TempProxy ());

    }

    protected static volatile Facade m_instance;
	protected static readonly object m_staticSyncRoot = new object();
	public static Facade Instance
	{
		get
		{
			if (m_instance == null)
			{
				lock (m_staticSyncRoot)
				{
					if (m_instance == null) m_instance = new Facade();
				}
			}
			
			return m_instance;
		}
	}

}
