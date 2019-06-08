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

        public void Create(Franqueado entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO FRANQUEADO VALUES(@nif,@nome,@morada)";

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

                nifParam.Value = entity.nif;
                nomeParam.Value = entity.nome;
                moradaParam.Value = entity.morada;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }

        public void Delete(Franqueado entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "delete from franqueado where fid = @fid)";

                SqlParameter fidParam = new SqlParameter();

                cmd.Parameters.Add(fidParam);

                fidParam.ParameterName = "@fid";
                fidParam.SqlDbType = SqlDbType.Int;
                fidParam.Value = entity.fid;

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

        public void Update(Franqueado entity)
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

                fidParam.Value = entity.fid;
                nifParam.Value = entity.nif;
                nomeParam.Value = entity.nome;
                moradaParam.Value = entity.morada;

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
