namespace ERP.Helper.Helper
{
    public class TemplateHtmlHelper
    {
        public string EmailCreateUser(string userName, string email)
        {
            return $"""
                   <h1>Hola</h1><p>Bienvenido <span style="color: green;"><strong>"{userName}"</strong></span></p><p>
                    Su correo es: {email}
                    </p>
                """;
        }
    }
}
