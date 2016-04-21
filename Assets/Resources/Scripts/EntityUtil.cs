using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityUtil {

	public static GameObject PlaceEntity( GameObject entity, Node node, IntVector2 gridPos, bool forced = false ) {

		// Check to see if the GameObject is valid, return null if not
		if ( entity == null ) {
			Debug.LogError( "Cannot place entity; entity is null!" );
			return null;
		}

		// Check to see if the node is valid
		if ( node == null ) {
			Debug.LogError( "Cannot place entity; node is null!" );
			return null;
		}

		// Check to see if the gridPos array is valid
		//if ( gridPos.Length < 1 ) {
		if ( gridPos == null ) {
			Debug.LogError( "Cannot place entity; gridPos requires at least 1 element!" );
			return null;
		}

		// Set the position of the GameObject, Multi-tile not yet implemented
		//if ( gridPos.Length > 2 ) {
			// Multi tile entity, so placement code is centered amongst the tiles

		//} else {
			// Single tile entity, so placement code is simplified
			entity.transform.position = new Vector3( Mathf.Floor( node.pos.x + gridPos.x * GameController.tileSize ), Mathf.Floor( node.pos.y + gridPos.y * GameController.tileSize ), 0.0f );
		//}

		// Add entity to node list
		node.entities.Add( entity );

		// Return the entity
		return entity;
	}

	public static void GenerateNode( IntVector2 nodePos, NodeType nodeType ) {

		int sizeX = 0;
		int sizeY = 0;
		int[,] nodeArray = new int[0,0];

		Node newNode = new Node(nodePos, new List<GameObject>());
		GameObject ent;

		ObjectPool optt0 = GameObject.FindGameObjectWithTag("OPTT0").GetComponent<ObjectPool>();
		ObjectPool optt1 = GameObject.FindGameObjectWithTag("OPTT1").GetComponent<ObjectPool>();
		ObjectPool optt2 = GameObject.FindGameObjectWithTag("OPTT2").GetComponent<ObjectPool>();
		ObjectPool optt3 = GameObject.FindGameObjectWithTag("OPTT3").GetComponent<ObjectPool>();
		ObjectPool optt4 = GameObject.FindGameObjectWithTag("OPTT4").GetComponent<ObjectPool>();
		ObjectPool optt5 = GameObject.FindGameObjectWithTag("OPTT5").GetComponent<ObjectPool>();
		ObjectPool optt6 = GameObject.FindGameObjectWithTag("OPTT6").GetComponent<ObjectPool>();
		ObjectPool optt7 = GameObject.FindGameObjectWithTag("OPTT7").GetComponent<ObjectPool>();
		ObjectPool optt8 = GameObject.FindGameObjectWithTag("OPTT8").GetComponent<ObjectPool>();
		ObjectPool optt9 = GameObject.FindGameObjectWithTag("OPTT9").GetComponent<ObjectPool>();
		ObjectPool optt10 = GameObject.FindGameObjectWithTag("OPTT10").GetComponent<ObjectPool>();
		ObjectPool optt11 = GameObject.FindGameObjectWithTag("OPTT11").GetComponent<ObjectPool>();
		ObjectPool optt12 = GameObject.FindGameObjectWithTag("OPTT12").GetComponent<ObjectPool>();
		ObjectPool optt13 = GameObject.FindGameObjectWithTag("OPTT13").GetComponent<ObjectPool>();
		ObjectPool optt14 = GameObject.FindGameObjectWithTag("OPTT14").GetComponent<ObjectPool>();
		ObjectPool optt15 = GameObject.FindGameObjectWithTag("OPTT15").GetComponent<ObjectPool>();
		ObjectPool optt16 = GameObject.FindGameObjectWithTag("OPTT16").GetComponent<ObjectPool>();
		ObjectPool optt17 = GameObject.FindGameObjectWithTag("OPTT17").GetComponent<ObjectPool>();
		ObjectPool optt18 = GameObject.FindGameObjectWithTag("OPTT18").GetComponent<ObjectPool>();
		ObjectPool optt19 = GameObject.FindGameObjectWithTag("OPTT19").GetComponent<ObjectPool>();
		ObjectPool optt20 = GameObject.FindGameObjectWithTag("OPTT20").GetComponent<ObjectPool>();
		ObjectPool optt21 = GameObject.FindGameObjectWithTag("OPTT21").GetComponent<ObjectPool>();
		ObjectPool optt22 = GameObject.FindGameObjectWithTag("OPTT22").GetComponent<ObjectPool>();
		ObjectPool optt23 = GameObject.FindGameObjectWithTag("OPTT23").GetComponent<ObjectPool>();
		ObjectPool optt24 = GameObject.FindGameObjectWithTag("OPTT24").GetComponent<ObjectPool>();
		ObjectPool optt25 = GameObject.FindGameObjectWithTag("OPTT25").GetComponent<ObjectPool>();
		ObjectPool optt26 = GameObject.FindGameObjectWithTag("OPTT26").GetComponent<ObjectPool>();
		ObjectPool optt27 = GameObject.FindGameObjectWithTag("OPTT27").GetComponent<ObjectPool>();
		ObjectPool optt28 = GameObject.FindGameObjectWithTag("OPTT28").GetComponent<ObjectPool>();
		ObjectPool optt29 = GameObject.FindGameObjectWithTag("OPTT29").GetComponent<ObjectPool>();
		ObjectPool optt30 = GameObject.FindGameObjectWithTag("OPTT30").GetComponent<ObjectPool>();
		ObjectPool optt31 = GameObject.FindGameObjectWithTag("OPTT31").GetComponent<ObjectPool>();

		// Switch case for setting the node array to the correct design
		switch ( nodeType ) {
			case NodeType.Node_0:
				nodeArray = new int[20,20]
					{	{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50}	};
				sizeX = 20;
				sizeY = 20;
				break;
			case NodeType.Node_1:
				nodeArray = new int[20, 20]
					{   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}   };
				sizeX = 20;
				sizeY = 20;
				break;
		}

		for (int y = 0; y < sizeY;  y++) {
			for (int x = 0; x < sizeX;  x++) {

				switch (nodeArray[x,y]) {
					case 0:
						ent = PlaceEntity( optt0.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 1:
						ent = PlaceEntity( optt1.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 2:
						ent = PlaceEntity( optt2.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 3:
						ent = PlaceEntity( optt3.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 4:
						ent = PlaceEntity( optt4.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 5:
						ent = PlaceEntity( optt5.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 6:
						ent = PlaceEntity( optt6.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 7:
						ent = PlaceEntity( optt7.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 8:
						ent = PlaceEntity( optt8.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 9:
						ent = PlaceEntity( optt9.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 10:
						ent = PlaceEntity( optt10.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 11:
						ent = PlaceEntity( optt11.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 12:
						ent = PlaceEntity( optt12.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 13:
						ent = PlaceEntity( optt13.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 14:
						ent = PlaceEntity( optt14.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 15:
						ent = PlaceEntity( optt15.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 16:
						ent = PlaceEntity( optt16.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 17:
						ent = PlaceEntity( optt17.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 18:
						ent = PlaceEntity( optt18.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 19:
						ent = PlaceEntity( optt19.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 20:
						ent = PlaceEntity( optt20.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 21:
						ent = PlaceEntity( optt21.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 22:
						ent = PlaceEntity( optt22.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 23:
						ent = PlaceEntity( optt23.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 24:
						ent = PlaceEntity( optt24.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 25:
						ent = PlaceEntity( optt25.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 26:
						ent = PlaceEntity( optt26.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 27:
						ent = PlaceEntity( optt27.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 28:
						ent = PlaceEntity( optt28.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 29:
						ent = PlaceEntity( optt29.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 30:
						ent = PlaceEntity( optt30.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 31:
						ent = PlaceEntity( optt31.GetPooledObject(), newNode, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					default:
						break;
				}
			}
		}
	}
}
