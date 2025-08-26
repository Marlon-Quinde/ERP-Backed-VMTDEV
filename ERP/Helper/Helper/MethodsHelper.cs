using ERP.Helper.Models;
using HelperGeneral.Models;
using HelperGeneral.Helper;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Helper.Helper
{
    public class MethodsHelper<T>
    {
        public IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public DataEncryptModel GetDataEncrypt(string key)
        {
            IConfiguration configuration = GetConfiguration();
            IConfigurationSection section = configuration.GetSection("encryptData").GetSection(key);
            return new DataEncryptModel(section.GetValue<string>("key"), section.GetValue<string>("iv"));
        }

        public ResponseGeneralModel<T> EncryptDataByMethod(string method, string text)
        {
            DataEncryptModel modelEncrypt = GetDataEncrypt(method);
            ResponseData<string> response = AES256Encryption.Encrypt(text, modelEncrypt.key, modelEncrypt.iv);
            if(response.isTrue)
            {
                return new ResponseGeneralModel<T>(200, response.data);
            } else
            {
                return new ResponseGeneralModel<T>(response.message, response.error, 500);
            }
        }

        public ResponseGeneralModel<T> DesencryptDataByMethod(string method, string text)
        {
            DataEncryptModel modelEncrypt = GetDataEncrypt(method);
            ResponseData<string> response = AES256Encryption.Decrypt(text, modelEncrypt.key, modelEncrypt.iv);
            if (response.isTrue)
            {
                return new ResponseGeneralModel<T>(200, response.data);
            }
            else
            {
                return new ResponseGeneralModel<T>(response.message, response.error, 500);
            }
        }

        public static ResponseGeneralModel<T> EncryptPassUser(string text)
        {
            return (new MethodsHelper<T>()).EncryptDataByMethod("passUser", text);
        }

        public static ResponseGeneralModel<T> DesencryptPassUser(string text)
        {
            return (new MethodsHelper<T>()).DesencryptDataByMethod("passUser", text);
        }

        public ResponseGeneralModel<T> GenerateJwtSession(SessionModel model)
        {
            IConfigurationSection section = GetConfiguration().GetSection("jwtSession");

            JwtGenerator<T> JWTG = new JwtGenerator<T>(
                SecretKey: section.GetValue<string>("secretKey"),
                Name: section.GetValue<string>("name"),
                Rol: section.GetValue<string>("rol"),
                Issuer: section.GetValue<string>("issuer"),
                Audience: section.GetValue<string>("audience"),
                DurationSec: section.GetValue<int>("durationSec")
            );

            string dataJwt = JsonConvert.SerializeObject(model);

            return JWTG.GenerateJwt(dataJwt);
        }

        public ResponseGeneralModel<T> DecodeJwtSession(string token)
        {
            IConfigurationSection section = GetConfiguration().GetSection("jwtSession");

            JwtGenerator<T> JWTG = new JwtGenerator<T>(
                SecretKey: section.GetValue<string>("secretKey"),
                Name: section.GetValue<string>("name"),
                Rol: section.GetValue<string>("rol"),
                Issuer: section.GetValue<string>("issuer"),
                Audience: section.GetValue<string>("audience"),
                DurationSec: section.GetValue<int>("durationSec")
            );

            return JWTG.DeserializeJwt(token);
        }

        public static ResponseGeneralModel<T> TransFormResultHelperDll(ResponseData<T> data)
        {
            return new ResponseGeneralModel<T>(
                data.isTrue ? 200 : 500,
                data.data,
                data.message,
                data.error
            );
        }

        public MongoLibrary.Helper.MongoHelper ConnectionMongo()
        {
            IConfigurationSection mongoC = GetConfiguration().GetSection("mongo");
            MongoLibrary.Helper.MongoHelper mongoDB = new MongoLibrary.Helper.MongoHelper(mongoC.GetValue<string>("connection"), mongoC.GetValue<string>("dataBase"));
            return mongoDB;
        }
    }
}
