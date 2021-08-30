using System;
using System.Linq;
using System.Collections.Generic;

namespace Merge
{
    public class GraphMerger
    {
        /// <summary>
        /// Merge incoming instances based on every key in the mergeKeys list.
        /// </summary>
        /// <param name="mergeKeys">list of keys that instances are supposed to be merged based on them.</param>
        /// <param name="unmergedInstances">random graph of data</param>
        /// <returns>merged instances based on all of the keys in mergeKeys list</returns>
        public IEnumerable<BaseInstance> Merge(IEnumerable<MergeKey> mergeKeys, IEnumerable<BaseInstance> unmergedInstances)
        {
            var unmergedInstanceList = unmergedInstances.ToList();
            var mergeKeysByElementId = mergeKeys.ToDictionary(key => key.ElementId, key => key.AttributeIds.ToHashSet());
            var keyUniqueValuesByAttributeIdByElementId = new Dictionary<long, Dictionary<long, HashSet<string>>>();
            var mergedInstances = new List<BaseInstance>();

            // Get all unique values for each key
            foreach (var unmergedInstance in unmergedInstanceList)
            {
                if (mergeKeysByElementId.TryGetValue(unmergedInstance.ElementId, out var attributeIds))
                {
                    foreach (var attribute in unmergedInstance.Attributes)
                    {
                        var attributeKeysValuesCount = 0;

                        if (attributeIds.Contains(attribute.Id))
                        {
                            attributeKeysValuesCount += attribute.Values.Count;

                            if (keyUniqueValuesByAttributeIdByElementId.TryGetValue(unmergedInstance.ElementId, out var uniqueValuesByAttributeId))
                            {
                                if (uniqueValuesByAttributeId.TryGetValue(attribute.Id, out var uniqueValues))
                                {
                                    uniqueValues.Union(attribute.Values.ToHashSet());
                                }
                                else
                                {
                                    uniqueValuesByAttributeId.Add(attribute.Id, attribute.Values.ToHashSet());
                                }
                            }
                            else
                            {
                                keyUniqueValuesByAttributeIdByElementId.Add(unmergedInstance.ElementId, new Dictionary<long, HashSet<string>> { { attribute.Id, attribute.Values.ToHashSet() } });
                            }
                        }

                        if (attributeKeysValuesCount == 0)
                        {
                            mergedInstances.Add(unmergedInstance);
                        }
                    }
                }
                else
                {
                    mergedInstances.Add(unmergedInstance);
                }
            }

            unmergedInstanceList = unmergedInstanceList.Except(mergedInstances).ToList();

            // Merge instances based on unique values
            foreach (var elementId in keyUniqueValuesByAttributeIdByElementId.Keys)
            {
                foreach (var attributeId in keyUniqueValuesByAttributeIdByElementId[elementId].Keys)
                {
                    foreach (var keyValue in keyUniqueValuesByAttributeIdByElementId[elementId][attributeId])
                    {
                        var sameInstances = new List<BaseInstance>();

                        foreach (var unmergedInstance in unmergedInstanceList)
                        {
                            if (DoesContainsKeyValue(unmergedInstance, elementId, attributeId, keyValue))
                            {
                                sameInstances.Add(unmergedInstance);
                            }
                        }

                        if (sameInstances.Count != 0)
                        {
                            mergedInstances.Add(UnionSameInstances(sameInstances));
                            unmergedInstanceList.Except(sameInstances);
                        }
                    }
                }
            }

            mergedInstances.AddRange(unmergedInstanceList);
            return mergedInstances;
        }

        public bool DoesContainsKeyValue(BaseInstance unmergedInstance, long elementId, long attributeId, string keyValue)
        {
            return unmergedInstance.ElementId == elementId && unmergedInstance.Attributes.Any(attribute => attribute.Id == attributeId && attribute.Values.Contains(keyValue));
        }

        public BaseInstance UnionSameInstances(List<BaseInstance> sameInstances)
        {
            var mergedInstance = new EntityInstance
            {
                InstanceGuid = Guid.NewGuid(),
                ElementId = sameInstances.First().ElementId,
                Attributes = new List<Attribute>()
            };

            foreach (var sameInstance in sameInstances)
            {
                foreach (var attribute in sameInstance.Attributes)
                {
                    if (!mergedInstance.Attributes.Any(attributeInMergedInstance => attributeInMergedInstance.Id == attribute.Id))
                    {
                        mergedInstance.Attributes.Add(new Attribute
                        {
                            Id = attribute.Id,
                            Values = new List<string>(attribute.Values)
                        });
                    }
                    else
                    {
                        var mergedAttribute = mergedInstance.Attributes.First(attributeInMergedInstance => attributeInMergedInstance.Id == attribute.Id);
                        mergedAttribute.Values.Union(attribute.Values);
                    }
                }
            }

            return mergedInstance;
        }
    }


}