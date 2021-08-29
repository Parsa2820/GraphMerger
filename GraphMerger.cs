using System;
using System.Collections.Generic;

namespace Merge
{
    public class GraphMerger
    {
        /// <summary>
        /// Merge incoming instances based on every key in the mergeKeys list.
        /// </summary>
        /// <param name="mergeKeys">list of keys that instances are supposed to be merged based on them.</param>
        /// <param name="unmergedInsances">random graph of data</param>
        /// <returns>merged instances based on all of the keys in mergeKeys list</returns>
        public IEnumerable<BaseInstance> Merge(IEnumerable<MergeKey> mergeKeys, IEnumerable<BaseInstance> unmergedInsances)
        {
            // your code here!
            throw new NotImplementedException();
        }
    }
}