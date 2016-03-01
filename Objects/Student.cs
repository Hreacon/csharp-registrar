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
  } // end class
} // end namespace
