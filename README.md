# GraphMerger
Merge logic: two instances merge to one instance if they have the same ElementId and that ElementId exists in the mergeKeys input of [GraphMerger](https://github.com/mohammad4x/GraphMerger/blob/main/GraphMerger.cs)'s Merge method. There is two
additional conditions to that merge. One that particular MergeKey has a list of AttributeIds that these two instances must contain all of those attributes. The second condition
is that at least one combination of the first instance's key attribute values should match at least one combination of the second instance's key attribute values. Let me show you an example:

```csharp
var instance1 = new EntityInstance
{
    ElementId = 100,
    Attributes = new List<Attribute>
    {
        new Attribute
        {
            Id = 1,
            Values = new List<string> { "David", "Charles" }
        },
        new Attribute
        {
            Id = 2,
            Values = new List<string> { "000", "555" }
        },
        new Attribute
        {
            Id = 3,
            Values = new List<string> { "London", "Paris" }
        }
   }
};

var instance2 = new EntityInstance
{
    ElementId = 100,
    Attributes = new List<Attribute>
    {
        new Attribute
        {
            Id = 1,
            Values = new List<string> { "David", "Robert" }
        },
        new Attribute
        {
            Id = 2,
            Values = new List<string> { "000", "111", "222" }
        },
        new Attribute
        {
            Id = 3,
            Values = new List<string> { "Paris" }
        }
    }
};
```

After calling the [GraphMerger](https://github.com/mohammad4x/GraphMerger/blob/main/GraphMerger.cs)'s Merge method
```csharp 
var mergeKeys = new List<MergeKey> 
{
    new MergeKey 
    {
        ElementId = 100,
        AttributeIds = new List<long> { 1, 3}
    }
};
            
var mergedInstances = new GraphMerger().Merge(mergeKeys, new List<BaseInstance> { instance1, instance2 });
```

mergedInstances should have one element and its value should be like this
```csharp
{
    ElementId = 100,
    Attributes = new List<Attribute> 
    {
        new Attribute 
        {
            Id = 1,
            Values = new List<string> { "David", "Charles", "Robert" }
        },
        new Attribute
        {
            Id = 2,
            Values = new List<string> { "000", "111", "222", "555" }
        },
        new Attribute 
        {
            Id = 3,
            Values = new List<string> { "London", "Paris" }
        }
    }
};
```

There could be thousands of BaseInstances as unmergedInstances and dozens of mergeKeys from input for [GraphMerger](https://github.com/mohammad4x/GraphMerger/blob/main/GraphMerger.cs)'s Merge method. these instances could be entity or link.
 
 **Notes** for link sources and targets:
 
 *before merge* : Source and target entities are guaranteed to exist in the input list as a standalone EntityInstance.
 
 *after merge* : The above rule must be considered for the result and every link's source and target should point to the final merged EntityInstance(if merged).
 
 **P.S**
 For simplicity, we don't merge any links. We assume all of the mergeKeys coming to the Merge method have an ElementId that only will be found in **EntityInstance**s
