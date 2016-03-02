using Nancy;
using RegistrarNS.Objects;
using System.Collections.Generic;
using System;

namespace RegistrarNS
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        Dictionary<string,object> model = new Dictionary<string,object>(){};
        model.Add("courses", Course.GetAll());
        model.Add("students", Student.GetAll());
        return View["viewEverything.cshtml", model];
      };
      Get["/course/{id}"] = parameters => {
        return View["viewCourse.cshtml", Course.Find(int.Parse(parameters.id))];
      };
      Get["/course/{cid}/add/{sid}"] = parameters => {
        Course.Find(int.Parse(parameters.cid)).AddStudent(int.Parse(parameters.sid));
        return View["forward.cshtml", "/course/"+int.Parse(parameters.cid)];
      };
      Get["/student/{sid}/add/{cid}"] = parameters => {
        Course.Find(int.Parse(parameters.cid)).AddStudent(int.Parse(parameters.sid));
        return View["forward.cshtml", "/student/"+int.Parse(parameters.sid)];
      };
      Get["/student/{id}"] = parameters => {
        return View["viewStudent.cshtml", Student.Find(int.Parse(parameters.id))];
      };
      Post["/addStudent"] = _ => {
        DateTime date = Request.Form["studentDate"];
        string name = Request.Form["studentName"];
        new Student(name, date).Save();
        return View["forward.cshtml", "/"];
      };
      Post["/addCourse"] = _ => {
        string courseNumber = Request.Form["courseNumber"];
        string name = Request.Form["courseName"];
        new Course(name, courseNumber).Save();
        return View["forward.cshtml", "/"];
      };
    }
  }
}
