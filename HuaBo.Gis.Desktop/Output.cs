using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuaBo.Gis.Desktop
{
    //todo：信息提示类，以后再考虑
    public class Output
    {
        public event EventHandler<EventArgs> DoingOutput;

        public void Warning(string message)
        {
            if (DoingOutput != null)
            {
                DoingOutput(message, new EventArgs());
                //Console.WriteLine(message);
            }
        }
    }
}
