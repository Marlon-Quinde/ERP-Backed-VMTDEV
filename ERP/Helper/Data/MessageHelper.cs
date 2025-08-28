namespace ERP.Helper.Data
{
    public class MessageHelper
    {
        public readonly static string errorParamsGeneral = "Los campos deben tener información válida";

        public readonly static string errorGeneral = "Ocurrio un error, intente más tarde";

        public readonly static string errorDB = "Ocurrio un error con la conexión a la BD";

        public readonly static string profileChangePasswordSuccess = "La contraseña se cambio con éxito";
        public readonly static string profileChangePasswordErrorNotEqualsOldPassword = "La contraseña ingresada no coincide con la anterior";
        public readonly static string profileChangePasswordErrorNotEqualsPass = "Las nuevas contraseñas no son iguales";
        public readonly static string profileChangeNameUserSuccess = "Se cambio el nombre del usuario";

        public readonly static string encryptError = "Ocurrio un error al codificar el texto";
        public readonly static string desencryptError = "Ocurrio un error al decodificar el texto";

        public readonly static string jwtErrorEncrypt = "Ocurrio un error al generar el JWT";
        public readonly static string jwtErrorDesencrypt = "Ocurrio un error al decodificar el token de sesión";
        public readonly static string jwtErrorDesencryptTime = "El token de sesión caducó";


        public readonly static string loginIncorrect = "Usuario y/o contraseña, no existe";
        public readonly static string registerSuccess = "Se creó el usuario con éxtio";
        public readonly static string registerErrorExist = "Ya existe un usuario con el mismo correo";
    }
}
