using UnityEngine;
using System.Collections;

public class IntVector2 {

	public int x { get; private set; }
	public int y { get; private set; }

	public IntVector2( int x, int y ) {
		this.x = x;
		this.y = y;
	}
}
