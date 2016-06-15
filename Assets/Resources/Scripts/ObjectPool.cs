using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Usage:
/// 1. Create a new empty 'gameobject' in Unity- name it to represent the 'gameobject' you are pooling
/// 2. Place the 'objectpool' script inside the new 'gameobject' you created
/// 3. Set the variables for the 'objectpool' script; 
///		'pooledObject' is the 'gameobject' you are pooling
///		'poolSize' is the number of 'gameobject' to initialize the pool with
///		'dynamicPool' is a boolean to allow/disallow the expansion/contraction of the pool size
/// 
/// - Additional gameobjects are created on demand- extras can be purged via the 'PurgePooledObjects()' method
/// - You must activate/deactivate gameobjects manually in local code;
///		Replace 'instantiate' with setActive(true)
///		Replace 'destroy' with setActive(false)
/// - Dynamic pooling can be used to limit the number of a gameobject in existance;
///		i.e. a guided missile that can only have one instance (set the 'poolSize' to 1 with 'dynamicPooling' disabled)
/// - When calling 'GetPooledObject()' method and 'dynamicPooling' is disabled ensure that you handle a 'null' return value
/// 
/// To Do:
/// 1. Modify ObjectPool class to support multiple pools for various gameobjects (?)
/// 2. Add helper methods for activating/deactivating gameobjects (helper functions may be implemented in entity placement script) (?)
/// 3. Timer based pool purging/culling (gameobjects that have not been activated in x seconds are destroyed) (?)
/// 4. Instantiate pooled objects as child of parent object (might have adverse effects with transforms) (?)
/// 5. Custom inspector script with debugging/management tools (?)
/// 6. Less wordy method names (?)
/// </summary>

public class ObjectPool : MonoBehaviour {

	// GameObject to be pooled
	[SerializeField]
	private GameObject pooledObject;
	public GameObject PooledObject { get { return pooledObject; } }

	// Entity type of the pool
	[SerializeField]
	private EntityType poolType;
	public EntityType PoolType { get { return poolType; } }

	// Initial size of the pool
	[SerializeField]
	private int poolSize;

	// Allow the pool to expand/contract
	[SerializeField]
	private bool dynamicPooling;

	// List for the pooled objects (serialized for debugging)
	[SerializeField]
	private List<GameObject> objectPool;

	/// <summary>
	/// The 'Start' method initializes the object pool with the desired amount of gameobjects.
	/// </summary>
	void Start() {

		// Check if 'pooledObject' has been set, throw an error if it is null
		if ( pooledObject == null ) {

			// Throw a debug error
			Debug.LogError( string.Format( "Object Pool \"{0}\" does not have 'pooledObject' type set!", this.name ) );

			// Break execution
			return;
		}

		// Initialize the list
		objectPool = new List<GameObject>();

		// Populate the object pool
		for ( int i = 0; i < poolSize; i++ ) {

			// Instantiate a new gameobject
			GameObject obj = (GameObject)Instantiate(pooledObject);

			// Set it to disabled (for consistancy)
			obj.SetActive( false );

			// Set object as child of ObjectPool
			obj.transform.parent = this.transform;

			// Add it to the pool
			objectPool.Add( obj );
		}
	}

	/// <summary>
	/// GetPooledObject Method
	/// ------------------------
	/// Usage:
	/// Call this method via 'myObjectPool.GetPooledObject()' to get a reference to an inactive
	/// gameobject from the pool. If 'dynamicPooling' is enabled a new gameobject will be created
	/// if there are no inactive references left- otherwise it will return 'null'.
	/// 
	/// Make sure to handle a 'null' value if dynamic pooling is disabled.
	/// </summary>
	/// <returns>
	/// Inactive 'gameobject' reference or 'null'
	/// </returns>
	public GameObject GetPooledObject() {

		// Iterate through object pool
		for ( int i = 0; i < objectPool.Count; i++ ) {

			// Check if gameobject is inactive
			if ( !objectPool[i].activeInHierarchy ) {

				// Return inactive gameobject
				return objectPool[i];
			}
		}

		// Create new game object if allowed
		if ( dynamicPooling ) {

			// Instantiate new game object
			GameObject obj = (GameObject)Instantiate(pooledObject);

			// Set object to inactive (for consistancy)
			obj.SetActive( false );

			// Set object as child of ObjectPool
			obj.transform.parent = this.transform;

			// Add it to the object pool
			objectPool.Add( obj );

			// Return 
			return obj;
		}

		// Return null if no inactive gameobjects are in the pool and dynamic is disabled
		return null;
	}

	/// <summary>
	/// PurgePooledObject Method
	/// ------------------------
	/// Usage:
	/// If 'dyamicPooling' is enabled, call this method via 'myObjectPool.PurgePooledObjects()' to
	/// destroy all extra inactive gameobject references in the pool.
	/// </summary>
	public void PurgePooledObjects() {

		// Check if dynamic pooling is enabled
		if ( dynamicPooling ) {

			// Do not purge if within poolSize limit
			if ( !( objectPool.Count > poolSize ) )
				return;

			// Iterate through object pool backwards (to avoid skipping)
			for ( int i = ( objectPool.Count - 1 ); i >= 0; i-- ) {

				// Check if gameobject is inactive
				if ( !objectPool[i].activeInHierarchy ) {

					// Delete gameobject
					Destroy( objectPool[i] );

					// Remove object from pool
					objectPool.Remove( objectPool[i] );
				}
			}
		}
	}
}
