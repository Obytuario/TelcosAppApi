using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using AplicationServices.Helpers.TextResorce;

namespace AplicationServices.DTOs.Generics
{
    //
    // Resumen:
    //     Encapsula el resultado de la petición
    //
    // Parámetros de tipo:
    //   T:
    //     Tipo del resultado
    public sealed class RequestResult<T>
    {
        //
        // Resumen:
        //     Obtiene un valor que indica si el resultado de la petición fue satisfactorio
        public bool IsSuccessful { get; set; }

        //
        // Resumen:
        //     Obtiene un valor que indica si ocurrió algún error al ejecutar la petición
        public bool IsError { get; set; }

        //
        // Resumen:
        //     Obtiene el mensaje del error ocurrido
        public string ErrorMessage { get; set; }

        //
        // Resumen:
        //     Obtiene la enumeración de mensajes de las validaciones que no permitieron que
        //     el resultado fuera satisfactorio
        public IEnumerable<string> Messages { get; set; }

        //
        // Resumen:
        //     Obtiene el objeto resultado de la petición
        public T Result { get; set; }

        //
        // Resumen:
        //     Propiedad extra con fines de extender opcionalmente la información del resultado.
        //     No ex serializable
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public object Extended { get; set; }

        //
        // Resumen:
        //     Inicializa una nueva instancia de la clase
        public RequestResult()
        {
        }

        //
        // Resumen:
        //     Inicializa una nueva instancia de la clase
        //
        // Parámetros:
        //   isSuccessful:
        //     Valor que indica si el resultado de la petición fue satisfactorio
        //
        //   isError:
        //     Valor que indica si ocurrió algún error en la ejecución
        //
        //   errorMessage:
        //     Mensaje del error ocurrido
        //
        //   messages:
        //     Enumeración de mensajes de validación que no permitieron que el resultado fuera
        //     satisfactorio
        //
        //   result:
        //     Objeto resultado de la petición
        internal RequestResult(bool isSuccessful, bool isError, string errorMessage, IEnumerable<string> messages, T result)
        {
            IsSuccessful = isSuccessful;
            IsError = isError;
            ErrorMessage = errorMessage;
            Messages = messages;
            Result = result;
        }

        //
        // Resumen:
        //     Crea un resultado de petición exitoso
        //
        // Parámetros:
        //   result:
        //     Objeto resultado
        //
        // Devuelve:
        //     Resultado de la petición
        public static RequestResult<T> CreateSuccessful(T result)
        {
            return new RequestResult<T>(isSuccessful: true, isError: false, null, null, result);
        }

        //
        // Resumen:
        //     Crea un resultado de petición fracasado
        //
        // Parámetros:
        //   messages:
        //     Enumeración de mensajes de validación que no permitieron que el resultado fuera
        //     exitoso
        //
        // Devuelve:
        //     Resultado de la petición
        public static RequestResult<T> CreateUnsuccessful(T result ,IEnumerable<string> messages,string errorMessage = null )
        {
            return new RequestResult<T>(isSuccessful: false, isError: false, errorMessage, messages, result);
        }

        //
        // Resumen:
        //     Crea un resultado de petición con error
        //
        // Parámetros:
        //   errorMessage:
        //     Mensaje del error ocurrido
        //
        // Devuelve:
        //     Resultado de la petición
        public static RequestResult<T> CreateError(string errorMessage)
        {
            return new RequestResult<T>(isSuccessful: false, isError: true, errorMessage, null, default(T));
        }

        //
        // Resumen:
        //     Crea un resultado no satisfactorio con un mensaje predefinido cuando una entidad
        //     consultada por su Id no ha sido encontrada en el repositorio de información
        //
        // Parámetros:
        //   entityId:
        //     Id de la entidad consultada
        //
        // Devuelve:
        //     Resultado de la petición
        public static RequestResult<T> CreateEntityNotExists(string entityId = null)
        {
            entityId = entityId?.Trim() ?? string.Empty;
            return new RequestResult<T>(isSuccessful: false, isError: false, null, new string[1] { RequestResultMsm.EntityNotExists+" "+ entityId }, default(T));
        }

        //
        // Resumen:
        //     Crea un resultado no satisfactorio con un mensaje predefinido cuando se intenta
        //     insertar una entidad que ya existe en el repositorio de información
        //
        // Parámetros:
        //   entityId:
        //     Id de la entidad a insertar
        //
        // Devuelve:
        //     Resultado de la petición
        public static RequestResult<T> CreateEntityAlreadyExists(string entityId = null)
        {
            entityId = entityId?.Trim() ?? string.Empty;
            return new RequestResult<T>(isSuccessful: false, isError: false, null, new string[1] { RequestResultMsm.EntityAlreadyExists + " " + entityId }, default(T));
        }

        //
        // Resumen:
        //     Crea un resultado no satisfactorio con un mensaje predefinido cuando se intenta
        //     realizar una operación con una entidad y su estado no corresponde con la operación
        //     a realizar
        //
        // Parámetros:
        //   entityName:
        //     Nombre de la entidad
        //
        // Devuelve:
        //     Resultado de la petición
        public static RequestResult<T> CreateEntityInvalidState(string entityName)
        {
            entityName = entityName?.Trim() ?? string.Empty;
            return new RequestResult<T>(isSuccessful: false, isError: false, null, new string[1] { RequestResultMsm.EntityInvalidState + " " + entityName }, default(T));
        }
    }
}
