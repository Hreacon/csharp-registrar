using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RegistrarNS.Objects
{
  public class Student
  {
    private int _id;
    private string _name;
    private DateTime _date;

    public Student(string name, DateTime date, int id =0)
    {
      _id = id;
      _name = name;
      _date = date;
    }
    public override bool Equals(System.Object otherStudent)
    {
      if (!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student testStudent = (Student) otherStudent;
        bool idEquality =this.GetId() == testStudent.GetId();
        bool nameEquality = this.GetName() == testStudent.GetName();
        return (idEquality && nameEquality);
      }
    }
    public int GetId()
    {
      return _id;
    }
    public void SetId(int id)
    {
      _id = id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string name)
    {
      _name = name;
    }
    public DateTime GetDate()
    {
      return _date;
    }
    public void SetDate(DateTime date)
    {
      _date = date;
    }

    public static List<Student> GetAll()
    {
      List<Student> allStudents = new List<Student>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime StudentDateTime = rdr.GetDateTime(2);
        Student testStudent = new Student(studentName, StudentDateTime, studentId);
        allStudents.Add(testStudent);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStudents;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO students (name, date) OUTPUT INSERTED.id VALUES (@StudentName, @StudentDate)", conn);

      SqlParameter nameParam = new SqlParameter();
      nameParam.ParameterName = "@StudentName";
      nameParam.Value = this.GetName();

      SqlParameter dateParam = new SqlParameter();
      dateParam.ParameterName = "@StudentDate";
      dateParam.Value = this.GetDate();

      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(dateParam);


      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    // Student student1 = Student.Find(1);

    public static Student Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @StudentId", conn);

      SqlParameter idParam = new SqlParameter();
      idParam.ParameterName = "@StudentId";
      idParam.Value = id;

      cmd.Parameters.Add(idParam);

      rdr = cmd.ExecuteReader();

      int foundStudentId = 0;
      string foundStudentName = null;
      DateTime foundStudentdate = new DateTime(2000,1,1);

      while(rdr.Read())
      {
        foundStudentId = rdr.GetInt32(0);
        foundStudentName = rdr.GetString(1);
        foundStudentdate = rdr.GetDateTime(2);
      }
      Student foundStudent = new Student(foundStudentName, foundStudentdate, foundStudentId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStudent;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("Delete FROM students", conn);

      rdr = cmd.ExecuteReader();

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
  } // end class
} // end namespace
