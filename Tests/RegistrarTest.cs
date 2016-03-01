using Xunit;
using RegistrarNS.Objects;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RegistrarNS
{
  public class StudentTest : IDisposable
  {
    [Fact]
    public void Student_StudentHoldsOwnName()
    {
      Student testStudent = new Student("matt", new DateTime(2016,1,4));
      Assert.Equal("matt", testStudent.GetName());
    }

    [Fact]
    public void Equals_Override_Student()
    {
      Student testStudent = new Student("matt", new DateTime(2016,1,4));
      Student testStudent2 = new Student("matt", new DateTime(2016,1,4));
      Assert.Equal(testStudent2, testStudent);
    }

    [Fact]
    public void Student_GetAll()
    {
      Assert.Equal(0, Student.GetAll().Count);
    }

    [Fact]
    public void Student_Saves()
    {
      Student testStudent = new Student("matt", new DateTime(2016,1,4));
      testStudent.Save();
      Assert.Equal(1, Student.GetAll().Count);
    }

    [Fact]
    public void Student_FindsById()
    {
      Student test = new Student("matt", new DateTime(1991,8,8));
      test.Save();
      Assert.Equal(test, Student.Find(test.GetId()));
    }

    [Fact]
    public void Student_DeleteById()
    {
      Student test = new Student("matt", new DateTime(1991,8,8));
      test.Save();
      test.Delete();
      Assert.Equal(0, Student.GetAll().Count);
    }
    [Fact]
    public void Course_CourseHoldsOwnName()
    {
      Course testCourse = new Course("Basic History Lesson 5", "HIST100");
      Assert.Equal("Basic History Lesson 5", testCourse.GetName());
    }

    [Fact]
    public void Equals_Override_Course()
    {
      Course testCourse = new Course("Basic History Lesson 5", "HIST100");
      Course testCourse2 = new Course("Basic History Lesson 5", "HIST100");
      Assert.Equal(testCourse2, testCourse);
    }

    [Fact]
    public void Course_GetAll()
    {
      Assert.Equal(0, Course.GetAll().Count);
    }

    [Fact]
    public void Course_Saves()
    {
      Course testCourse = new Course("Basic History Lesson 5", "HIST100");
      testCourse.Save();
      Assert.Equal(1, Course.GetAll().Count);
    }

    [Fact]
    public void Course_FindsById()
    {
      Course test = new Course("Basic History Lesson 5", "HIST100");
      test.Save();
      Assert.Equal(test, Course.Find(test.GetId()));
    }

    [Fact]
    public void Course_DeleteById()
    {
      Course test = new Course("Basic History Lesson 5", "HIST100");
      test.Save();
      test.Delete();
      Assert.Equal(0, Course.GetAll().Count); // get all the courses using Course.Method, a static method call
    }
    [Fact]
    public void GetStudents_FromCourseById()
    {
      Course history = new Course("Basic History", "HIST100");
      history.Save();
      Student matt = new Student("matt", new DateTime(2016,1,4));
      matt.Save();
      history.AddStudent(matt.GetId());   // // matt is taking history, add matt to the specific history class (non static reference)
      Assert.Equal(matt, history.GetStudents()[0]);
    }
     public StudentTest()
     {
       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=harvard_test;Integrated Security=SSPI;";
     }
     public void Dispose()
     {
       Student.DeleteAll();
       Course.DeleteAll();
     }
  }
}
