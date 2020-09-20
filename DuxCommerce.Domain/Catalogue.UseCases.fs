namespace DuxCommerce.Catalogue

open DuxCommerce.Common

module UseCases = 
    type CreateProduct = CreateProductRequest -> AsyncResult<int, CreateProductError>
