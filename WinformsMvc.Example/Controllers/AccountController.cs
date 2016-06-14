using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformsMvc.Example.Views;

namespace WinformsMvc.Example.Controllers
{
    public class AccountController : Controller
    {
        private IView _view;
        public override IView View
        {
            get
            {
                return _view ?? new AccountView();
            }
        }

        public override bool Loadable()
        {
            return false; // Could be used to check if an user was signed in.
        }

        protected override void OnLoadFailedHandler(object sender, EventArgs e)
        {
            base.OnLoadFailedHandler(sender, e);

            // Optionally run a custom controller failed handler. 
            // On default it will prompt a message to the user with a message that application will close.
        }
    }
}
