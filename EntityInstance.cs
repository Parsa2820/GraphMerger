using System.Linq;

namespace Merge
{
    public class EntityInstance : BaseInstance
    {
        public override string ToString()
        {
            var attrs = string.Join(" | ", base.Attributes
                .Select(a => $"{a.Id}: {string.Join(", ", a.Values)}"));
            return $"GUID = {base.InstanceGuid} & "
                + $"ElementId = {base.ElementId} & "
                + $"Attributes = {attrs}";
        }
    }
}