using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;



namespace TrackingProject.WebService
{
    /// <summary>
    /// Summary description for TrackingService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TrackingService : System.Web.Services.WebService
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IDriverService _DriverService;

        public TrackingService(IDriverService driverService, IUnitOfWork uow)
        {
            _uow = uow;
            _DriverService = driverService;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string DriverLogin(string Username, string Password)
        {
            if (_DriverService.GetAll().Where(m => m.Username == Username && m.Password == Password).Any())
            {
                return "Yes";
            }
            else
            {
                return "No";
            }

        }
    }
}
