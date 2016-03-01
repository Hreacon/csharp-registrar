using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RegistrarNS.Objects
{
  public class Course
  {
    private int _id;
    private string _name;
    private string _courseNumber;

    public Course(string name, string courseNumber, int id =0)
    {
      _id = id;
      _name = name;
      _courseNumber = courseNumber;
    }
    public override bool Equals(System.Object otherCourse)
    {
      if (!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course testCourse = (Course) otherCourse;
        bool idEquality =this.GetId() == testCourse.GetId();
        bool nameEquality = this.GetName() == testCourse.GetName();
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
    public string GetCourseNumber()
    {
      return _courseNumber;
    }
    public void SetCourseNumber(string courseNumber)
    {
      _courseNumber = courseNumber;
    }

    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course testCourse = new Course(courseName, courseNumber, courseId);
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
    public List<Student> GetStudents()
    {
      List<Student> allStudents = new List<Student>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT students.* FROM courses JOIN class ON (courses.id = class.course_id) JOIN students ON (class.student_id = students.id) WHERE courses.id = @parameterId", conn);

      SqlParameter idParam = new SqlParameter();
      idParam.ParameterName = "@parameterId";
      idParam.Value = this.GetId();

      cmd.Parameters.Add(idParam);

      rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        // get the student data from the database
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime date = rdr.GetDateTime(2);
         // make the student object`
         Student testStudent = new Student(studentName, date, studentId);
         // add the student object to the list
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
    public void AddStudent(int studentId)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO class (student_id, course_id) OUTPUT INSERTED.id VALUES (@StudentId, @CourseId)",conn);

      SqlParameter studentIdParam = new SqlParameter();
      studentIdParam.ParameterName = "@StudentId";
      studentIdParam.Value = studentId;

      SqlParameter CourseIdParam = new SqlParameter();
      CourseIdParam.ParameterName = "@CourseId";
      CourseIdParam.Value = this.GetId();

      cmd.Parameters.Add(studentIdParam);
      cmd.Parameters.Add(CourseIdParam);

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
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses (name, courseNumber) OUTPUT INSERTED.id VALUES (@CourseName, @CourseNumber)", conn);

      SqlParameter nameParam = new SqlParameter();
      nameParam.ParameterName = "@CourseName";
      nameParam.Value = this.GetName();

      SqlParameter dateParam = new SqlParameter();
      dateParam.ParameterName = "@CourseNumber";
      dateParam.Value = this.GetCourseNumber();

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
    // Course course1 = Course.Find(1);

    public static Course Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId", conn);

      SqlParameter idParam = new SqlParameter();
      idParam.ParameterName = "@CourseId";
      idParam.Value = id;

      cmd.Parameters.Add(idParam);

      rdr = cmd.ExecuteReader();

      int foundCourseId = 0;
      string foundCourseName = null;
      string foundCourseNumber = null;

      while(rdr.Read())
      {
        foundCourseId = rdr.GetInt32(0);
        foundCourseName = rdr.GetString(1);
        foundCourseNumber = rdr.GetString(2);
      }
      Course foundCourse = new Course(foundCourseName, foundCourseNumber, foundCourseId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCourse;
    }
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("Delete From courses WHERE id = @CourseId", conn);

      SqlParameter idParam = new SqlParameter();
      idParam.ParameterName = "@CourseId";
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

      SqlCommand cmd = new SqlCommand("Delete FROM courses", conn);

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
