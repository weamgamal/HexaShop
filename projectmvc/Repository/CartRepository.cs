using projectmvc.Models;

namespace projectmvc.Repository
{
    public class CartRepository: ICartRepository
    {

        Context context;
        public CartRepository(Context context)//inject
        {
            this.context = context;// new Context();
        }

        //CRUD operation
        public List<Cart> GetAll()
        {
            return context.Cart.ToList();
        }

        public Cart GetById(int id)
        {
            return context.Cart.FirstOrDefault(e => e.Id == id);
        }
        public void Insert(Cart cart)
        {
            context.Cart.Add(cart);
            context.SaveChanges();
        }

     /*   public void Update(int id, Cart cart)
        {
            Cart oldCart = GetById(id);

            oldCart.ProductID = cart.ProductID;
            oldCart.CustomerID= cart.CustomerID;

            context.SaveChanges();

        }*/

        public void Delete(int id)
        {
            Cart oldCart = GetById(id);
            context.Cart.Remove(oldCart);
            context.SaveChanges();
        }


    }
}
