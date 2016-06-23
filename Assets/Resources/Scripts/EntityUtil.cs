using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityUtil {

	public static GameObject PlaceEntity( GameObject entity, POI poi, IntVector2 gridPos, bool forced = false ) {

		// Check to see if the GameObject is valid, return null if not
		if ( entity == null ) {
			Debug.LogError( "Cannot place entity; entity is null!" );
			return null;
		}

		// Check to see if the node is valid
		if ( poi == null ) {
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
		entity.transform.position = new Vector3( Mathf.Floor( poi.pos.x + gridPos.x * GameController.tileSize ), Mathf.Floor( poi.pos.y + gridPos.y * GameController.tileSize ), 0.0f );
		//}

		// Add entity to node list
		poi.entities.Add( entity );

		// Return the entity
		return entity;
	}

	public static void GenerateSector( IntVector2 SectorPos, int POICount ) {

		// Create a new list to hold unique random numbers
		List<int> randList = new List<int>();

		// Create new system random number
		System.Random rnd = new System.Random();

		// Generate random number and add it to the list if unique
		// For some reason I cannot get this to work
		while ( randList.Count < POICount ) {

			// Generate a random number between 0 and SectorCellCount^2
			int number = rnd.Next(0, (GameController.SectorCellCount * GameController.SectorCellCount));
			//Debug.Log( randList.Count );

			// Check to see if the random number is unique
			if ( !randList.Contains(number) ) {
				randList.Add( number );
				//Debug.Log( string.Format( "Added POI ... {0}", randList.Count ) );
			}
		}

		// Cycle through each randomly selected cell and generate a POI
		for (int i = 0; i < randList.Count; i++) {

			// Set the position of the POI- fun maths; finds the location of the center of the cell in respect to the location of the sector
			// i.e SectorPos.X = 2000
			// Cell Size = 250
			// Cell Count = 8
			// POI.X = 2
			// Cell Center X = SectorPosX + (CellSize * POIX) - (CellSize * (CellCount / 2)) - (CellSize / 2)
			// Cell Center X = 2000 + (250 * 2) - (250 * (8 / 2)) - (250 / 2)
			// Cell Center X = 2000 + (500) - (1000) - (125) = 1375
			int POIY = Mathf.FloorToInt( randList[i] / GameController.SectorCellCount );
			//Debug.Log( string.Format("POIY: {0}", POIY) );
			int POIX = ( randList[i] % GameController.SectorCellCount );
			//Debug.Log( string.Format( "POIX: {0}", POIX ) );
			IntVector2 POIPos = new IntVector2( SectorPos.x + (int)((GameController.SectorCellSize * POIX) - (GameController.SectorCellSize * (GameController.SectorCellCount / 2)) - (GameController.SectorCellSize / 2)),
												SectorPos.y + (int)((GameController.SectorCellSize * POIY) - (GameController.SectorCellSize * (GameController.SectorCellCount / 2)) - (GameController.SectorCellSize / 2)));
			//Debug.Log( string.Format( "POIPOSX: {0} POIPOSY: {1}", POIPos.x, POIPos.y ) );

			// Generate a random POI type
			// Random range 1 to 4 because we want to select POIType of range 1-3
			// Min is inclusive, max is exclusive
			// We change this to 0,4 once dynamic generation is in
			int rand = rnd.Next(1, 4);
			POIType type;

			// Select a POI type based on the random
			switch ( rand ) {
				case 0:
					type = POIType.POI_0;
					break;
				case 1:
					type = POIType.POI_1;
					break;
				case 2:
					type = POIType.POI_2;
					break;
				case 3:
					type = POIType.POI_3;
					break;
				default:
					type = POIType.POI_0;
					break;
			}

			// Generate POI
			GeneratePOI( POIPos, type );
		}
	}

	public static void GeneratePOI( IntVector2 POIPos, POIType poiType ) {

		int sizeX = 0;
		int sizeY = 0;
		int[,] tileArray = new int[0,0];
		int[,] entityArray = new int[0,0];

		POI newPOI = new POI(POIPos, new List<GameObject>());
		GameObject ent;

		GameController.POIs.Add( newPOI );

		ObjectPoolController opc = GameObject.FindGameObjectWithTag("OPC").GetComponent<ObjectPoolController>();

		ObjectPool optt0 = opc.getObjectPool(EntityType.Tile_0);
		ObjectPool optt1 = opc.getObjectPool(EntityType.Tile_1);
		ObjectPool optt2 = opc.getObjectPool(EntityType.Tile_2);
		ObjectPool optt3 = opc.getObjectPool(EntityType.Tile_3);
		ObjectPool optt4 = opc.getObjectPool(EntityType.Tile_4);
		ObjectPool optt5 = opc.getObjectPool(EntityType.Tile_5);
		ObjectPool optt6 = opc.getObjectPool(EntityType.Tile_6);
		ObjectPool optt7 = opc.getObjectPool(EntityType.Tile_7);
		ObjectPool optt8 = opc.getObjectPool(EntityType.Tile_8);
		ObjectPool optt9 = opc.getObjectPool(EntityType.Tile_9);
		ObjectPool optt10 = opc.getObjectPool(EntityType.Tile_10);
		ObjectPool optt11 = opc.getObjectPool(EntityType.Tile_11);
		ObjectPool optt12 = opc.getObjectPool(EntityType.Tile_12);
		ObjectPool optt13 = opc.getObjectPool(EntityType.Tile_13);
		ObjectPool optt14 = opc.getObjectPool(EntityType.Tile_14);
		ObjectPool optt15 = opc.getObjectPool(EntityType.Tile_15);
		ObjectPool optt16 = opc.getObjectPool(EntityType.Tile_16);
		ObjectPool optt17 = opc.getObjectPool(EntityType.Tile_17);
		ObjectPool optt18 = opc.getObjectPool(EntityType.Tile_18);
		ObjectPool optt19 = opc.getObjectPool(EntityType.Tile_19);
		ObjectPool optt20 = opc.getObjectPool(EntityType.Tile_20);
		ObjectPool optt21 = opc.getObjectPool(EntityType.Tile_21);
		ObjectPool optt22 = opc.getObjectPool(EntityType.Tile_22);
		ObjectPool optt23 = opc.getObjectPool(EntityType.Tile_23);
		ObjectPool optt24 = opc.getObjectPool(EntityType.Tile_24);
		ObjectPool optt25 = opc.getObjectPool(EntityType.Tile_25);
		ObjectPool optt26 = opc.getObjectPool(EntityType.Tile_26);
		ObjectPool optt27 = opc.getObjectPool(EntityType.Tile_27);
		ObjectPool optt28 = opc.getObjectPool(EntityType.Tile_28);
		ObjectPool optt29 = opc.getObjectPool(EntityType.Tile_29);
		ObjectPool optt30 = opc.getObjectPool(EntityType.Tile_30);
		ObjectPool optt31 = opc.getObjectPool(EntityType.Tile_31);

		ObjectPool optt32 = opc.getObjectPool(EntityType.Tile_32);
		ObjectPool optt33 = opc.getObjectPool(EntityType.Tile_33);
		ObjectPool optt34 = opc.getObjectPool(EntityType.Tile_34);
		ObjectPool optt35 = opc.getObjectPool(EntityType.Tile_35);
		ObjectPool optt36 = opc.getObjectPool(EntityType.Tile_36);
		ObjectPool optt37 = opc.getObjectPool(EntityType.Tile_37);
		ObjectPool optt38 = opc.getObjectPool(EntityType.Tile_38);
		ObjectPool optt39 = opc.getObjectPool(EntityType.Tile_39);
		ObjectPool optt40 = opc.getObjectPool(EntityType.Tile_40);
		ObjectPool optt41 = opc.getObjectPool(EntityType.Tile_41);
		ObjectPool optt42 = opc.getObjectPool(EntityType.Tile_42);

		// Switch case for setting the node array to the correct design
		switch ( poiType ) {
			case POIType.POI_0:
				// Dynamic generation stuff goes hereeeee

				break;
			case POIType.POI_1:
				tileArray = new int[20, 20]
					{   {50,31, 5,10,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,31,13, 9,50,50,50,50, 1, 3, 3,10,50,50,50,50,50,50,50,50},
						{50,31,19, 1,10, 1, 3,10, 2,19,19,12,50,50,50,50,50,50,50,50},
						{50,31,19, 4,12, 2,19,12, 2,19,19,12,50,50,50,50,50,50,50,50},
						{50,31,19, 0, 9, 0,13, 9, 2,19,19,12,50,50,50,50,50,50,50,50},
						{50,31,19,25,26,27,19,19, 4,19,19,12,50,50,50,50,50,50,50,50},
						{50,31,19, 1,10, 1, 3,10, 2,19,19,12,50,50,50,50,50,50,50,50},
						{50,31,19, 4,12, 2,19,12, 1, 5, 5,10,50,50,50,50,50,50,50,50},
						{ 1,10,13, 0, 9, 0,13, 9, 0,11,11, 9, 1,10,50,50,50,50,50,50},
						{ 4,14,19,19,19,19,19,19,19,19,19,19, 4,14,50,50,50,50,50,50},
						{ 4,14,19,19,19,19,19,19,19,19,19,19, 4,14,50,50,50,50,50,50},
						{ 0, 9, 1,10, 5, 1,10,19,19, 1, 5,10, 0, 9,50,50,50,50,50,50},
						{50,50, 2,12,13, 2,12,19,19, 2,19,12,50,50,50,50,50,50,50,50},
						{50,50, 2,19,19,19,12,19,19, 2,19,12,50,50,50,50,50,50,50,50},
						{50,50, 0,11,11,11, 9,19,19, 0,11, 9,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50, 1, 5, 5,10,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50, 0,13,13, 9,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50}   };
				entityArray = new int[20, 20]
					{   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,2,0,0,8,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,9,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }	};
				sizeX = 20;
				sizeY = 20;
				break;
			case POIType.POI_2:
				tileArray = new int[20, 20]
					{   {50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,50,50,50, 1, 3, 3, 3,10,50,50,50,50,50},
						{50,50,50,50,50, 1, 3, 3, 3,10, 2,19,19,19,12,50,50,50,50,50},
						{50,50,50,50,50, 2,19,19,19,12, 5, 2,19,19,12,50,50,50,50,50},
						{50,50,50,50,50, 2,20,11,11, 9,13, 2,19,19,12,50,50,50,50,50},
						{50,50,50,50,50, 2,19, 4,14,19,19, 2,19,19,12,50,50,50,50,50},
						{50,50,50,50,50, 2,19, 4,14,19,19, 0,11,11, 9,50,50,50,50,50},
						{50,50,50,50,50,29,29,29, 1, 5, 5,10,50,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,30, 0,19,19, 9,30,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50, 4,14,19,20, 4,14,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50, 4,14,19,19, 4,14,50,50,50,50,50,50,50},
						{50,50,50,50,50,50,50,29, 1,19,19,10,29,50,50,50,50,50,50,50},
						{50,50,30,30,30,30,30,30, 0,13,13, 9,30,30,30,30,30,30,50,50},
						{50,31,19,19,19,19,19,19,19,19,19,19,19,20,19,19,19,19,28,50},
						{50,31,19,19,19,19,19,19,20,19,19,19,19,19,19,19,19,19,28,50},
						{50,31,19,19, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3,10,19,19,28,50},
						{50,31,19,19, 0,11,19,20,19,19,19,19,19,19,11, 9,19,19,28,50},
						{50,31,19,19, 4,14,19,19,19,19,19,19,19,19, 4,14,19,19,28,50},
						{50,31,19,19, 1, 3,19,19,19,19,19,20,19,19, 3,10,19,19,28,50},
						{50,50,29,29, 0,11,11,11,11,11,11,11,11,11,11, 9,29,29,50,50}   };
				entityArray = new int[20, 20]
					{   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,3,0,4,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  };
				sizeX = 20;
				sizeY = 20;
				break;
			case POIType.POI_3:
				tileArray = new int[20, 20]
					{   {50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50},
						{50,50,30,30,30,30,30,30,30,30,30,30,50,50,50,50,50,50,50,50},
						{50,50, 2,19,19,19, 4,14,19,19,19,19,28,50,50,50,50,50,50,50},
						{50,50, 2,19,19,19,10, 1,19,19,19,19,28,50,50,50,50,50,50,50},
						{50,50, 2,19,19,19,12, 2,19,19,19,19, 1, 3, 3, 3,10,50,50,50},
						{50,50, 2,19,19,19,12, 2,19,19,12, 5, 2,19,19,19,12,50,50,50},
						{50,50, 0,11,11,11, 9, 0,11,11, 9,13, 2,19,19,19,12,50,50,50},
						{50,50,50,50,50, 4,14,25,26,27,12,19, 0,11,19,19,12,50,50,50},
						{50,50,50,50,50, 1, 3, 3, 3, 3,12,19, 4,14,19,19,12,50,50,50},
						{50,50,50,50,50, 2,19,19,19,19,12,19, 1,10, 5, 1, 3, 3,10,50},
						{50,50,50,50,50, 2,19,19,19,19,12,19, 2,12,13, 2,19,19,12,50},
						{50,50,50,50,50, 2,19,19,19,19,12,19, 2,19,19,19,19,19,12,50},
						{50,50,50,50, 1,10, 5, 2,19,19,12,19, 2,19,19,19,19,19,12,50},
						{50,50,50,50, 2,12,13, 0,11,11, 9,19, 2,19,19,19,19,19,12,50},
						{50,50,50,50, 2,19,19,19,19, 4,14,19, 2,12, 5, 2,19,19,12,50},
						{50,50,50,50, 2,19,19,19,19, 4,14,19, 0, 9,13, 0,11,11, 9,50},
						{50,50,50,50, 2,19,19,19,19, 3,10,19, 4,14,19,19,12,50,50,50},
						{50,50,50,50, 0,11,11,11,11,11, 9,19, 1, 3,19,19,12,50,50,50},
						{50,50,50,50,50,50,50, 4,14,25,26,27, 2,19,19,19,12,50,50,50},
						{50,50,50,50,50,50,50,29,29,29,29,29, 0,11,11,11, 9,50,50,50}  };
				entityArray = new int[20, 20]
					{   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,7,0,0,0,4,0,0,0,0,0,0,0,0,10,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
						{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }  };
				sizeX = 20;
				sizeY = 20;
				break;
		}

		for ( int y = 0; y < sizeY; y++ ) {
			for ( int x = 0; x < sizeX; x++ ) {

				switch ( tileArray[x, y] ) {
					case 0:
						ent = PlaceEntity( optt0.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 1:
						ent = PlaceEntity( optt1.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 2:
						ent = PlaceEntity( optt2.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 3:
						ent = PlaceEntity( optt3.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 4:
						ent = PlaceEntity( optt4.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 5:
						ent = PlaceEntity( optt5.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 6:
						ent = PlaceEntity( optt6.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 7:
						ent = PlaceEntity( optt7.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 8:
						ent = PlaceEntity( optt8.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 9:
						ent = PlaceEntity( optt9.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 10:
						ent = PlaceEntity( optt10.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 11:
						ent = PlaceEntity( optt11.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 12:
						ent = PlaceEntity( optt12.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 13:
						ent = PlaceEntity( optt13.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 14:
						ent = PlaceEntity( optt14.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 15:
						ent = PlaceEntity( optt15.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 16:
						ent = PlaceEntity( optt16.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 17:
						ent = PlaceEntity( optt17.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 18:
						ent = PlaceEntity( optt18.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 19:
						ent = PlaceEntity( optt19.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 20:
						ent = PlaceEntity( optt20.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 21:
						ent = PlaceEntity( optt21.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 22:
						ent = PlaceEntity( optt22.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 23:
						ent = PlaceEntity( optt23.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 24:
						ent = PlaceEntity( optt24.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 25:
						ent = PlaceEntity( optt25.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 26:
						ent = PlaceEntity( optt26.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 27:
						ent = PlaceEntity( optt27.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 28:
						ent = PlaceEntity( optt28.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 29:
						ent = PlaceEntity( optt29.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 30:
						ent = PlaceEntity( optt30.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 31:
						ent = PlaceEntity( optt31.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					default:
						break;
				}

				switch ( entityArray[x, y] ) {
					case 1:
						ent = PlaceEntity( optt32.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 2:
						ent = PlaceEntity( optt33.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 3:
						ent = PlaceEntity( optt34.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 4:
						ent = PlaceEntity( optt35.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 5:
						ent = PlaceEntity( optt36.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 6:
						ent = PlaceEntity( optt37.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 7:
						ent = PlaceEntity( optt38.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 8:
						ent = PlaceEntity( optt39.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 9:
						ent = PlaceEntity( optt40.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 10:
						ent = PlaceEntity( optt41.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					case 11:
						ent = PlaceEntity( optt42.GetPooledObject(), newPOI, new IntVector2( x, y ) );
						ent.SetActive( true );
						break;
					default:
						break;
				}
			}
		}
	}
}
