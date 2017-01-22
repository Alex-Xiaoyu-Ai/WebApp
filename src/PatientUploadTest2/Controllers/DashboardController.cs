using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PatientUploadTest2.Models;
using Microsoft.AspNetCore.Identity;

namespace PatientUploadTest2.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private UserManager<ApplicationUser> _usrManager;
        private Dictionary<Roles, String> RolesToStrings = new Dictionary<Roles, string>();

        public DashboardController(UserManager<ApplicationUser> usrManager)
        {
            _usrManager = usrManager;
            RolesToStrings.Add(Roles.Auditor, "审阅人");
            RolesToStrings.Add(Roles.Doctor, "撰写人");
            RolesToStrings.Add(Roles.HospitalClient, "医院用户");
            RolesToStrings.Add(Roles.SuperUser, "管理员");



        }

        private void prepareView(ApplicationUser currentUser)
        {
            ViewData["PageTitle"] = String.Format("欢迎您，{0}！", currentUser.Name);
            ViewData["UserRole"] = String.Format("{0}", RolesToStrings[currentUser.Role]);
            ViewData["ManageAccountTitle"] = "账户管理";
            ViewData["ManageAccountContent"] = "编辑、更新您的密码等个人信息";
            ViewData["ManageAccountButton"] = "转到我的账户";
            ViewData["RedirectToAccount"] = "";
            ViewData["EnterWorkSpaceTitle"] = "工作列表";
            ViewData["EnterWorkSpaceContent"] = "进入工作列表，开始业务处理";
            ViewData["EnterWorkspaceButton"] = "开始工作";
            
            switch (currentUser.Role)
            {
                case Roles.SuperUser:
                    {
                        ViewData["RedirectToWorkSpace"] = "/Accounts/Register";
                    }
                    break;
                case Roles.Doctor:
                    {
                        ViewData["RedirectToWorkSpace"] = "/Reports";
                    }
                    break;
                case Roles.HospitalClient:
                    {
                        ViewData["RedirectToWorkSpace"] = "/Patients";
                    }
                    break;
                case Roles.Auditor:
                    {
                        ViewData["RedirectToWorkSpace"] = "/Reports";
                    }
                    break;
            }
        }


        public IActionResult Index()
        {
            var currentUser = _usrManager.GetUserAsync(HttpContext.User).Result;
            prepareView(currentUser);
            return View();
            
        }

        
    }
}