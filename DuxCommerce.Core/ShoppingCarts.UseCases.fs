namespace DuxCommerce.ShoppingCarts

module UseCases =

    let addCartItem connString shopperId request =
        
        // Todo: retrieve shopper cart
        let cart : CartInfo = {
            Id = 0L
            ShopperId = shopperId
            CartTotal = 0.0M
            }
        
        // Todo: retrieve product using request
        
        // Todo: convert request to cart item
        let cartItem : CartItemInfo = {
            Id = 0L
            CartId = 0L
            ProductName = ""
            Price = 0.0M
            Quantity = 0.0M
            ItemTotal = 0.0M
            }
                    
        DataAccess.addCartItem connString cart cartItem