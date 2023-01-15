using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DndCore.Ability;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Translation
{
    public class AbilityTargetTranslate : LuaTranslate
    {
        private readonly string EntityIdsVariable = "entityIds";
        private readonly string TargetTypeVariable = "targetType";
        private readonly string TargetPositionVariable = "targetPosition";

        public void RegisterFromLua()
        {
            Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Table, typeof(AbilityTarget),
                dynVal =>
                {
                    Table table = dynVal.Table;
                    AbilityTarget result = new AbilityTarget();

                    Table entities = table.Get(EntityIdsVariable).Table;
                    foreach(DynValue entitiesId in entities.Values) {
                        result.EntityIds.Add(entitiesId.ToObject<Guid>());
                    }

                    result.TargetType = (AbilityTargetType)Enum.Parse(typeof(AbilityTargetType), table.Get(TargetTypeVariable).String);
                    result.TargetPosition = table.Get(TargetPositionVariable).ToObject<Vector2Int>();
                    
                    return result;
                }
            );
        }

        public void RegisterToLua()
        {
            Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<AbilityTarget>(
                (script, abilityTarget) => {
                    Table table = new Table(script);

                    DynValue[] entityIds = abilityTarget.EntityIds.Select( x => DynValue.FromObject(script, x)).ToArray();
                    DynValue entities = DynValue.NewTable(script, entityIds);
                    table.Set(EntityIdsVariable, entities);

                    table.Set(TargetTypeVariable, DynValue.NewString(abilityTarget.TargetType.ToString()));
                    table.Set(TargetPositionVariable, DynValue.FromObject(script, abilityTarget.TargetPosition));

                    return DynValue.NewTable(table);
                }
            );
        }
    }
}
