using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DbDataHelper.Attributes
{
    [AttributeUsage( AttributeTargets.Class, AllowMultiple =true)]
    public class StructureMetaAttribute:Attribute, ISqlCreator
    {
        public StructureMetaAttribute(String sqlString,bool returnList=false)
        {
            this.SqlString = sqlString;
            this.ReturnList = returnList;
        }

        public String SqlString { get; set; }
        public bool ReturnList { get; set; }

        public string ToSql(object sender)
        {
            return SqlString;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class Where : Attribute, ISqlCreator
    {
        public String SqlString { get; set; }

        public string ToSql(object sender)
        {
            return "WHERE";
        }
    }


    public interface ISqlCreator
    {
        String ToSql(object sender);
    }


    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class StructureConditionMetaAttribute : Attribute,ISqlCreator
    {
        public StructureConditionMetaAttribute(String conditionString, String clauseKey="WHERE",String ConditionString="", bool conditionNullable=false)
        {
            this.ConditionString = conditionString;
            this.ClauseKey = clauseKey;
            
            this.ConditionNullable = conditionNullable;
        }
        public String ClauseKey { get; set; }
        public String ConditionString { get; set; }

        public bool ConditionNullable { get; set; }

        public string ToSql(object sender)
        {
            String cs = ConditionString;
            

            Regex propertyReg = new Regex("\\{THIS\\.\\w+\\}");
            foreach (Match m in propertyReg.Matches(cs))
            {
                String propertyName= m.Value.Replace("{THIS.", "");
                propertyName = propertyName.Replace("}", "");

                String replaceM = m.Result(sender.GetType().GetProperty(propertyName).GetValue(sender).ToString());
                System.Reflection.PropertyInfo pi =sender.GetType().GetProperty(propertyName);
                switch (pi.PropertyType.Name)
                {
                    case "String":
                        replaceM = String.Format("'{0}'", replaceM);
                        break;
                }

                cs = cs.Replace(m.Value, replaceM);
                
            }
            //typeof(DbDataHelper.Oracle.Table).GetProperty("")
            //ConditionString = 
            return String.Format("{0}", cs);
        }
    }

    #region Connector

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class OR : Attribute, ISqlCreator
    {
        public string ToSql(object sender)
        {
            return "OR";
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AND : Attribute, ISqlCreator
    {
        public string ToSql(object sender)
        {
            return "AND";
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RANGE_START : Attribute, ISqlCreator
    {
        public RANGE_START(int rangeId)
        {
            this.RangeId = rangeId;
        }
        public int RangeId { get; set; }
        public string ToSql(object sender)
        {
            return "(";
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RANGE_END : Attribute, ISqlCreator
    {
        public RANGE_END(int rangeId)
        {
            this.RangeId = rangeId;
        }
        public int RangeId { get; set; }
        public string ToSql(object sender)
        {
            return ")";
        }
    }






    #endregion


    [AttributeUsage( AttributeTargets.Property)]
    public class DbEntityMappingAttribute : Attribute, ISqlCreator
    {
        public DbEntityMappingAttribute(String ColumnName)
        {
            this.ColumnName = ColumnName;
        }

        public String ColumnName { get; set; }

        public string ToSql(object sender)
        {
            throw new NotImplementedException();
        }
    }
}
