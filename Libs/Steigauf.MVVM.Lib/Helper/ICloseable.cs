using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Steigauf.MVVM.Lib.Helper
{
    interface ICloseable
    {
        event EventHandler CloseWindowEvent;
    }
}
