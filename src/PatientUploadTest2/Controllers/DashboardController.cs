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
            RolesToStrings.Add(Roles.Auditor, "������");
            RolesToStrings.Add(Roles.Doctor, "׫д��");
            RolesToStrings.Add(Roles.HospitalClient, "ҽԺ�û�");
            RolesToStrings.Add(Roles.SuperUser, "����Ա");



        }

        private void prepareView(ApplicationUser currentUser)
        {
            ViewData["PageTitle"] = String.Format("��ӭ����{0}��", currentUser.Name);
            ViewData["UserRole"] = String.Format("{0}", RolesToStrings[currentUser.Role]);
            ViewData["ManageAccountTitle"] = "�˻�����";
            ViewData["ManageAccountContent"] = "�༭��������������ȸ�����Ϣ";
            ViewData["ManageAccountButton"] = "ת���ҵ��˻�";
            ViewData["RedirectToAccount"] = "";
            ViewData["EnterWorkSpaceTitle"] = "�����б�";
            ViewData["EnterWorkSpaceContent"] = "���빤���б���ʼҵ����";
            ViewData["EnterWorkspaceButton"] = "��ʼ����";
            
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