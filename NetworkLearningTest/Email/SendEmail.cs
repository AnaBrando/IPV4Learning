using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Email;

namespace TestNetWork.Email
{
    [TestClass]
    public class SendEmail
    {
        [TestClass]
        public class EnviaEmail
        {
            string host = "smtp.gmail.com";
            int port = 587;
            bool enableSSL = true;
            string userName = "akiramon6669@gmail.com";
            string passWord = "Ichigobleach0";

            //public Task SendEmailAsync(string email, string subject, string htmlMessage)
            [TestMethod]
            public void EnviouEmail()
            {
                string email = "anapchavesweb@gmail.com";
                EmailSender emailSender = new EmailSender(host, port, enableSSL, userName, passWord);
                var Enviou = emailSender.SendEmailAsync(email, "teste", "teste");
                Assert.IsNotNull(Enviou);
                
            }
        }
    }
}
