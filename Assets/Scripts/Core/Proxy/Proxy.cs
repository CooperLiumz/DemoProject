public class Proxy : System.Object {

	public static string NAME = "Proxy";

	public Proxy(): this(NAME, null)
	{
	}

	public Proxy(string proxyName): this(proxyName, null)
	{
	}

	public Proxy(string proxyName, object data)
	{		
		m_proxyName = (proxyName != null) ? proxyName : NAME;
		if (data != null) m_data = data;
	}

	virtual public void OnRegister()
	{
	}

	virtual public void OnRemove()
	{
	}

	public string ProxyName
	{
		get { return m_proxyName; }
	}

	public object Data
	{
		get 
		{
			return m_data; 
		}
		set { m_data = value; }
	}
			
	protected string m_proxyName;

	protected object m_data;
    
}
