namespace DapperAdvanceDemoLibrary
{
    public static class Tools
    {
        public static string GetConnectionString(string name = "DapperDB")
        {
            //return ConfigurationManager.ConnectionStrings[name].ConnectionString;

            return "Data Source=DESKTOP-LBTN8H4\\SQLEXPRESS2019;Initial Catalog=DapperDBRKDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}