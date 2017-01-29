using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic {
    public class ActionPlayerAbovePathNodeDetection : ActionBase {
#region attributes
        private GameObject m_targetPathNode = null;
#endregion

#region override methods
        public override bool Update() {
            return true;
        }
#endregion

#region custom methods
        public ActionPlayerAbovePathNodeDetection(GameObject owner, GameObject target_path_node) : base (owner) {
            m_actionType = ActionBase.ACTION_TYPE.Action_playerAbovePathNodeDetection;
            m_targetPathNode = target_path_node;
        }
#endregion
    }
}