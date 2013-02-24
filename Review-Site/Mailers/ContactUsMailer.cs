using System.Configuration;
using Mvc.Mailer;
using Review_Site.Models;

namespace Review_Site.Mailers
{ 
    public class ContactUsMailer : MailerBase, IContactUsMailer 	
	{
		public ContactUsMailer()
		{
			MasterName="_Layout";
		}

        public virtual MvcMailMessage ThankYou(ContactForm form)
		{
            ViewBag.form = form;
			return Populate(x =>
			{
				x.Subject = "Thanks for contacting theREVIEW!";
				x.ViewName = "ThankYou";
                x.IsBodyHtml = true;
				x.To.Add(form.Email);
			});
		}

        public virtual MvcMailMessage UserContact(ContactForm form)
		{
            ViewBag.form = form;
			return Populate(x =>
			{
				x.Subject = "A message to theREVIEW from " + form.Name;
				x.ViewName = "UserContact";
                x.IsBodyHtml = true;
                x.To.Add(ConfigurationManager.AppSettings["contact-email"]);
                x.ReplyToList.Add(form.Email);
			});
		}
 	}
}