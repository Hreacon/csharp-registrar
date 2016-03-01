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
     public StudentTest()
     {
       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=harvard_test;Integrated Security=SSPI;";
     }
     public void Dispose()
     {
       Registrar.DeleteAll();
     }
  }
}
