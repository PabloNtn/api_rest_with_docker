using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using ApiDocker.Model;
using ApiDocker.Business;

namespace ApiDocker.Repository.Implementation
{
    public class DirectorShipRepositoryImplementation : IDirectorShipRepository
    {
        public ActionResult<DirectorShip> Create(DirectorShip director)
        {
            string conString = "User Id=SYSTEM;Password=257729;Data Source=172.23.128.1:1521/xe;";
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        ControllerConnectionDataBase(con);

                        cmd.CommandText = "prDIR_INS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("VUserName", director.Dir_userName).Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("VPwd", director.Dir_pwd).Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("VRole", director.Dir_role).Direction = ParameterDirection.Input;

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        ControllerConnectionDataBase(con);
                    }
                }
            }
            return director;
        }

        public ActionResult<int> Delete(long id)
        {
            bool testeDelecao = false;
            string conString = "User Id=SYSTEM;Password=257729;Data Source=172.23.128.1:1521/xe;";
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        ControllerConnectionDataBase(con);

                        cmd.CommandText = "prDIR_DEL";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("id", id).Direction = ParameterDirection.Input;

                        cmd.ExecuteNonQuery();

                        testeDelecao = true;
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        ControllerConnectionDataBase(con);
                    }
                }
            }
            if (!testeDelecao)
                return null;
            return Convert.ToInt32(id);
        }

        public List<DirectorShip> FindAll()
        {
            List<DirectorShip> directorList = new();

            string conString = "User Id=SYSTEM;Password=257729;Data Source=172.23.128.1:1521/xe;";
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        ControllerConnectionDataBase(con);

                        cmd.CommandText = "prDIR_SEL_Buscar";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var director = new DirectorShip(
                                int.Parse(reader.GetString(0)),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3));
                            directorList.Add(director);
                        }
                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        ControllerConnectionDataBase(con);
                    }
                }
            }
            return directorList;
        }

        public DirectorShip FindByUserName(string username, string password)
        {
            DirectorShip director = new();

            string conString = "User Id=SYSTEM;Password=257729;Data Source=172.23.128.1:1521/xe;";
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        ControllerConnectionDataBase(con);

                        cmd.CommandText = "prDIR_SEL_ByUserName";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("VUserName", username).Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("VPwd", password).Direction = ParameterDirection.Input;

                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            director = new DirectorShip(
                            int.Parse(reader.GetString(0)),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3)
                            );
                        }
                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        ControllerConnectionDataBase(con);
                    }
                }

            }
            return director;
        }

        void ControllerConnectionDataBase(OracleConnection connection)
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                else
                    connection.Open();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
