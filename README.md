# XReflect

XReflect is a C# .NET explicit mapping library that specializes in mapping from a `source` object to a `target` object while preserving the target's references. This library is particularly useful when dealing with object-relational mapping (ORM) frameworks like Entity Framework.
<br/>
<br/>
When retrieving an object from an ORM such as Entity Framework, the ORM keeps track of the entity and any associated entities for efficiency purposes. This tracking mechanism allows the ORM to detect changes made to the entities and construct optimized queries.
<br/>
<br/>
To ensure that the references to the entities are maintained during modification, XReflect offers configuration options. By configuring XReflect appropriately, you can add or remove entities from the persistence layer while preserving the necessary references.

main: ![Build Status](https://github.com/Tmarndt1/XReflect/workflows/.NET/badge.svg?branch=main)

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

## Features
* Simple and intuitive API for defining mapping rules
* Support for mapping properties at different levels of object hierarchy
* Conditional mapping based on custom predicates
* Ignoring properties during mapping
* Easy integration into existing projects
* Lightweight and performant

## Usage
To use XReflect in your project, follow these steps:

1. Define your source and destination objects.
2. Configure the mapping rules using the XMapper fluent API.
3. Perform the object mapping using the Run method.

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
In this example, the same relationships exist as the **Basic Example** between `Student`, `Teacher`, and `Classroom`. 
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
## Contributing
Contributions to XReflect are welcome! If you find any issues or have suggestions for improvements, please create a new issue or submit a pull request.

## Authors

- **Travis Arndt**

## License

This project is licensed under the MIT License - [LICENSE.md](LICENSE)
