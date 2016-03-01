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

     public StudentTest()
     {
       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=harvard_test;Integrated Security=SSPI;";
     }
     public void Dispose()
     {
       Student.DeleteAll();
     }
  }
}
