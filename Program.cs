using System.Collections.Generic;
using System.Linq;
using System;

namespace Merge
{
    class Program
    {
        private static List<EntityInstance> entityInstances = new List<EntityInstance>();
        private static List<MergeKey> mergeKeys = new List<MergeKey>();

        static void Main(string[] args)
        {
            var graphMerger = new GraphMerger();
            InitFields();
            entityInstances.ToList().ForEach(e => Console.WriteLine(e.ToString()));
            Console.WriteLine("----------------------------------------------------");
            var mergedEntitiyInstances = graphMerger.Merge(mergeKeys, entityInstances);
            mergedEntitiyInstances.ToList().ForEach(e => Console.WriteLine(e.ToString()));
        }

        static void InitFields()
        {
            entityInstances.Add(new EntityInstance() 
            {
                InstanceGuid = Guid.NewGuid(),
                ElementId = 54,
                Attributes = new List<Attribute>() 
                {
                    new Attribute() 
                    {
                        Id = 20,
                        Values = new List<string>()
                        {
                            "Mohammad"
                        } 
                    },
                    new Attribute()
                    {
                        Id = 21,
                        Values = new List<string>()
                        {
                            "Mahmodi"
                        }
                    }
                }
            });
            entityInstances.Add(new EntityInstance() 
            {
                InstanceGuid = Guid.NewGuid(),
                ElementId = 54,
                Attributes = new List<Attribute>() 
                {
                    new Attribute() 
                    {
                        Id = 20,
                        Values = new List<string>()
                        {
                            "Amir"
                        } 
                    },
                    new Attribute()
                    {
                        Id = 21,
                        Values = new List<string>()
                        {
                            "Shokohi"
                        }
                    }
                }
            });
            entityInstances.Add(new EntityInstance() 
            {
                InstanceGuid = Guid.NewGuid(),
                ElementId = 54,
                Attributes = new List<Attribute>() 
                {
                    new Attribute() 
                    {
                        Id = 20,
                        Values = new List<string>()
                        {
                            "Ali"
                        } 
                    },
                    new Attribute()
                    {
                        Id = 21,
                        Values = new List<string>()
                        {
                            "Mahmodi",
                            "Shokohi"
                        }
                    }
                }
            });
            mergeKeys.Add(new MergeKey()
            {
                ElementId = 54,
                AttributeIds = new List<long>() { 21 }
            });
        }
    }
}
