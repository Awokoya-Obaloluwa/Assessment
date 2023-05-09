namespace Assessment.Helpers
{
    public class commonHelpers
    {
        private IConfiguration _Config;


        public commonHelpers(IConfiguration Config)
        {
            _Config = Config;
        }

   /*     public int DMLTransaction(IConfiguration _Config)
        {

        }
   */


            public int DMLTransaction(string Query)
            {

                int Result;

            string? v = _Config["ConnectionStrings :DefaultConnection"];
            string connectionString = v;

                using (SqlConnection connection = new(connectionString))
                {
                    connection.Open();

                    string sql = Query;

                    SqlCommand command = new SqlCommand(sql, connection);
                    Result = command.ExecuteNonQuery();
                    connection.Close();

                }
                return Result;


            }

        public IConfiguration Get_Config()
        {
            return _Config;
        }

        public bool UserAlreadyExists(string query, IConfiguration _Config)
            {
                bool flag = false;

                string connectionString = _Config["ConnectionStrings :DefaultConnection"];

                using (SqlConnection connection = new(connectionString))
                {
                    connection.Open();

                    string sql = query;

                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader rd = command.ExecuteReader();
                    if (rd.HasRows)
                    {
                        flag = true;
                    }
                    connection.Close();
                }
                return flag;
            }

        }
    }
