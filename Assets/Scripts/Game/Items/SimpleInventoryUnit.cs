using System;

namespace Game.Items
{
    [Serializable]
    public class SimpleInventoryUnit : ItemInventoryUnit
    {
        public override string SubDataInfo => null;

        protected override Type ClassType => typeof(SimpleInventoryUnit);
    }
}