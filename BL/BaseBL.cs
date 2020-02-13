using System;
using System.Collections.Generic;
using System.Text;
using DL;
using Entity;

namespace BL
{
    public class BaseBL
    {
        public virtual IEnumerable<T> Get<T>()
        {
            BaseDL baseDL = new BaseDL();
            return baseDL.Get<T>();
        } 
    }
}
