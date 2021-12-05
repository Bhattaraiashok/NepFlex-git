using DataAccess.Repositories;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Interfaces.Repositories;
using NepFlex.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NepFlex.DataAccess.Repositories
{
    public class SendEmailRepository : Repository<string, int>, ISendEmailRepository
    {
        private readonly IOnlinePasalContext _context;
        public SendEmailRepository(IOnlinePasalContext context) : base(context)
        {
            _context = context;
        }

        public string SendEmail(SendEmailRequest email)
        {
            //MailMessage mail = new MailMessage();

            //mail.From = new MailAddress("nepalicraig@gmail.com");
            //mail.To.Add(new MailAddress(email.SentToEmail));
            //mail.Bcc.Add(new MailAddress("nepalicraig@gmail.com"));
            //mail.Body = email.EmailText;

            //SmtpClient smtpClient = new SmtpClient();
            //smtpClient.UseDefaultCredentials = false;
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.EnableSsl = true;

            //smtpClient.Send(mail);

             
            //MailMessage msg = new MailMessage();

            //msg.From = new MailAddress("nepalicraig@gmail.com");
            //msg.To.Add(email.SentToEmail);
            //msg.Subject = email.SubjectEmail;
            //msg.Body = email.EmailText;
            //msg.Priority = MailPriority.High;

            //SmtpClient client = new SmtpClient();

            //client.Send(msg);

            //using (MailMessage mail = new MailMessage())
            //{
            //    mail.From = new MailAddress("nepalicraig@gmail.com");
            //    mail.To.Add(email.SentToEmail);
            //    mail.Subject = email.SubjectEmail;
            //    mail.Body = email.EmailText;
            //    mail.IsBodyHtml = true;

            //    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            //    {
            //        smtp.Credentials = new NetworkCredential("nepalicraig@gmail.com", "P@ssword1234");
            //       // smtp.EnableSsl = true;
            //        smtp.UseDefaultCredentials = false;
            //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //        smtp.Timeout = 300000;
            //        smtp.Send(mail);
            //    }
            //}
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("nepalicraig@gmail.com");
                mail.To.Add(email.SentToEmail);
                mail.Subject = "Test Mail..!!!!";
                mail.Body = "mail with attachment";

                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(@"C:\Attachment.txt");
                //mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("nepalicraig@gmail.com", "");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "mail send";
        }
    }

}
