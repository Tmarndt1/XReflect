﻿namespace XReflect.Test
{
    public class Teacher : IXReflectEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Classroom Classroom { get; set; }
    }
}
