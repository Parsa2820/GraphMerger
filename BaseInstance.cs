using System.Collections.Generic;
using System;

namespace Merge
{
    public abstract class BaseInstance
    {
        public Guid InstanceGuid { get; set; }
        public long ElementId { get; set; }
        public List<Attribute> Attributes { get; set; }
    }
}
