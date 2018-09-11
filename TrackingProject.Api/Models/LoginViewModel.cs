using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Api.Models
{
    public class LoginViewModel
    {
        public User User { get; set; }
        public bool RememberMe { get; set; }
        public string Message { get; set; }
    }
}