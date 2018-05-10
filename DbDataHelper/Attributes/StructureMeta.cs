using System;
using System.Collections.Generic;
using System.Text;

namespace DbDataHelper.Attributes
{
    [AttributeUsage( AttributeTargets.Class, AllowMultiple =true)]
    public class StructureMetaAttribute:Attribute
    {
        public StructureMetaAttribute(String sqlString)
        {
            this.SqlString = sqlString;
        }

        public String SqlString { get; set; }
    }

    public interface ISqlCreator
    {
        String ToSql()
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class StructureConditionMetaAttribute : Attribute
    {
        public StructureConditionMetaAttribute(String conditionString, String clauseKey="WHERE",String ConditionString="", bool conditionNullable=false)
        {
            this.ConditionString = ConditionString;
            this.ClauseKey = clauseKey;
            this.ConditionString = ConditionString;
            this.ConditionNullable = conditionNullable;
        }
        public String ClauseKey { get; set; }
        public String ConditionString { get; set; }

        public bool ConditionNullable { get; set; }

        
        
    }

    #region Connector

    [AttributeUsage( AttributeTargets.Class, AllowMultiple = true)]
    public class OR : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AND : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NOT : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class LIKE : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class IN : Attribute
    {

    }

    #endregion


    [AttributeUsage( AttributeTargets.Property)]
    public class DbEntityMappingAttribute : Attribute
    {
        public DbEntityMappingAttribute(String ColumnName)
        {
            this.ColumnName = ColumnName;
        }

        public String ColumnName { get; set; }
    }
}
