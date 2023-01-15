using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Translation
{
    public class GuidTranslate : LuaTranslate
    {
        public void RegisterFromLua()
        {
            Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.String, typeof(Guid),
                dynVal =>
                {
                    return Guid.Parse(dynVal.String);
                }
            );
        }

        public void RegisterToLua()
        {
            Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Guid>(
                (script, guid) => {
                    return DynValue.NewString(guid.ToString());
                }
            );
        }
    }
}
