using UnityEngine;
using System.Collections;

public class Tile{

	// Each tile needs to know it's grid location
	public Vector2 position { get; private set; }

	// Each tile needs to know it's owner, we can implement this later
	// public Faction owner { get; private set; }

	// Each tile needs to know it's tile type
	public TileType type { get; private set; }

	// The Tile class constructor
	public Tile(Vector2 _position, TileType _type ) {
		this.position = _position;
		this.type = _type;
	}
}
