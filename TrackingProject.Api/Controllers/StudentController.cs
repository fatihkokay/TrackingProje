using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Api.Models;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.StudentServices;

namespace TrackingProject.Api.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _StudentService;

        public StudentController(IStudentService studentService, IUnitOfWork uow) : base(uow)
        {
            _StudentService = studentService;
        }
    }
}