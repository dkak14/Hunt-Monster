using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreHpbar : Hpbar
{
    void Start() {
        EventManager<GameEvent>.Instance.AddListener(GameEvent.CoreHPChange, this, CorehpChange);
    }
    void CorehpChange(GameEvent eventType, Component sender, object param) {
        if (sender is Core) {
            Core player = (Core)sender;
            ChangeValue(player);
        }
    }
}
