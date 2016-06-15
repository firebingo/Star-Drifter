using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class POI {

	public IntVector2 pos { get; set; }
	public List<GameObject> entities { get; set; }

	public POI( IntVector2 pos, List<GameObject> entities ) {
		this.pos = pos;
		this.entities = entities;
	}
}
