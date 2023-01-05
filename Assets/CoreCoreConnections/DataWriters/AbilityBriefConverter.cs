using System;
using DndCore.Ability;
using Mirror;

namespace MirrorConverter
{
    public static class AbilityBriefConverter
    {
        public static void WriteAbilityBriefConverter(this NetworkWriter writer, AbilityBrief abilityBrief)
        {
            writer.Write<Guid>(abilityBrief.Id);
            writer.WriteString(abilityBrief.Name);
            writer.WriteString(abilityBrief.Description);
            writer.WriteInt((int)abilityBrief.ActionType);
            
            writer.WriteInt(abilityBrief.Instructions.Count);
            foreach(AbilityInputInstruction instruction in abilityBrief.Instructions)
            {
                writer.Write<AbilityInputInstruction>(instruction);
            }
        }

        public static AbilityBrief ReadAbilityBriefConverter(this NetworkReader reader)
        {
            Guid id = reader.Read<Guid>();
            string name = reader.ReadString();
            string description = reader.ReadString();
            AbilityActionType actionType = (AbilityActionType)reader.ReadInt();

            Ability ability = new Ability(name, description, actionType);
            ability.Id = id;

            int instructionCount = reader.ReadInt();
            for(int i = 0 ; i < instructionCount; i++)
            {
                ability.Instructions.Add(reader.Read<AbilityInputInstruction>());
            }

            return new AbilityBrief(ability);
        }
    }
}
