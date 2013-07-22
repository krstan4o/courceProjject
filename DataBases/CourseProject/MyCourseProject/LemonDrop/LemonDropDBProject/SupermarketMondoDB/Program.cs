﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Data;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace SupermarketMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            //           "product-id" : 3,
            // "product-name" : "Beer “Beck’s”",
            // "vendor-name" : "Zagorka Corp.",
            // "total-quantity-sold" : 236,
            // "total-incomes" : 262.95
            List<ProductRaport> jsonData = new List<ProductRaport>();
            string query = "SELECT * FROM Products";
            SqlProvider.ExecuteSqlQueryReturnValue(query, null, delegate(SqlDataReader reader)
            {
                while (reader.Read())
                {
                    int id = (int)reader.GetValue(0);
                    string productName = (string)reader.GetValue(2);
                    int vendorId = (int)reader.GetValue(1);
                    string vendorName="";
                    Dictionary<string, object> parametters = new Dictionary<string, object>();
                    parametters.Add("vendorId", vendorId);
                    string queryVendors = "SELECT * FROM Vendors WHERE Id=@vendorId";
                    SqlProvider.ExecuteSqlQueryReturnValue(queryVendors, parametters, delegate(SqlDataReader readerVendors)
                    {
                        while (readerVendors.Read())
                        {
                            vendorName = readerVendors.GetValue(1).ToString();
                        }
                    });
                    double totalQuantity = 0;
                    double totalSum = 0;
                    parametters = new Dictionary<string, object>();
                    parametters.Add("productId", id);
                    string queryProducts = "SELECT * FROM sales WHERE ProductId=@productId";
                    SqlProvider.ExecuteSqlQueryReturnValue(queryProducts, parametters, delegate(SqlDataReader readerProducts)
                    {
                        while (readerProducts.Read())
                        {
                            totalQuantity = totalQuantity + (double)readerProducts.GetValue(2);
                            totalSum = totalSum + (double)readerProducts.GetValue(4);
                        }
                    });
                    jsonData.Add(new ProductRaport()
                    {
                        ProductId = id,
                        ProductName = productName,
                        Vendor_name = vendorName,
                        TotalQuantitySold = totalQuantity,
                        TotalIncomes = totalSum
                    });
                }
            });
            string json = JsonConvert.SerializeObject(jsonData.ToArray());
            System.IO.File.WriteAllText(@"..\path.json", json);
        }
    }
}