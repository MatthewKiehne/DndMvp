using DndCore.Ability;
using Mirror;

namespace MirrorConverter
{
    public static class AbilityInputInstructionConverter
    {
        public static void WriteAbilityInputInstructionConverter(this NetworkWriter writer, AbilityInputInstruction abilityInputInstruction)
        {
            writer.WriteInt((int)abilityInputInstruction.AbilityTargetType);
            writer.WriteInt(abilityInputInstruction.Range);
            writer.WriteBool(abilityInputInstruction.Optional);


            writer.WriteInt(abilityInputInstruction.Instructions.Count);
            foreach (AbilityInputInstruction instruction in abilityInputInstruction.Instructions)
            {
                writer.Write<AbilityInputInstruction>(instruction);
            }

        }

        public static AbilityInputInstruction ReadAbilityInputInstructionConverter(this NetworkReader reader)
        {
            AbilityTargetType t = (AbilityTargetType)reader.ReadInt();
            int range = reader.ReadInt();
            bool optional = reader.ReadBool();
            AbilityInputInstruction result = new AbilityInputInstruction(t, range, optional);

            int instructionCount = reader.ReadInt();
            for (int i = 0; i < instructionCount; i++)
            {
                result.Instructions.Add(reader.Read<AbilityInputInstruction>());
            }

            return result;
        }
    }
}
