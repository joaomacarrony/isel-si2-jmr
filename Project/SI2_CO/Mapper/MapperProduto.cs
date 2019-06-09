using SI2_CO.Entities;
using SI2_CO.Mapper_Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace SI2_CO.Mapper
{
    class MapperProduto : IMapperProduto
    {
        private string cs;

        public MapperProduto()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }

        public void Create(Produto entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO Produto VALUES(@codigo,@tipo,@descricao,@quantidade, @quantidade_minima,@quantidade_maxima)";
				
                SqlParameter codigoParam = new SqlParameter();
                SqlParameter tipoParam = new SqlParameter();
                SqlParameter descricaoParam = new SqlParameter();
                SqlParameter quantParam = new SqlParameter();
				SqlParameter quantMinParam = new SqlParameter();
                SqlParameter quantMaxParam = new SqlParameter();
               

                cmd.Parameters.Add(codigoParam);
                cmd.Parameters.Add(tipoParam);
				cmd.Parameters.Add(descricaoParam);
                cmd.Parameters.Add(quantParam);
                cmd.Parameters.Add(quantMinParam);
                cmd.Parameters.Add(quantMaxParam);
                

                codigoParam.ParameterName = "@codigo_produto";
                codigoParam.SqlDbType = SqlDbType.Int;

                tipoParam.ParameterName = "@tipo";
                tipoParam.SqlDbType = SqlDbType.VarChar;
				
				descricaoParam.ParameterName = "@descricao";
                descricaoParam.SqlDbType = SqlDbType.VarChar;
				
                quantParam.ParameterName = "@quantidade";
                quantParam.SqlDbType = SqlDbType.Int;

                quantMinParam.ParameterName = "@quantidade_minima";
                quantMinParam.SqlDbType = SqlDbType.Int;

                quantMaxParam.ParameterName = "@quantidade_maxima";
                quantMaxParam.SqlDbType = SqlDbType.Int;

                

                codigoParam.Value = entity.codigo;
                tipoParam.Value = entity.tipo;
				descricaoParam.Value = entity.descricao;
                quantParam.Value = entity.quantidade;
                quantMinParam.Value = entity.quantidade_minima;
                quantMaxParam.Value = entity.quantidade_maxima;
                

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }

        public void Delete(Produto entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
				
                cmd.CommandText = "delete from Produto where codigo = @codigo;";

                SqlParameter codigoParam = new SqlParameter();

                cmd.Parameters.Add(codigoParam);

                codigoParam.ParameterName = "@codigo";
                codigoParam.SqlDbType = SqlDbType.Int;

                codigoParam.Value = entity.codigo;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }

        public Produto Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Produto entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "update from Produto set " +
														"tipo = @tipo" +
														"descricao = @descricao" +
                                                        "quantidade = @quantidade," +
                                                        "quantidade_minima = @quantidade_minima," +
                                                        "quantidade_maxima = @quantidade_maxima," +
                                                        "where codigo = @codigo ";

                SqlParameter codigoParam = new SqlParameter();
                SqlParameter tipoParam = new SqlParameter();
				SqlParameter descricaoParam = new SqlParameter();
                SqlParameter quantParam = new SqlParameter();
                SqlParameter quantMinParam = new SqlParameter();
                SqlParameter quantMaxParam = new SqlParameter();
                

                cmd.Parameters.Add(codigoParam);
                cmd.Parameters.Add(tipoParam);
				cmd.Parameters.Add(descricaoParam);
                cmd.Parameters.Add(quantParam);
                cmd.Parameters.Add(quantMinParam);
                cmd.Parameters.Add(quantMaxParam);
                

                codigoParam.ParameterName = "@codigo";
                codigoParam.SqlDbType = SqlDbType.Int;

                tipoParam.ParameterName = "@tipo";
                tipoParam.SqlDbType = SqlDbType.VarChar;
                tipoParam.Size = 40;

                descricaoParam.ParameterName = "@descricao";
                descricaoParam.SqlDbType = SqlDbType.VarChar;
                descricaoParam.Size = 100;

                quantParam.ParameterName = "@quantidade";
                quantParam.SqlDbType = SqlDbType.Int;

                quantMinParam.ParameterName = "@quantidade_minima";
                quantMinParam.SqlDbType = SqlDbType.Int;

                quantMaxParam.ParameterName = "@quantidade_maxima";
                quantMaxParam.SqlDbType = SqlDbType.Int;

                codigoParam.Value = entity.codigo;
                tipoParam.Value = entity.tipo;
				descricaoParam.Value = entity.descricao;
                quantParam.Value = entity.quantidade;
                quantMinParam.Value = entity.quantidade_minima;
                quantMaxParam.Value = entity.quantidade_maxima;
                

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
