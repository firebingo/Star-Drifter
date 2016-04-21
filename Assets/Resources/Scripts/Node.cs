using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {

	public IntVector2 pos { get; set; }
	public List<GameObject> entities { get; set; }

	public Node(IntVector2 pos, List<GameObject> entities) {
		this.pos = pos;
		this.entities = entities;
	}
}
