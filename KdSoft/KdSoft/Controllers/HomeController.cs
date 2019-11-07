using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KdSoft.Models;
using KdSoft.Engine;

namespace KdSoft.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            MetodosMail();
            return View();
        }

        private async Task MetodosMail ()
        {
            EngineProyect Funcion = new EngineProyect();
            //************************MAILCHIMP **********************************************************************************************************************************
            //await Funcion.LogMailChimp("https://us20.api.mailchimp.com/3.0/");
            //await Funcion.AddMemberMailChimp("https://us20.api.mailchimp.com/3.0/lists/c2af783892/members/");
            //await Funcion.GetToken("https://us20.api.mailchimp.com");
            //************************SENDIBLUE**********************************************************************************************************************************
            // await Funcion.CreateContactSendiBlue("https://api.sendinblue.com/v3/contacts");
            //await Funcion.GetAllContactSendiBlue("https://api.sendinblue.com/v3/contacts");
            //await Funcion.SendMailSendiBlue("https://api.sendinblue.com/v3/smtp/email");
            Funcion.EnviarEmail();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact( string username , string password)
        {


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
