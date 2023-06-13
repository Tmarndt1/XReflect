
namespace XReflect.Test
{
    public class FirstLevelTest
    {
        [Fact]
        public void First_Level_Map_Success()
        {
            // Arrange
            Student student1 = new Student()
            {
                Id = 1,
                Name = "Steve",
            };

            Student student2 = new Student()
            {
                Id = 1,
                Name = "Bob"
            };

            XMapper<Student> mapper = new XMapper<Student>((builder =>
            {
                builder.Map(x => x.Teachers).When((a, b) =>
                {
                    return a.Id == b.Id;
                });
            }));

            // Act
            mapper.Run(student1, student2);

            // Assert
            Assert.Equal("Steve", student2.Name);
        }

        [Fact]
        public void First_Level_Null_Target_Map_Success()
        {
            // Arrange
            Student student1 = new Student()
            {
                Id = 1,
                Name = "Steve",
            };

            Student student2 = default;

            XMapper<Student> mapper = new XMapper<Student>((builder =>
            {
                builder.Map(x => x.Teachers).When((a, b) =>
                {
                    return a.Id == b.Id;
                });
            }));

            Exception ex = null;

            // Act
            try
            {
                mapper.Run(student1, student2);
            }
            catch (ArgumentNullException argException)
            {
                ex = argException;
            }

            // Assert
            Assert.NotNull(ex);
            Assert.Null(student2);
        }
    }
}
