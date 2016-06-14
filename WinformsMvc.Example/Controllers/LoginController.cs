using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinformsMvc.Example.Views;

namespace WinformsMvc.Example.Controllers
{
    public class LoginController : Controller
    {
        private IView _view;
        public override IView View
        {
            get
            {
                return _view ?? new LoginView();
            }
        }

        public override bool Loadable()
        {
            return true;
        }
    }
}
