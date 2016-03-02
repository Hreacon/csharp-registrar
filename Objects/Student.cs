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
    public List<Course> GetCoursesNotInStudent()
    {
      List<Course> allCourses = new List<Course>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT courses.* FROM courses JOIN class ON (courses.id = class.course_id) JOIN students ON (class.student_id = students.id) WHERE students.id = @parameterId", conn);

      SqlParameter idParam = new SqlParameter();
      idParam.ParameterName = "@parameterId";
      idParam.Value = this.GetId();

      cmd.Parameters.Add(idParam);

      rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        // get the student data from the database
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
         // make the course object`
         Course newCourse = new Course(courseName, courseNumber, courseId);
         // add the student object to the list
         allCourses.Add(newCourse);
      }
      rdr.Close();
      cmd = new SqlCommand("SELECT * FROM courses", conn);

      rdr = cmd.ExecuteReader();
      List<Course> output = new List<Course>(){};
      while(rdr.Read())
      {
        // foreach student the reader is bringing in loop through students in the course
        int id = rdr.GetInt32(0);
        bool found = false;
        foreach(Course s in allCourses)
        {
          if( s.GetId() == id )
            found = true;
        }
        if(!found)
          output.Add(new Course(rdr.GetString(1), rdr.GetString(2), id));
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return output;
    }
    public List<Course> GetCourses()
    {
      List<Course> allCourses = new List<Course>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT courses.* FROM courses JOIN class ON (courses.id = class.course_id) JOIN students ON (class.student_id = students.id) WHERE students.id = @parameterId", conn);

      SqlParameter idParam = new SqlParameter();
      idParam.ParameterName = "@parameterId";
      idParam.Value = this.GetId();

      cmd.Parameters.Add(idParam);

      rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        // get the student data from the database
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
         // make the student object`
         Course testCourse = new Course(courseName, courseNumber, courseId);
         // add the student object to the list
         allCourses.Add(testCourse);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCourses;
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
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("Delete From students WHERE id = @StudentId", conn);

      SqlParameter idParam = new SqlParameter();
      idParam.ParameterName = "@StudentId";
      idParam.Value = this.GetId();

      cmd.Parameters.Add(idParam);

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
