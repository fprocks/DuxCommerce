namespace DuxCommerce.ShoppingCarts.Dto

//open DuxCommerce.ShoppingCarts.PublicTypes
//open DuxCommerce.ShoppingCarts.InternalTypes
//open DuxCommerce.ShoppingCarts.SimpleTypes
//open DuxCommerce.Catalogue.SimpleTypes
//open DuxCommerce.Common

//module CartItemInfo = 

//    let fromDomain (cartItem:CartItem) :CartItemInfo =
//        {
//            Id = CartItemId.value cartItem.Id
//            CartId = CartId.value cartItem.CartId
//            ProductId = ProductId.value cartItem.ProductId
//            ProductName = String255.value cartItem.ProductName
//            Price = SalePrice.value cartItem.Price
//            Quantity = ItemQuantity.value cartItem.Quantity
//            ItemTotal = ItemTotal.value cartItem.ItemTotal
//        }
            
//    let toDomain (itemInfo:CartItemInfo) :CartItem =
//        {
//            Id = CartItemId.create itemInfo.Id
//            CartId = CartId.create itemInfo.CartId
//            ProductId = ProductId.create itemInfo.ProductId
//            ProductName = String255 itemInfo.ProductName
//            Price = SalePrice.create itemInfo.Price
//            Quantity = ItemQuantity.create itemInfo.Quantity
//            ItemTotal = ItemTotal.create itemInfo.ItemTotal
//        }

//module CartInfo =

//    let fromDomain (cart:ShoppingCart) :CartInfo =
//        {
//            Id = CartId.value cart.Id
//            ShopperId = ShopperId.value cart.ShopperId
//            LineItems = cart.LineItems |> Seq.map CartItemInfo.fromDomain
//            CartTotal = CartTotal.value cart.CartTotal
//        }
    
//    let toDomain (cartInfo:CartInfo) :ShoppingCart =
//        {
//            Id = CartId.create cartInfo.Id
//            ShopperId = ShopperId.create cartInfo.ShopperId
//            LineItems = cartInfo.LineItems |> Seq.map CartItemInfo.toDomain
//            CartTotal = CartTotal.create cartInfo.CartTotal
//        }