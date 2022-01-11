using Server.Api.Dtos;
using Server.Api;

namespace Server.Api {
    public interface IRepository {
        public List<Product> LoadProducts(int ID);
        public Customer LoadCustomer(string firstName, string lastName);
        public Store LoadStore(int ID);
        public void SaveNewPerson(string firstName, string lastName, string password, int storeID);
        public List<Store> GetStoreID();
        public void SaveItems(Item item, int storeId, int customerId);
        public void SaveOrder(int storeId, int customerId, decimal total);
        public List<OrderHistory> LoadOrderHistory(int customerId);
        public bool MakePurchase(Item item, int storeId);
        public List<OrderHistory> LoadOrderHistory();





    }
}
