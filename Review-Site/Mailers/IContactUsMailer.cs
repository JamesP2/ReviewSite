using Mvc.Mailer;
using Review_Site.Models;

namespace Review_Site.Mailers
{ 
    public interface IContactUsMailer
    {
            MvcMailMessage ThankYou(ContactForm form);
			MvcMailMessage UserContact(ContactForm form);
	}
}