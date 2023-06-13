using System.Diagnostics;

namespace XReflect.Test
{
    public class ThirdLevelTest
    {
        [Fact]
        public void Third_Level_Map_Success()
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
                        Classroom = new Classroom()
                        {
                            Id = 1,
                            RoomNumber = 1,
                        }
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
                        Classroom = new Classroom()
                        {
                            Id = 2,
                            RoomNumber = 2,
                        }
                    }
                }
            };

            XMapper<Student> mapper = new XMapper<Student>(builder =>
            {
                builder
                    .Map(x => x.Teachers)
                        .When((a, b) => a.Id == b.Id)
                    .Access(x => x.Teachers)
                        .Map(x => x.Classroom)
                            .When((a, b) => a.Id == b.Id);
            });

            // Act
            mapper.Run(student1, student2);

            // Assert
            Assert.Equal("Steve", student2.Name);
            Assert.Equal(1, student2.Teachers.First().Classroom.RoomNumber);
        }

        [Fact]
        public void Third_Level_Ignore_Success()
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
                        Classroom = new Classroom()
                        {
                            Id = 1,
                            RoomNumber = 1,
                        }
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
                        Classroom = new Classroom()
                        {
                            Id = 2,
                            RoomNumber = 2,
                        }
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
            Assert.Equal(2, student2.Teachers.First().Classroom.RoomNumber);
        }
    }
}
