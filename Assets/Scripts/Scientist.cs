using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist: Character {

	protected override void die() {
		Destroy(gameObject);
	}

	protected override void getMoveConsequences() {
		base.getMoveConsequences();
		if (levelManager.getBlockUnder(levelPos) is GoalBlock) {
			levelManager.win();
		}
	}
}
