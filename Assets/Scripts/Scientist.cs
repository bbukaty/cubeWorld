using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist: CubeObject {

	public override void getMoveConsequences() {
		base.getMoveConsequences();
		if (levelManager.getCubeObjIn(levelPos + Vector3Int.down) is GoalBlock) {
			levelManager.win();
		}
	}
}
