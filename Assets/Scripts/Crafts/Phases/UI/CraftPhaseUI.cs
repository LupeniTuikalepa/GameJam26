using UnityEngine;

namespace Crafts.Phases.UI
{
    public abstract class CraftPhaseUI : MonoBehaviour
    {
        private CraftPhase phase;

        public virtual void Bind(CraftPhase phase)
        {
            this.phase = phase;
        }

        public virtual void Unbind(CraftPhase phase)
        {
            if (this.phase == phase)
                this.phase = null;
        }

        public void Confirm()
        {
            
        }

        public void Cancel()
        {

        }
    }
}