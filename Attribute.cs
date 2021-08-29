using System.Collections.Generic;

namespace Merge
{
    public class Attribute
    {
        public long Id { get; set; }
        /// <summary>
        /// list of string values
        /// </summary>
        /// <value>"David", "Charles", ...</value>
        public List<string> Values { get; set; }
    }
}