using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDelegates
{
    public class InfoObtained
    {
        public event GetInfo OnInfoObtained;
        public object p_sender;
        public string p_EventArgs;

        public InfoObtained()
        {

        }

        public void RaiseEvent (object sender, string e)
        {
            p_sender = sender;
            p_EventArgs = e;

            OnInfoObtained += InfoObtained_OnInfoObtained;
            OnInfoObtained(sender, e);
        }

        private void InfoObtained_OnInfoObtained(object sender, string e)
        {
            //throw new NotImplementedException();
        }
    }
}
