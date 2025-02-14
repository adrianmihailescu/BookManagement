using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookManagement
{
    public class UnusedOldCode
    {

        /*
        /// <summary>
        /// checks if an user has books scheduled to be returned for a specific date
        /// </summary>
        /// <param name="ImgId"></param>
        /// <returns></returns>
        protected int CheckIfUserHasScheduledBooksForToday(DateTime data, int IDUser)
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();

            int result = 0;

            try
            {
                conn = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["BookManagementConnectionString"].ConnectionString);

                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;

                cmd.CommandText = "GetScheduledBooksForTodayByUser";


                cmd.Parameters.Add("@data", SqlDbType.DateTime).Value = data;
                cmd.Parameters.Add("@IDUser", SqlDbType.Int).Value = IDUser;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }

            catch
            {
                result = 0;
                return result;
            }

            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                // conn.Dispose();
                cmd.Dispose();
            }
        }
         */

    }
}