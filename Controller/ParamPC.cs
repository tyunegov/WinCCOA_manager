using ETM.WCCOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Controller
{
    class ParamPC : DataPoint
    {
        public ParamPC(OaManager manager):base(manager)
        {
            DptName = "PC_Param";
        }

        protected override void WriteValuesExample()
        {

            Parametrs = SystemParametrs.ParamPC();
            base.WriteValuesExample();
        }
    }
}
