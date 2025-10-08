using ERP.CoreDB;
using ERP.Helper.Models;
using ERP.Models.Security.Options;

namespace ERP.Bll.Security.Option
{
    public interface IOptionBll
    {
        ResponseGeneralModel<string?> CreateOption(OptionRequestModel request);
        ResponseGeneralModel<bool?> EditOption(int id, OptionRequestModel requestModel);
        public List<Opcion> GetOption();

    }
}
