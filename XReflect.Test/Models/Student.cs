﻿namespace XReflect.Test
{
    public class Student : IXReflectEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
