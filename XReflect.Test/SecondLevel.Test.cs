
namespace XReflect.Test
{
    public class SecondLevelTest
    {
        [Fact]
        public void Second_Level_Map_Success()
        {
            // Arrange
            Student student1 = new Student()
            {
                Id = 1,
                Name = "Steve",
                Teachers = new List<Teacher>()
                {
                    new Teacher()
                    {
                        Id = 1,
                        Name = "Mr. Williams",
                    },
                    new Teacher()
                    {
                        Id = 2,
                        Name = "Ms. Smith",
                    }
                }
            };

            Student student2 = new Student()
            {
                Id = 1,
                Name = "Bob",
                Teachers = new List<Teacher>()
                {
                    new Teacher()
                    {
                        Id = 1,
                        Name = "Mr. Williams",
                    }
                }
            };

            XMapper<Student> mapper = new XMapper<Student>((builder =>
            {
                builder
                    .Map(x => x.Teachers).When((a, b) =>
                    {
                        return a.Id == b.Id;
                    });
            }));

            // Act
            mapper.Run(student1, student2);

            // Assert
            Assert.Equal("Steve", student2.Name);
            Assert.Equal(2, student2.Teachers.Count);
        }

        [Fact]
        public void Second_Level_Null_Target_Map_Success()
        {
            // Arrange
            Student student1 = new Student()
            {
                Id = 1,
                Name = "Steve",
                Teachers = new List<Teacher>()
                {
                    new Teacher()
                    {
                        Id = 1,
                        Name = "Mr. Williams",
                    },
                    new Teacher()
                    {
                        Id = 2,
                        Name = "Ms. Smith",
                    }
                }
            };

            Student student2 = new Student()
            {
                Id = 1,
                Name = "Bob",
                Teachers = new List<Teacher>()
                {
                    default
                }
            };


            XMapper<Student> mapper = new XMapper<Student>((builder =>
            {
                builder
                    .Map(x => x.Teachers).When((a, b) =>
                    {
                        return a.Id == b.Id;
                    });
            }));

            // Act
            mapper.Run(student1, student2);

            // Assert
            Assert.Equal("Steve", student2.Name);
            Assert.Equal(3, student2.Teachers.Count);
        }

        [Fact]
        public void Second_Level_IgnoreNull_Source_Map_Success()
        {
            // Arrange
            Student student1 = new Student()
            {
                Id = 1,
                Name = "Steve",
                Teachers = new List<Teacher>()
                {
                    new Teacher()
                    {
                        Id = 1,
                        Name = "Mr. Williams",
                    },
                    default
                }
            };

            Student student2 = new Student()
            {
                Id = 1,
                Name = "Bob",
                Teachers = new List<Teacher>()
                {
                    new Teacher()
                    {
                        Id = 2,
                        Name = "Ms. Smith",
                    }
                }
            };


            XMapper<Student> mapper = new XMapper<Student>((builder =>
            {
                builder
                    .Map(x => x.Teachers).When((a, b) =>
                    {
                        return a.Id == b.Id;
                    });
            }));

            // Act
            mapper.Run(student1, student2);

            // Assert
            Assert.Equal("Steve", student2.Name);
            Assert.Equal(2, student2.Teachers.Count);
        }

        [Fact]
        public void Second_Level_Null_Source_Map_Success()
        {
            // Arrange
            Student student1 = new Student()
            {
                Id = 1,
                Name = "Steve",
                Teachers = new List<Teacher>()
                {
                    new Teacher()
                    {
                        Id = 1,
                        Name = "Mr. Williams",
                    },
                    default
                }
            };

            Student student2 = new Student()
            {
                Id = 1,
                Name = "Bob",
                Teachers = new List<Teacher>()
                {
                    new Teacher()
                    {
                        Id = 2,
                        Name = "Ms. Smith",
                    }
                }
            };

            XMapper<Student> mapper = new XMapper<Student>((builder =>
            {
                builder
                    .Map(x => x.Teachers).When((a, b) =>
                    {
                        return a.Id == b.Id;
                    })
                    .Configure(new XReflectConfiguration()
                    {
                        CollectionOption = CollectionOption.AddRemove,
                        IgnoreNull = false
                    });
            }));


            // Act
            mapper.Run(student1, student2);

            // Assert
            Assert.Equal("Steve", student2.Name);
            Assert.Equal(3, student2.Teachers.Count);
        }
    }
}
