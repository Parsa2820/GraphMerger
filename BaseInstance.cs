using System.Collections.Generic;

namespace Merge
{
    public abstract class BaseInstance
    {
        public Guid InstanceGuid { get; set; }
        public long ElementId { get; set; }
        public List<Attribute> Attributes { get; set; }
    }
}
