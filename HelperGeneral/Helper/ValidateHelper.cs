using HelperGeneral.Data;
using HelperGeneral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelperGeneral.Helper
{
    public class ValidateHelper<T>
    {
        public ResponseData<T> ValidResp(string Value, string Name, int? Max = null, int? Min = null, List<string>? ListRegExp = null, string? MsjMinV = null, string? MsjMaxV = null, List<string>? ListMsjRegExp = null)
        {
            if (Max != null)
            {
                if (!MaxLength(Value, Max ?? 0)) return new ResponseData<T>(
                    MessageHelper.errorParamsGeneral,
                    MsjMaxV ?? "El parametro '" + Name + "' excede el límite de " + Max + " caracteres"
                );
            }
            if (Min != null)
            {
                if (!MinLength(Value, Min ?? 0)) return new ResponseData<T>(
                    MessageHelper.errorParamsGeneral,
                    MsjMinV ?? "La longitud del parametro '" + Name + "' debe ser mayor a " + Min + " caracteres"
                );
            }

            if (ListRegExp != null)
            {
                for (int i = 0; i < ListRegExp.Count; i++)
                {
                    if (!RegExpVald(Value, ListRegExp[i]))
                    {
                        bool isMsjPers = ListMsjRegExp != null ? ListMsjRegExp.Count >= (i - 1) : false;
                        return new ResponseData<T>(
                            MessageHelper.errorParamsGeneral,
                            isMsjPers ? ListMsjRegExp[i] : "El parametro '" + Name + "' no cumple con la expresión regular " + ListRegExp[i]
                        );
                    }
                }
            }


            return new ResponseData<T>();
        }

        bool MaxLength(string Value, int Max)
        {
            return (Value.Length <= Max);
        }

        bool MinLength(string Value, int Min)
        {
            return (Value.Length >= Min);
        }

        bool RegExpVald(string Value, string RegExp)
        {
            Regex RegE = new Regex(RegExp);
            return RegE.IsMatch(Value);
        }


        bool EmailValid(string Value)
        {
            string RegExpEmail = VarHelper.RegExpEmail;
            return RegExpVald(Value, RegExpEmail);
        }
    }
}
