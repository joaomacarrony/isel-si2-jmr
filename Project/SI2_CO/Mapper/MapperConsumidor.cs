using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;


using SI2_CO.Entities;
using SI2_CO.Mapper_Interfaces;



using System.Configuration;
using System.Transactions;



namespace SI2_CO.Mapper
{
    class MapperConsumidor
    {
        private string cs;

        public MapperConsumidor()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }

        public void Create(Consumidor consumidor)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO Consumidor(cid,nome,data_transacao) VALUES(@cid,@nome,@data_transacao)";
             
                SqlParameter cid = new SqlParameter();
                SqlParameter nome = new SqlParameter();
                SqlParameter data_transacao = new SqlParameter();


                cmd.Parameters.Add(cid);
                cmd.Parameters.Add(nome);   
                cmd.Parameters.Add(data_transacao);
              
                cid.ParameterName = "@cid";
                cid.SqlDbType = SqlDbType.Int;
                nome.ParameterName = "@nome";
                nome.SqlDbType = SqlDbType.VarChar;
                data_transacao.ParameterName = "@data_transacao";
                data_transacao.SqlDbType = SqlDbType.DateTime;

                
                cid.Value = consumidor.cid;
                nome.Value = consumidor.nome;
                data_transacao.Value = consumidor.data_transacao;


                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }

        public Consumidor Read(int cid)
        {
            Consumidor consumidor = new Consumidor();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Consumidor WHERE cid = @cid";

                SqlParameter FoidParam = new SqlParameter();
                cmd.Parameters.Add(FoidParam);
                FoidParam.ParameterName = "@cid";
                FoidParam.SqlDbType = SqlDbType.Int;
                FoidParam.Value = cid;
               
                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                           string  tid = rdr["tid"].ToString();                               // Do somthing with this rows string, for example to put them in to a list
                            string cid1 = rdr["cid"].ToString();
                            string nome = rdr["nome"].ToString();
                            string data_transacao = rdr["data_transacao"].ToString();
                            consumidor.cid = Int32.Parse(cid1);
                            consumidor.tid = Int32.Parse(tid);
                            consumidor.nome = nome;
                            if (!data_transacao.Equals(""))
                            {
                                Console.WriteLine("data transacao " + data_transacao);
                                consumidor.data_transacao = DateTime.Parse(data_transacao);//DateTime.Parse(data_transacao);
                            }

                        }
                    }
                   
                
                    


                }
                ts.Complete();
            }
            return consumidor;
        }

        public void Update(int cid1,int tid1,string nome1,DateTime data_transacao1)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Consumidor SET  cid=@cid,nome=@nome,data_transacao=@data_transacao WHERE cid=@cid";
                SqlParameter tid = new SqlParameter();
                SqlParameter cid = new SqlParameter();
                SqlParameter nome = new SqlParameter();
                SqlParameter data_transacao = new SqlParameter();

                cmd.Parameters.Add(tid);
                cmd.Parameters.Add(cid);
                cmd.Parameters.Add(nome);
                cmd.Parameters.Add(data_transacao);

                tid.ParameterName = "@tid";
                tid.SqlDbType = SqlDbType.Int;
                cid.ParameterName = "@cid";
                cid.SqlDbType = SqlDbType.Int;
                nome.ParameterName = "@nome";
                nome.SqlDbType = SqlDbType.VarChar;
                data_transacao.ParameterName = "@data_transacao";
                data_transacao.SqlDbType = SqlDbType.Date;

                tid.Value = tid1;
                cid.Value = cid1;
                 nome.Value = nome1;
                data_transacao.Value = data_transacao1;
                

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }
        public void Delete(int cid1)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM Consumidor WHERE cid = @cid";
                SqlParameter cid = new SqlParameter();
         

                cmd.Parameters.Add(cid);
        

                cid.ParameterName = "@cid";
                cid.SqlDbType = SqlDbType.Int;
                

                cid.Value = cid1;
                
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
