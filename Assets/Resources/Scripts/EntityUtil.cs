using UnityEngine;
using System.Collections;

public class EntityUtil {

	public static GameObject PlaceEntity( GameObject entity, int[] gridPos, bool forced = false ) {

		// Check to see if the GameObject is valid, return null if not
		if ( entity == null ) {
			Debug.LogError( "Cannot place entity; entity is null!" );
			return null;
		}

		// Check to dee if the gridPos array is valid
		if ( gridPos.Length < 2 ) {
			Debug.LogError( "Cannot place entity; gridPos requires at least 2 elements!" );
			return null;
		}

		// Set the position of the GameObject, Multi-tile not yet implemented
		if ( gridPos.Length > 2 ) {
			// Multi tile entity, so placement code is centered amongst the tiles

		} else {
			// Single tile entity, so placement code is simplified
			entity.transform.position = new Vector3( Mathf.Floor( gridPos[0] * GameController.tileSize ), Mathf.Floor( gridPos[1] * GameController.tileSize ), 0.0f );
		}



		// Return the entity
		return entity;
	}
}
