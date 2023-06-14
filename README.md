# XReflect

XReflect is a C# .NET explicit mapping library that retains a target object's reference. 
<br/>
<br/>
When retrieving an object from an ORM like EntityFramework, typically the ORM tracks the entity and any related entities for efficiency to detect changes that are made and build the query. Therefore, it is critical to maintain the reference to the entities when making changes. Through configuration, XReflect has the ability to modify collections by adding, and or removing entities from the persistance layer.

main: ![Build Status](https://github.com/Tmarndt1/XReflect/workflows/.NET/badge.svg?branch=main)

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

## Basic Example
In this example, a `Student` has a `Teachers` collection and the `Teacher` has a related `Classroom` entity. 
<br/>
When mapping the source `student1` to the target `student2` the references in memory on `student2` will remain unchanged. 
```csharp

XMapper<Student> mapper = new XMapper<Student>(builder =>
{
    builder
        .Map(x => x.Teachers) // Maps the Teachers collection.
            .When((a, b) => a.Id == b.Id) // Maps the Teacher object when the Id property matches.
        .Access(x => x.Teachers) // Access the Teachers collection.
            .Map(x => x.Classroom) // Maps the Classroom object on the Teachers collection
                .When((a, b) => a.Id == b.Id) // Maps the Classroom object when the Id property matches.
});

mapper.Run(student1, student2);

```

## Configuration Example
In this example, the same relationships exist as the Basic Example between `Student`, `Teacher`, and `Classroom`. 
<br/>
The XReflect configuration can be modified by specifying to only add new entities through `CollectionOption.Add`.
<br/>
The XReflect configuration can be modified by specifying to only remove new entities through `CollectionOption.Remove`.
<br/>
By default the XReflect configuration is `CollectionOption.AddRemove` which adds and removes entities in collections.
```csharp

XMapper<Student> mapper = new XMapper<Student>((builder =>
{
    builder
        .Map(x => x.Teachers) // Maps the Teachers collection.
            .When((a, b) => a.Id == b.Id) // Maps the Teacher object when the Id property matches.
        .Configure(new XReflectConfiguration()
        {
            // Default configuration is AddRemove.
            CollectionOption = CollectionOption.Add, // Will only add new objects and won't remove existing in collection.
            // Default configuration is true.
            IgnoreNull = false // Will ignore null values in a source's collection while mapping.
        });
}));


// Act
mapper.Run(student1, student2);

```

## Authors

- **Travis Arndt**

## License

This project is licensed under the MIT License - [LICENSE.md](LICENSE)
