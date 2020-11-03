namespace DuxCommerce.Common

open System

type ValidationError = ValidationError of string

type DataIntegrityError = DataIntegrityError of string

type InternalServerError = {
    Exception : Exception
    }

type ServiceInfo = {
    Name : string
    Endpoint: Uri
    }

type RemoteServiceError = {
    Service : ServiceInfo 
    Exception : Exception
    }

type ErrorType =
    | Validation of ValidationError
    | DataIntegrity of DataIntegrityError
    | InternalServer of InternalServerError
    | RemoteService of RemoteServiceError
    
type CustomError = {
    Error : ErrorType
}

module CustomError =
    let mapValidation (source: Result<_,string>) :Result<_,CustomError> =
        let error = Result.mapError ValidationError source
        let f = fun e -> { Error = Validation e }
        Result.mapError f error
        
    let mapDataIntegrity (source: Result<_,string>) :Result<_,CustomError> =
        let error = Result.mapError DataIntegrityError source
        let f = fun e -> { Error = DataIntegrity e }
        Result.mapError f error
        
    let mapInternalServer (source: Result<_,Exception>) :Result<_,CustomError> =
        let f = fun e -> { Exception = e }
        let error = Result.mapError f source
        let f' = fun e -> { Error = InternalServer e }
        Result.mapError f' error