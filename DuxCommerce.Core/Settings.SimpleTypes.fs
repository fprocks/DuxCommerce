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
        
    let create contactEmail :Result<StoreContactEmail, string>=
        result {
            let! email = EmailAddress.create "ContactEmail" contactEmail
            return (StoreContactEmail email)
        }
        
type StoreSenderEmail = private StoreSenderEmail of EmailAddress
module StoreSenderEmail =
    let value (StoreSenderEmail (senderEmail)) =
        EmailAddress.value senderEmail
        
    let create senderEmail =
        result {
            let! email = EmailAddress.create "SenderEmail" senderEmail
            return (StoreSenderEmail email)
        }
        
type TimeZoneId = TimeZoneId of String50
module TimeZoneId =
    let value (TimeZoneId id) = id
    let create field id =
        result {
            let! zoneId = String50.create field id
            return (TimeZoneId zoneId)
        }
      
type AddressId = private AddressId of int64
module AddressId =
    let value (AddressId id) = id
    let create id = AddressId id
    
    
type CountryCode = private CountryCode of String2

module CountryCode =
    let value (CountryCode code) = code
    let create field code =
        result {
            let! countryCode = String2.create field code
            return CountryCode countryCode
        }
               