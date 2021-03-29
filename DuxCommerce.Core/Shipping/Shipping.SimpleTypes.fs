namespace DuxCommerce.Core.Shipping.SimpleTypes

open DuxCommerce.Common

type StoreProfileId = private StoreProfileId of string
module StoreProfileId =
    let value (StoreProfileId id) = id
    let create id = StoreProfileId id
    
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

type ShippingOriginId = private ShippingOriginId of string
module ShippingOriginId =
    let value (ShippingOriginId id) = id
    let create id = ShippingOriginId id

type ShippingProfileId = private ShippingProfileId of string
module ShippingProfileId =
    let value (ShippingProfileId id) = id
    let create id = ShippingProfileId id

type ShippingMethodId = private ShippingMethodId of string
module ShippingMethodId =
    let value (ShippingMethodId id) = id
    let create id = ShippingMethodId id

type StateId = private StateId of string
module StateId =
    let value (StateId id) = id
    let create id = StateId id

type RateCondition = private RateCondition of decimal
module RateCondition =
    let value (RateCondition cond) = cond
    let create cond = RateCondition cond    

type ShippingRateAmount = private ShippingRateAmount of decimal
module RateAmount =
    let value (ShippingRateAmount amount) = amount
    let create amount = ShippingRateAmount amount    