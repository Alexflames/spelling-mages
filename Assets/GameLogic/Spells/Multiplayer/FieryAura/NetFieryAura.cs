//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;

//public class NetFieryAura : NetEmptyAura
//{
//	NetFieryAuraInit NFAI;


//	void Awake()
//	{
//		if (isClient)
//		{
//			NFAI = gameObject.GetComponent<NetFieryAuraInit>();
//		}
//		else if (isServer)
//		{
//			CmdInit();
//		}
//	}

//	[Command]
//	void CmdInit()
//	{
//		NFAI = gameObject.GetComponent<NetFieryAuraInit>();
//	}

//	//[ClientRpc]
//	//void RpcInit(NetFieryAuraInit auraInit)
//	//{
//	//	NFAI = auraInit;
//	//}

	


//}
