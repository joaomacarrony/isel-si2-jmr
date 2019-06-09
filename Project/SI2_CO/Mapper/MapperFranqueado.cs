using SI2_CO.Entities;
using SI2_CO.Mapper_Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace SI2_CO.Mapper
{
    public class MapperFranqueado : IMapperFranqueado
    {
        private string cs;

        public MapperFranqueado()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }

        public void Create(Franqueado franq)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO FRANQUEADO output inserted.fid VALUES(@nif,@nome,@morada)";

                SqlParameter nifParam = new SqlParameter();
                SqlParameter nomeParam = new SqlParameter();
                SqlParameter moradaParam = new SqlParameter();

                cmd.Parameters.Add(nifParam);
                cmd.Parameters.Add(nomeParam);
                cmd.Parameters.Add(moradaParam);


                nifParam.ParameterName = "@nif";
                nifParam.SqlDbType = SqlDbType.Int;
                nomeParam.ParameterName = "@nome";
                nomeParam.SqlDbType = SqlDbType.VarChar;
                nomeParam.Size = 40;
                moradaParam.ParameterName = "@morada";
                moradaParam.SqlDbType = SqlDbType.VarChar;
                moradaParam.Size = 100;

                nifParam.Value = franq.nif;
                nomeParam.Value = franq.nome;
                moradaParam.Value = franq.morada;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    franq.fid = (int)cmd.ExecuteScalar();

                }
                ts.Complete();
            }
        }

        public void Delete(Franqueado franq)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "delete from franqueado where fid = @fid";

                SqlParameter fidParam = new SqlParameter();

                cmd.Parameters.Add(fidParam);

                fidParam.ParameterName = "@fid";
                fidParam.SqlDbType = SqlDbType.Int;
                fidParam.Value = franq.fid;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }

        public Franqueado Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Franqueado franq)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "update from franqueado set nif = @nif, nome = @nome, morada = @morada where fid = @fid";

                SqlParameter fidParam = new SqlParameter();
                SqlParameter nifParam = new SqlParameter();
                SqlParameter nomeParam = new SqlParameter();
                SqlParameter moradaParam = new SqlParameter();

                cmd.Parameters.Add(fidParam);
                cmd.Parameters.Add(nifParam);
                cmd.Parameters.Add(nomeParam);
                cmd.Parameters.Add(moradaParam);

                fidParam.ParameterName = "@fid";
                fidParam.SqlDbType = SqlDbType.Int;
                nifParam.ParameterName = "@nif";
                nifParam.SqlDbType = SqlDbType.Int;
                nomeParam.ParameterName = "@nome";
                nomeParam.SqlDbType = SqlDbType.VarChar;
                nomeParam.Size = 40;
                moradaParam.ParameterName = "@morada";
                moradaParam.SqlDbType = SqlDbType.VarChar;
                moradaParam.Size = 100;

                fidParam.Value = franq.fid;
                nifParam.Value = franq.nif;
                nomeParam.Value = franq.nome;
                moradaParam.Value = franq.morada;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }
    }
}
