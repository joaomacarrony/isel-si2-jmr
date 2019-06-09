using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Configuration;
using System.Transactions;

using System.Data;
using System.Data.SqlClient;
using SI2_CO.Entities;
using SI2_CO.Mapper_Interfaces;

namespace SI2_CO.Mapper
{
    class MapperVendas : IMapperVendas
    {
        private string cs;

        public MapperVendas()
        {
            cs = ConfigurationManager.ConnectionStrings["SI2"].ConnectionString;
        }
        public MapperVendas(String cs)
        {
            this.cs = cs;
        }
        public void Create(Vendas vendas)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO Vendas(fid,cid,tid,codigo_produto,data_venda,preco_venda,quantidade) VALUES(@fid,@cid,@tid,@codigo_produto,@data_venda,@preco_venda,@quantidade)";
                
               // SqlParameter vid= new SqlParameter();
                SqlParameter fid = new SqlParameter();
                SqlParameter cid = new SqlParameter();
                SqlParameter tid = new SqlParameter();
                SqlParameter codigo_produto = new SqlParameter();
                SqlParameter preco_venda = new SqlParameter();
                SqlParameter data_venda = new SqlParameter();
                SqlParameter quantidade = new SqlParameter();

               // cmd.Parameters.Add(vid);
                cmd.Parameters.Add(fid);
                cmd.Parameters.Add(cid);
                cmd.Parameters.Add(tid);
                cmd.Parameters.Add(codigo_produto);
                cmd.Parameters.Add(preco_venda);
                cmd.Parameters.Add(data_venda);
                cmd.Parameters.Add(quantidade);


//vid.ParameterName = "@vid";
                //vid.SqlDbType = SqlDbType.Int;
                fid.ParameterName = "@fid";
                fid.SqlDbType = SqlDbType.Int;
                cid.ParameterName = "@cid";
                cid.SqlDbType = SqlDbType.Int;
                tid.ParameterName = "@tid";
                tid.SqlDbType = SqlDbType.Int;
                codigo_produto.ParameterName ="@codigo_produto";
                codigo_produto.SqlDbType = SqlDbType.Int;
                preco_venda.ParameterName = "@preco_venda";
                preco_venda.SqlDbType = SqlDbType.Float;
                data_venda.ParameterName = "@data_venda";
                data_venda.SqlDbType = SqlDbType.Date;
                quantidade.ParameterName = "@quantidade";
                quantidade.SqlDbType = SqlDbType.Int;
                ;
               
               
                fid.Value = vendas.fid;
                //vid.value = vendas.vid;
                cid.Value = vendas.cid;
                tid.Value = vendas.tid;
                codigo_produto.Value = vendas.codigo_produto;
                preco_venda.Value = vendas.preco_venda;
                data_venda.Value = vendas.data_venda;
                quantidade.Value = vendas.quantidade;
                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }

        public Vendas Read(int vid1)
        { Vendas v = new Vendas();
            
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Vendas WHERE vid=@vid";

                SqlParameter vid = new SqlParameter();
                SqlParameter fid = new SqlParameter();
                SqlParameter cid = new SqlParameter();
                SqlParameter tid = new SqlParameter();
                SqlParameter codigo_produto = new SqlParameter();
                SqlParameter preco_venda = new SqlParameter();
                SqlParameter data_venda = new SqlParameter();
                SqlParameter quantidade = new SqlParameter();
                cmd.Parameters.Add(vid);
                cmd.Parameters.Add(fid);
                cmd.Parameters.Add(cid);
                cmd.Parameters.Add(tid);
                cmd.Parameters.Add(codigo_produto);
                cmd.Parameters.Add(preco_venda);
                cmd.Parameters.Add(data_venda);
                cmd.Parameters.Add(quantidade);


                //vid.ParameterName = "@vid";
                //vid.SqlDbType = SqlDbType.Int;
                vid.ParameterName = "@vid";
                vid.SqlDbType = SqlDbType.Int;
                fid.ParameterName = "@fid";
                fid.SqlDbType = SqlDbType.Int;
                cid.ParameterName = "@cid";
                cid.SqlDbType = SqlDbType.Int;
                tid.ParameterName = "@tid";
                tid.SqlDbType = SqlDbType.Int;
                codigo_produto.ParameterName = "@codigo_produto";
                codigo_produto.SqlDbType = SqlDbType.Int;
                preco_venda.ParameterName = "@preco_venda";
                preco_venda.SqlDbType = SqlDbType.Float;
                data_venda.ParameterName = "@data_venda";
                data_venda.SqlDbType = SqlDbType.Date;
                quantidade.ParameterName = "@quantidade";
                quantidade.SqlDbType = SqlDbType.Int;
                vid.Value = vid1;
                v.vid = vid1;

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            // Do somthing with this rows string, for example to put them in to a list
                            //string foid = rdr["foid"].ToString();

                            string svid = rdr["vid"].ToString();
                            string sfi = rdr["fid"].ToString();
                            string scid = rdr["vid"].ToString();
                            string stid = rdr["fid"].ToString();
                            string scodigo_produto = rdr["codigo_produto"].ToString();
                            string sdata_venda = rdr["data_venda"].ToString();
                            string spreco_venda = rdr["preco_venda"].ToString();
                            string squantidade = rdr["quantidade"].ToString();



                           // v.vid = Int32.Parse(svid);
                            v.fid = Int32.Parse(sfi);
                            v.cid = Int32.Parse(scid);
                            v.tid = Int32.Parse(stid);
                            v.codigo_produto = Int32.Parse(scodigo_produto);
                            v.data_venda = DateTime.Parse(sdata_venda);
                            v.preco_venda = float.Parse(spreco_venda);
                            v.quantidade = Int32.Parse(squantidade);
                        }
                    }

                }
                ts.Complete();
               
                return  v ;
            }
          
        }

        public void Update(Vendas vendas)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Vendas SET fid=@fid,cid=@cid,tid=@tid,preco_venda=@preco_venda,quantidade=@quantidade,data_venda=@data_venda WHERE vid = @vid";
                SqlParameter vid = new SqlParameter();
                SqlParameter codigo_produto = new SqlParameter();
                SqlParameter fid = new SqlParameter();
                SqlParameter cid = new SqlParameter();
                SqlParameter tid = new SqlParameter();
                SqlParameter data_venda = new SqlParameter();
                SqlParameter preco_venda = new SqlParameter();
                SqlParameter quantidade = new SqlParameter();

                cmd.Parameters.Add(vid);
                cmd.Parameters.Add(codigo_produto);
                cmd.Parameters.Add(fid);
                cmd.Parameters.Add(tid);
                cmd.Parameters.Add(cid);
                cmd.Parameters.Add(data_venda);
                cmd.Parameters.Add(preco_venda);
                cmd.Parameters.Add(quantidade);

                vid.ParameterName = "@vid";
                vid.SqlDbType = SqlDbType.Int;
                codigo_produto.ParameterName = "@codico_produto";
                codigo_produto.SqlDbType = SqlDbType.Int;
                fid.ParameterName = "@fid";
                fid.SqlDbType = SqlDbType.Int;
                tid.ParameterName = "@tid";
                tid.SqlDbType = SqlDbType.Int;
                cid.ParameterName = "@cid";
                cid.SqlDbType = SqlDbType.Int;
                data_venda.ParameterName = "@data_venda";
                data_venda.SqlDbType = SqlDbType.DateTime;
                preco_venda.ParameterName = "@preco_venda";
                preco_venda.SqlDbType = SqlDbType.Float;
                quantidade.ParameterName = "@quantidade";
                quantidade.SqlDbType = SqlDbType.Int;

                vid.Value = vendas.vid;
                cid.Value = vendas.cid;
                 fid.Value = vendas.fid;
                 tid.Value = vendas.tid;
                codigo_produto.Value = vendas.codigo_produto;
                data_venda.Value = vendas.data_venda;
                quantidade.Value = vendas.quantidade;
                preco_venda.Value = vendas.preco_venda;

                

                using (var cn = new SqlConnection(cs))
                {

                    cmd.Connection = cn;
                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
                ts.Complete();
            }
        }
        public void Delete(Vendas vendas)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM Vendas WHERE vid = @vid";
                SqlParameter vid = new SqlParameter();
 


                cmd.Parameters.Add(vid);
             

                vid.ParameterName = "@vid";
                vid.SqlDbType = SqlDbType.Int;
                vid.Value = vendas.vid;
               

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
