namespace ERP.Helper.Helper.TemplateView
{
    public interface ITemplateViewHelper
    {
        public Task<string> RenderViewToStringAsync(string view, object data);
    }
}
