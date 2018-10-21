using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WebWithAuthentication.Service
{
    public class OtherServices
    {
        public static void SendMail(string emailTo, string subject, string message)
        {
            MailMessage mail = new MailMessage("dtbookcompany@gmail.com", emailTo)
            {
                Subject = subject,
                Body = message
            };
            string emailAddress = ConfigurationManager.AppSettings["emailAddress"];
            string password = ConfigurationManager.AppSettings["passwordEmailAddress"];
            SmtpClient client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(emailAddress, password)
            };

            client.Send(mail);
        }
    }
}