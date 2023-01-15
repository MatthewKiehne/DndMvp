using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DndCore.Ability;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Lua.Translation
{
    public class AbilityInputInstructionTranslate : LuaTranslate
    {
        private readonly string InstructionsVariable = "instructions";
        private readonly string RangeVariable = "range";
        private readonly string OptionalVariable = "optional";
        private readonly string AbilityTargetTypeVariable = "abilityTargetType";

        public void RegisterFromLua()
        {
            Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Table, typeof(AbilityInputInstruction),
                dynVal =>
                {
                    Table table = dynVal.Table;
                    AbilityInputInstruction result = new AbilityInputInstruction();

                    Table instructions = table.Get(InstructionsVariable).Table;
                    if (instructions != null)
                    {
                        foreach (DynValue instruction in instructions.Values)
                        {
                            result.Instructions.Add(instruction.ToObject<AbilityInputInstruction>());
                        }
                    }

                    result.AbilityTargetType = (AbilityTargetType)Enum.Parse(typeof(AbilityTargetType), table.Get(AbilityTargetTypeVariable).String);
                    result.Optional = table.Get(OptionalVariable).Boolean;
                    result.Range = (int)table.Get(RangeVariable).Number;

                    return result;
                }
            );
        }

        public void RegisterToLua()
        {
            Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<AbilityInputInstruction>(
                (script, abilityInputInstruction) =>
                {
                    Table table = new Table(script);

                    DynValue[] nestedInstructions = abilityInputInstruction.Instructions.Select(x => DynValue.FromObject(script, x)).ToArray();
                    DynValue instructions = DynValue.NewTable(script, nestedInstructions);
                    table.Set(InstructionsVariable, instructions);

                    table.Set(RangeVariable, DynValue.NewNumber(abilityInputInstruction.Range));
                    table.Set(OptionalVariable, DynValue.NewBoolean(abilityInputInstruction.Optional));
                    table.Set(AbilityTargetTypeVariable, DynValue.NewString(abilityInputInstruction.AbilityTargetType.ToString()));

                    return DynValue.NewTable(table);
                }
            );
        }
    }
}
