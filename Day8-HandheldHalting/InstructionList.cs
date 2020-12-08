using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;

namespace Day8_HandheldHalting
{
    public class InstructionList
    {
        public List<Instruction> Instructions { get; set; } 
        public int Acc { get; set; }
        
        public int ActiveIndex { get; set; }

        public Instruction Parse(string s)
        {
            InstructionType intType;
            InstructionType.TryParse(s.Split(" ")[0], out intType);
            
            return new Instruction()
            {
                Type = intType,
                Arg = int.Parse(s.Split(" ")[1]),
                Visited = 0
            };
        }

        public List<Instruction> ParseMany(string[] sm)
        {
            return sm.Select(x => Parse(x)).ToList();
        }

        public InstructionList(string[] input, int acc = 0)
        {
            Instructions = ParseMany(input);
            Acc = acc;
            ActiveIndex = 0;
        }

        public void ExecuteCurrentInstruction()
        {
            switch (Instructions[ActiveIndex].Type)
            {
                case InstructionType.acc:
                {
                    Acc += Instructions[ActiveIndex].Arg;
                    Instructions[ActiveIndex].Visited += 1;
                    ActiveIndex += 1;
                    break;
                }
                case InstructionType.jmp:
                {
                    Instructions[ActiveIndex].Visited += 1;
                    ActiveIndex += Instructions[ActiveIndex].Arg;
                    break;
                }
                case InstructionType.nop:
                {
                    Instructions[ActiveIndex].Visited += 1;
                    ActiveIndex += 1;
                    break;
                }
                case InstructionType.end:
                {
                    break;
                }
            }
        }

        public int ExecuteUntilRepeat()
        {
            while (Instructions[ActiveIndex].Visited < 1)
            {
                ExecuteCurrentInstruction();
            }

            return Acc;
        }
        
        public int ExecuteUntilFix()
        {
            for (int i = 0; i < Instructions.Count; i++)
            {
                if (Instructions[i].Type == InstructionType.jmp || Instructions[i].Type  == InstructionType.nop)
                {
                    ResetValues();

                    Instructions[i].Type = Switch(Instructions[i].Type);

                    while (Instructions[ActiveIndex].Visited < 1)
                    {
                        if (Instructions[ActiveIndex].Type == InstructionType.end)
                        {
                            return Acc;
                        }

                        ExecuteCurrentInstruction();
                    }

                    Instructions[i].Type = Switch(Instructions[i].Type);
                }
                
            }

            return 0;
        }

        public InstructionType Switch(InstructionType t)
        {
            if (t == InstructionType.jmp)
            {
                return InstructionType.nop;
            }
            if (t == InstructionType.nop)
            {
                return InstructionType.jmp;
            }

            return t;
        }

        public void ResetValues()
        {
            Acc = 0;
            ActiveIndex = 0;
            foreach (var i in Instructions)
            {
                i.Visited = 0;
            }
        }

    }
}