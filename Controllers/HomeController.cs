using SMTP_Tester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SMTP_Tester.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Index(Mail _objModelMail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(_objModelMail.To);
                    mail.From = new MailAddress(_objModelMail.From);
                    mail.Subject = "E-Mail by SMTP Tester";
                    mail.Body = "Your SMTP worked!<br>" + "This mail was sent to test SMTP for " + _objModelMail.From;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(_objModelMail.From, _objModelMail.Password);
                    smtp.EnableSsl = true;
                    if (!string.IsNullOrEmpty(_objModelMail.Host))
                    {
                        smtp.Host = _objModelMail.Host;
                    }
                    if (!string.IsNullOrEmpty(_objModelMail.Port))
                    {
                        smtp.Port = Convert.ToInt32(_objModelMail.Port);
                    }

                    smtp.Send(mail);
                }
                catch(Exception e)
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Unknown error! try after sometime");
                    return View("Index", _objModelMail);
                }
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Email sent successfully to " + _objModelMail.To);
                ModelState.Clear();
                return View(new Mail());
            }
            else
            {
                return View();
            }
        }

        public ActionResult Settings()
        {
            Mail mail = new Mail();
            var reqCookies = Request.Cookies["Mail"];
            if (reqCookies != null)
            {
                mail.Host = reqCookies["Host"].ToString();
                mail.Port = reqCookies["Port"].ToString();
                mail.To = reqCookies["To"].ToString();
                mail.From = reqCookies["From"].ToString();
                mail.Password = reqCookies["Password"].ToString();
                return View("Index", mail);
            }
            else
            {
                HttpCookie MailInfo = new HttpCookie("Mail");
                MailInfo["Host"] = "";
                MailInfo["Port"] = "";
                MailInfo["To"] = "";
                MailInfo["From"] = "";
                MailInfo["Password"] = "";
                MailInfo.Expires.Add(new TimeSpan(0, 1, 0));
                Response.Cookies.Add(MailInfo);
            }

            return View();
        }

        [HttpPost]
        public ViewResult Settings(Mail _objModelMail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpCookie MailInfo = new HttpCookie("Mail");
                    MailInfo["Host"] = _objModelMail.Host;
                    MailInfo["Port"] = _objModelMail.Port;
                    MailInfo["To"] = _objModelMail.To;
                    MailInfo["From"] = _objModelMail.From;
                    MailInfo["Password"] = _objModelMail.To;
                    MailInfo.Expires.Add(new TimeSpan(0, 1, 0));
                    Response.Cookies.Add(MailInfo);
                }
                catch (Exception e)
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Unknown error! try after sometime");
                    return View("Index", _objModelMail);
                }
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Saved successfully");
                ModelState.Clear();
                return View(new Mail());
            }
            else
            {
                return View();
            }
        }
    }
}