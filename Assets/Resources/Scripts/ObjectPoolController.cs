using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolController : MonoBehaviour {

	[SerializeField]
	public Dictionary<EntityType, ObjectPool> poolList { get; private set; }

	// Use this for initialization
	public void Initialize() {
		poolList = new Dictionary<EntityType, ObjectPool>();

		Component[] opList;
		opList = transform.GetComponentsInChildren<ObjectPool>();

		foreach ( ObjectPool op in opList ) {

			if ( op != null )
				poolList.Add( op.PoolType, op );
		}
	}

	public ObjectPool getObjectPool( EntityType typeKey ) {

		return poolList[typeKey];
	}
}
