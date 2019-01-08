using System;
using System.Collections.Generic;
using System.IO;
using Models.DB;

namespace webshop_backend.html.order
{
    public class OrderToCSharp
    {
        public static string Order(User user, Address adress, List<OrderTable> orderitems, Order order)
        {
            var main = @"<div>
    <style scoped>
        .test {
            width: 50%;
            margin: auto;
            margin-top: 5%
        }

        @media (max-width: 300px) {
            .test {
                width: 100%;
                margin: auto;
                margin-top: 5%
            }
        }
    </style>
    <div >
        <div>
            <h3>Hello {{name}}, </h3>

            <p>Thank you for choosing Magic the Gathering Webshop. Below you can check whether your order has been passed corectly.
                If you have any questions about your order please contact us at magicthegatheringwebshop@gmail.com. </p>

        </div>
        {{orderInformation}}
        {{orderTable}}
    </div>
</div>";
            // using (var reader = File.OpenText(@"html/order/order.html"))
            // {
            //     main = reader.ReadToEnd();
            // }
            main = main.Replace("{{name}}", user.name);
            main = main.Replace("{{orderInformation}}", OrderToCSharp.OrderInformation(user, adress, order));
            main = main.Replace("{{orderTable}}", OrderToCSharp.OrderTable(orderitems));
            return main;
        }

        private static string OrderInformation(User user, Address adress, Order order)
        {
            var orderInformation = @"<div style='display: block;padding-bottom: 10px; width: 100%'>
    <div style='float: left; padding-bottom: 5px; width: 49.9%; padding-right: 0.1%'>
        <table style='font-family: Arial, Helvetica, sans-serif;border-collapse: collapse;width: 100%;'>
            <thead>
                <tr>
                    <th style='border: 1px solid #ddd;padding: 8px; background-color: #3399FF;color: white;'>Billing
                        Information:</th>
                </tr>
            </thead>
            <tbody style='border: 1px solid #ddd;padding: 8px;'>
                <tr>
                    <td style='padding: 5px;'>{{name}}</td>
                </tr>
                <tr>
                    <td style='padding: 5px;'>{{street}} </td>
                </tr>
                <tr>
                    <td style='padding: 5px;'>{{city}}, {{zipcode}}</td>
                </tr>
            </tbody>
        </table>
    </div>
    <div style='float: right; width: 49.9%; padding-left: 0.1%'>
        <table style='font-family: Arial, Helvetica, sans-serif;border-collapse: collapse;width: 100%;'>
            <thead>
                <tr>
                    <th style='border: 1px solid #ddd;padding: 8px; background-color: #3399FF;color: white;'>Payment
                        Method:</th>
                </tr>
            </thead>
            <tbody style='border: 1px solid #ddd;padding: 8px;'>
                <tr>
                    <td style='padding: 5px;'>{{paymentMethod}}</td>
                </tr>
                <tr>
                    <td style='padding: 5px;'>&nbsp;</td>
                </tr>
                <tr>
                    <td style='padding: 5px;'>&nbsp;</td>
                </tr>
            </tbody>
        </table>
    </div>
</div>";
            // using (var reader = File.OpenText(@"html/order/orderInformation.html"))
            // {
            //     orderInformation = reader.ReadToEnd();
            // }
            orderInformation = orderInformation.Replace("{{name}}", user.name);
            orderInformation = orderInformation.Replace("{{street}}", $"{adress.Street} {adress.Number}");
            orderInformation = orderInformation.Replace("{{city}}", $"{adress.City}");
            orderInformation = orderInformation.Replace("{{zipcode}}", $"{adress.ZipCode}");
            orderInformation = orderInformation.Replace("{{paymentMethod}}", order.PayMethod);

            return orderInformation;
        }

        private static string OrderTable(List<OrderTable> orderitems)
        {
            var ordeTable = @"<table style='font-family: Arial, Helvetica, sans-serif;border-collapse: collapse;width: 100%;'>
    <thead>
        <tr>
            <th style='border: 1px solid #ddd;padding: 8px; padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #3399FF;color: white;width: 75%;'>Item</th>
            <th style='border: 1px solid #ddd;padding: 8px; padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #3399FF;color: white;width: 10%;'>Quantity</th>
            <th style='border: 1px solid #ddd;padding: 8px; padding-top: 12px;padding-bottom: 12px;text-align: left;background-color: #3399FF;color: white;width: 15%;'>Total</th>
        </tr>
    <tbody>
        {{orderRows}}
    </tbody>
    </thead>
</table>";
            var row = "";
            var baseRow = @"<tr>
    <td style='border: 1px solid #ddd;padding: 8px;'>{{name}}</td>
    <td style='border: 1px solid #ddd;padding: 8px;'>{{quantity}}</td>
    <td style='border: 1px solid #ddd;padding: 8px;text-align: right;'><b>&euro; {{price}} </b></td>
</tr>";
            decimal totalPrice = 0;
            int totalCards = 0;
            // using (var reader = System.IO.File.OpenText(@"html/order/orderTabel.html"))
            // {
            //     ordeTable = reader.ReadToEnd();
            // }
            for (int i = 0; i < orderitems.Count; i++)
            {
                // using (var reader = System.IO.File.OpenText(@"html/order/orderRow.html"))
                // {

                    var newRow = baseRow;
                    newRow = newRow.Replace("{{name}}", orderitems[i].Name);
                    newRow = newRow.Replace("{{quantity}}", orderitems[i].Quantity.ToString());
                    decimal price = (((decimal)orderitems[i].Price) * ((decimal)orderitems[i].Quantity)) / 100;
                    newRow = newRow.Replace("{{price}}", string.Format("{0:0.00}", price));

                    row += newRow;
                    totalPrice += price;
                    totalCards += orderitems[i].Quantity;
                // }
            }
            // using (var reader = System.IO.File.OpenText(@"html/order/orderRow.html"))
            // {
                var totalRow = baseRow ;
                totalRow = totalRow.Replace("{{name}}", "");
                totalRow = totalRow.Replace("{{quantity}}", $"<b>{totalCards}</b>");
                totalRow = totalRow.Replace("{{price}}", $"{string.Format("{0:0.00}", totalPrice)}");

                row += totalRow;
            // }
            ordeTable = ordeTable.Replace("{{orderRows}}", row);
            return ordeTable;
        }

    }
}