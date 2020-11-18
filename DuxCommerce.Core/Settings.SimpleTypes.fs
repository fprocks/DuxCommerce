namespace DuxCommerce.Settings.SimpleTypes

open DuxCommerce.Common

type StoreId = private StoreId of int64

module StoreId =
    let value (StoreId id) = id
    let create id = StoreId id
    
type StoreContactEmail = private StoreContactEmail of EmailAddress

module StoreContactEmail =
    let value (StoreContactEmail (contactEmail)) =
        EmailAddress.value contactEmail
        
    let create contactEmail =
        result {
            let! email = EmailAddress.create "ContactEmail" contactEmail
            StoreContactEmail email
        }
        
type StoreSenderEmail = private StoreSenderEmail of EmailAddress

module StoreSenderEmail =
    let value (StoreSenderEmail (senderEmail)) =
        EmailAddress.value senderEmail
        
    let create senderEmail =
        result {
            let! email = EmailAddress.create "SenderEmail" senderEmail
            StoreSenderEmail email
        }
        
type TimeZoneId = TimeZoneId of string
      
      
type AddressId = private AddressId of int64

module AddressId =
    let value (AddressId id) = id
    let create id = AddressId id
    
    
type CountryCode = private CountryCode of String2
               