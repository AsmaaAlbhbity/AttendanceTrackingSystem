﻿using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.IRepository
{
    public interface IRepoStudent
    {
        public List<Student> getAll();
        public Student getById(int id);
        public void Add(Student student);
        public void Update(Student student);
        public void Delete(int id);
    }
}