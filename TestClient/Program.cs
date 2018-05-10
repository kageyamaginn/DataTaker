using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DbDataHelper.Oracle;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Table t = new Table();
            t.TableName = "SNT_FI_AUDIT";
            t.Owner = "PLM";

            Type tableType = typeof(Table);

            List<Attribute> atts = tableType.GetCustomAttributes().ToList() ;
            for (int attIndex = 0; attIndex < atts.Count(); attIndex++)
            {
                Type attType = atts[attIndex].GetType();
                MethodInfo mi = attType.GetMethod("ToSql");

                String sqlString= mi.Invoke(atts[attIndex], new object[] { t}) .ToString();

            }
            
        }
    }
}
