using Nancy;
using RegistrarNS.Objects;
using System.Collections.Generic;
namespace RegistrarNS
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["header.cshtml"];
      };
    }
  }
}
