using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace MyCMS.Data.Test
{
    [TestClass]
    public class SqlDbConnectionTest
    {
        string connectionString = "Data Source=172.16.40.41;Initial Catalog=SYS;Persist Security Info=True;User ID=om;Password=om@Lampton";
        [TestMethod]
        public void QueryScalarTest()
        {
            SqlDbDriver driver = new SqlDbDriver();
            SqlStatement sql = new SqlStatement();
            sql.SqlClause = "select count(*) from vol_org";
            IConnection conn = driver.CreateConnection(connectionString);
            
            object o = conn.QueryScalar(sql);
            Assert.AreEqual(15, (int)o);
        }

        [TestMethod]
        public void QueryTest()
        {
            SqlDbDriver driver = new SqlDbDriver();
            SqlStatement sql = new SqlStatement();
            sql.SqlClause = "select ID from vol_org";
            IConnection conn = driver.CreateConnection(connectionString);

            DataTable o = conn.Query(sql);
            Assert.AreEqual(15, o.Rows.Count);
        }

        [TestMethod]
        public void LoadDatabasesTest()
        {
            Dictionaries ds = new Dictionaries();
            ds.SetGlobalDBString("conn", "MyCMS.Data.SqlDbDriver, MyCMS.Data");
            ds.LoadDataSource("D:\\", null);
            Assert.AreEqual(1,ds.DatabaseDict.Count);
            Assert.AreEqual(true, ds.DatabaseDict["We7.CMS.Common"] != null);
            Assert.AreEqual(38,((Database)(ds.DatabaseDict["We7.CMS.Common"])).EntityObjs.Count);
            Assert.AreEqual(34, ((Database)(ds.DatabaseDict["We7.CMS.Common"])).EntityObjs["Channel"].PropertyDict.Count);
            Assert.AreEqual(6, ((Database)(ds.DatabaseDict["We7.CMS.Common"])).EntityObjs["VoteAnswerStat"].PropertyDict.Count);
        }
    }
}
