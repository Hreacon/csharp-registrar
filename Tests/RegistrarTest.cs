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
      Student testStudent = new Student("matt", new DateTime(1/4/2016));
      Assert.Equal("matt", testStudent.GetName());
    }
     public StudentTest()
     {
       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=harvard_test;Integrated Security=SSPI;";
     }
     public void Dispose()
     {
      //  Student.DeleteAll();
     }
  }
}
