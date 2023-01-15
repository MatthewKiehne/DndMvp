using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Translation
{
    public class Vector2IntTranslate : LuaTranslate
    {
        private readonly string xVariable = "x";
        private readonly string yVariable = "y";

        public void RegisterFromLua()
        {
            Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Table, typeof(Vector2Int),
                dynVal =>
                {
                    Table table = dynVal.Table;
                    int x = (int)table.Get(xVariable).Number;
                    int y = (int)table.Get(yVariable).Number;
                    
                    return new Vector2Int(x, y);
                }
            );
        }

        public void RegisterToLua()
        {
            Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Vector2Int>(
                (script, vector) => {
                    Table table = new Table(script);
                    table.Set(xVariable, DynValue.NewNumber(vector.x));
                    table.Set(yVariable, DynValue.NewNumber(vector.y));

                    return DynValue.NewTable(table);
                }
            );
        }
    }
}
