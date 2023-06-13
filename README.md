# XReflect

XReflect is a C# .NET explicit mapping library that retains a target object's reference.

main: ![Build Status](https://github.com/Tmarndt1/XReflect/workflows/.NET/badge.svg?branch=main)

## Give a Star! :star:

If you like or are using this project please give it a star. Thanks!

## Example

```csharp

Mapper<Student> mapper = new Mapper<Student>(builder =>
{
    builder
        .Map(x => x.Teachers)
            .When((a, b) => a.Id == b.Id)
        .Access(x => x.Teachers)
            .Map(x => x.Classroom)
                .When((a, b) => a.Id == b.Id);
});

mapper.Run(student1, student2);

```

## Authors

- **Travis Arndt**

## License

This project is licensed under the MIT License - [LICENSE.md](LICENSE)
