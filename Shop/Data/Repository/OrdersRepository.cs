using Shop.Data.interfaces;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Data.Repository {
    public class OrdersRepository : IAllOrders {

        private readonly AppDBContent appDBContent;
        private readonly ShopCart shopCart;

        public OrdersRepository(AppDBContent appDBContent, ShopCart shopCart) {
            this.appDBContent = appDBContent;
            this.shopCart = shopCart;
        }

        public void createOrder(Order order)
        {
            order.orderTime = DateTime.Now;
            appDBContent.Order.Add(order);
            appDBContent.SaveChanges(); // Сохраняем заказ, чтобы получить идентификатор

            var items = shopCart.listShopItems;

            foreach (var el in items)
            {
                var orderDetail = new OrderDetail()
                {
                    CarID = el.car.id,
                    orderID = order.id, // Идентификатор заказа теперь доступен
                    price = el.car.price
                };
                appDBContent.OrderDetail.Add(orderDetail);
            }

            appDBContent.SaveChanges(); // Сохраняем детали заказа
        }

    }
}
