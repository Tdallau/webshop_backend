using System;
using System.Collections.Generic;
using System.IO;
using Models.DB;

namespace webshop_backend.html.order
{
    public class OrderToCSharp
    {
        public static string Order(User user, Address adress, List<OrderTable> orderitems)
        {
            var main = "";
            using (var reader = File.OpenText(@"html/order/order.html"))
            {
                main = reader.ReadToEnd();
            }
            main = main.Replace("{{name}}", user.name);
            main = main.Replace("{{orderInformation}}", OrderToCSharp.OrderInformation(user, adress));
            main = main.Replace("{{orderTable}}", OrderToCSharp.OrderTable(orderitems));
            return main;
        }

        private static string OrderInformation(User user, Address adress)
        {
            var orderInformation = "";
            using (var reader = File.OpenText(@"html/order/orderInformation.html"))
            {
                orderInformation = reader.ReadToEnd();
            }
            orderInformation = orderInformation.Replace("{{name}}", user.name);
            orderInformation = orderInformation.Replace("{{street}}", $"{adress.Street} {adress.Number}");
            orderInformation = orderInformation.Replace("{{city}}", $"{adress.City}");
            orderInformation = orderInformation.Replace("{{zipcode}}", $"{adress.ZipCode}");
            orderInformation = orderInformation.Replace("{{paymentMethod}}", "Ideal");

            return orderInformation;
        }

        private static string OrderTable(List<OrderTable> orderitems)
        {
            var ordeTable = "";
            var row = "";
            decimal totalPrice = 0;
            int totalCards = 0;
            using (var reader = System.IO.File.OpenText(@"html/order/orderTabel.html"))
            {
                ordeTable = reader.ReadToEnd();
            }
            for (int i = 0; i < orderitems.Count; i++)
            {
                using (var reader = System.IO.File.OpenText(@"html/order/orderRow.html"))
                {

                    var newRow = reader.ReadToEnd();
                    newRow = newRow.Replace("{{name}}", orderitems[i].Name);
                    newRow = newRow.Replace("{{quantity}}", orderitems[i].Quantity.ToString());
                    decimal price = (((decimal)orderitems[i].Price) * ((decimal)orderitems[i].Quantity)) / 100;
                    newRow = newRow.Replace("{{price}}", string.Format("{0:0.00}", price));

                    row += newRow;
                    totalPrice += price;
                    totalCards += orderitems[i].Quantity;
                }
            }
            using (var reader = System.IO.File.OpenText(@"html/order/orderRow.html"))
            {
                var totalRow = reader.ReadToEnd(); ;
                totalRow = totalRow.Replace("{{name}}", "");
                totalRow = totalRow.Replace("{{quantity}}", $"<b>{totalCards}</b>");
                totalRow = totalRow.Replace("{{price}}", $"{string.Format("{0:0.00}", totalPrice)}");

                row += totalRow;
            }
            ordeTable = ordeTable.Replace("{{orderRows}}", row);
            return ordeTable;
        }

    }
}